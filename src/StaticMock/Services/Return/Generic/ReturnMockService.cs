using System;
using System.Reflection;
using StaticMock.Helpers;

namespace StaticMock.Services.Return.Generic
{
    internal unsafe class ReturnMockService<TValue> : IReturnMockService<TValue> where TValue : unmanaged
    {
        private static TValue* _injectionValue;

        private readonly MethodInfo _originalMethodInfo;

        public ReturnMockService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        }

        public unsafe void Returns(TValue value)
        {
            _injectionValue = &value;
            //var injectionMethodInfo = typeof(InjectionMethods).GetMethod(nameof(InjectionMethods.ReturnInjectionMethod));
            //var methodInfoByHandler = MethodBase.GetMethodFromHandle(injectionMethodInfo.MethodHandle, GetType().TypeHandle);

            Func<TValue> injectionMethod = () => *_injectionValue;

            CodeInjectionHelper.Inject(_originalMethodInfo, injectionMethod.Method);
        }
    }
}
