using System;
using System.Reflection;
using StaticMock.Services.Injection.Entities;

namespace StaticMock.Services.Injection.Implementation
{
    internal class InjectionServiceX32 : IInjectionService
    {
        private MethodMemoryInfo<uint> _x32MethodMemoryInfo;

        private readonly MethodBase _method;

        public InjectionServiceX32(MethodBase method)
        {
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public unsafe IReturnable Inject(MethodBase methodToInject)
        {
            if (methodToInject == null)
            {
                throw new ArgumentNullException(nameof(methodToInject));
            }

            var methodPtr = (byte*) _method.MethodHandle.GetFunctionPointer().ToPointer();

            SaveMethodMemoryInfo(methodPtr);

            *methodPtr = 0x49;
            *(methodPtr + 1) = 0xBB;
            *(uint*)(methodPtr + 2) = (uint)methodToInject.MethodHandle.GetFunctionPointer().ToInt32();
            *(methodPtr + 6) = 0x41;
            *(methodPtr + 7) = 0xFF;
            *(methodPtr + 8) = 0xE3;

            return this;
        }

        public unsafe void Return()
        {
            var methodPtr = (byte*) _method.MethodHandle.GetFunctionPointer().ToPointer();

            *methodPtr = _x32MethodMemoryInfo.Byte1;
            *(methodPtr + 1) = _x32MethodMemoryInfo.Byte2;
            *(uint*) (methodPtr + 2) = _x32MethodMemoryInfo.MethodMemoryValue;
            *(methodPtr + 6) = _x32MethodMemoryInfo.Byte1AfterMethod;
            *(methodPtr + 7) = _x32MethodMemoryInfo.Byte2AfterMethod;
            *(methodPtr + 8) = _x32MethodMemoryInfo.Byte3AfterMethod;
        }

        public void Dispose()
        {
            Return();
        }

        private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
        {
            _x32MethodMemoryInfo.Byte1 = *methodPtr;
            _x32MethodMemoryInfo.Byte2 = *(methodPtr + 1);
            _x32MethodMemoryInfo.MethodMemoryValue = *(uint*) (methodPtr + 2);
            _x32MethodMemoryInfo.Byte1AfterMethod = *(methodPtr + 6);
            _x32MethodMemoryInfo.Byte2AfterMethod = *(methodPtr + 7);
            _x32MethodMemoryInfo.Byte3AfterMethod = *(methodPtr + 8);
        }
    }
}
