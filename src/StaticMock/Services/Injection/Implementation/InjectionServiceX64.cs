using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using StaticMock.Services.Injection.Entities;

namespace StaticMock.Services.Injection.Implementation
{
    internal class InjectionServiceX64 : IInjectionService
    {
        private MethodMemoryInfo<ulong> _x64MethodMemoryInfo;

        private readonly MethodBase _method;

        public InjectionServiceX64(MethodBase method)
        {
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public unsafe IReturnable Inject(MethodBase methodToInject)
        {
            if (methodToInject == null)
            {
                throw new ArgumentNullException(nameof(methodToInject));
            }

            RuntimeHelpers.PrepareMethod(_method.MethodHandle);
            RuntimeHelpers.PrepareMethod(methodToInject.MethodHandle);

            var methodPtr = (byte*) _method.MethodHandle.GetFunctionPointer().ToPointer();

            SaveMethodMemoryInfo(methodPtr);

            *methodPtr = 0x49; // mov r11, target
            *(methodPtr + 1) = 0xBB;
            *(ulong*)(methodPtr + 2) = (ulong)methodToInject.MethodHandle.GetFunctionPointer().ToInt64();
            *(methodPtr + 10) = 0x41;
            *(methodPtr + 11) = 0xFF;
            *(methodPtr + 12) = 0xE3;

            return this;
        }

        public unsafe void Return()
        {
            //RuntimeHelpers.PrepareMethod(_method.MethodHandle);

            var methodPtr = (byte*) _method.MethodHandle.GetFunctionPointer().ToPointer();

            *methodPtr = _x64MethodMemoryInfo.Byte1;
            *(methodPtr + 1) = _x64MethodMemoryInfo.Byte2;
            *(ulong*) (methodPtr + 2) = _x64MethodMemoryInfo.MethodMemoryValue;
            *(methodPtr + 10) = _x64MethodMemoryInfo.Byte1AfterMethod;
            *(methodPtr + 11) = _x64MethodMemoryInfo.Byte2AfterMethod;
            *(methodPtr + 12) = _x64MethodMemoryInfo.Byte3AfterMethod;
        }

        public void Dispose()
        {
            Return();
        }

        private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
        {
            _x64MethodMemoryInfo.Byte1 = *methodPtr;
            _x64MethodMemoryInfo.Byte2 = *(methodPtr + 1);
            _x64MethodMemoryInfo.MethodMemoryValue = *(ulong*) (methodPtr + 2);
            _x64MethodMemoryInfo.Byte1AfterMethod = *(methodPtr + 10);
            _x64MethodMemoryInfo.Byte2AfterMethod = *(methodPtr + 11);
            _x64MethodMemoryInfo.Byte3AfterMethod = *(methodPtr + 12);
        }
    }
}
