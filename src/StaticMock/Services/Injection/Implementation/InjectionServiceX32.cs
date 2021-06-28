using System;
using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Injection.Entities;

namespace StaticMock.Services.Injection.Implementation
{
    internal class InjectionServiceX32 : IInjectionService
    {
        private MethodMemoryInfoX32 _methodMemoryInfoX32;

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

            //push replacementSite
            *methodPtr = 0x68;
            *(uint*) (methodPtr + 1) = (uint) methodToInject.MethodHandle.GetFunctionPointer().ToInt32();

            //ret
            *(methodPtr + 5) = 0xC3;

            return this;
        }

        public unsafe void Return()
        {
            var methodPtr = (byte*) _method.MethodHandle.GetFunctionPointer().ToPointer();

            *methodPtr = _methodMemoryInfoX32.Byte1;
            *(uint*) (methodPtr + 2) = _methodMemoryInfoX32.MethodMemoryValue;
            *(methodPtr + 5) = _methodMemoryInfoX32.Byte1AfterMethod;
        }

        public void Dispose()
        {
            Return();
        }

        private unsafe void SaveMethodMemoryInfo(byte* methodPtr)
        {
            _methodMemoryInfoX32.Byte1 = *methodPtr;
            _methodMemoryInfoX32.MethodMemoryValue = *(uint*) (methodPtr + 2);
            _methodMemoryInfoX32.Byte1AfterMethod = *(methodPtr + 5);
        }
    }
}
