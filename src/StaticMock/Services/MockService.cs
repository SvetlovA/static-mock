using System;
using System.Reflection;
using StaticMock.Helpers;

namespace StaticMock.Services
{
    internal class MockService : IMockService
    {
        private static object _injectedReturnValue;

        private readonly MethodInfo _originalMethodInfo;

        public MockService(MethodInfo originalMethod)
        {
            _originalMethodInfo = originalMethod ?? throw new ArgumentNullException(nameof(originalMethod));
        }

        public void Return(object value)
        {
            if (_originalMethodInfo.ReturnType == typeof(void))
            {
                throw new Exception("Original method must be function with return");
            }

            _injectedReturnValue = value ?? throw new ArgumentNullException(nameof(value));
            Func<object> injectMethod = () => _injectedReturnValue;

            CodeInjectionHelper.Inject(_originalMethodInfo, injectMethod.Method);
        }
    }
}
