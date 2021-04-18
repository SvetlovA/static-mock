using System;
using System.Reflection;
using StaticMock.Helpers;

namespace StaticMock.Services.Return
{
    internal class ReturnReferenceMockService : IReturnReferenceMockService
    {
        private static object _injectedReturnValue;

        private readonly MethodInfo _originalMethodInfo;

        public ReturnReferenceMockService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        }

        public void Returns(object value)
        {
            _injectedReturnValue = value;
            Func<object> injectionMethod = () => _injectedReturnValue;

            CodeInjectionHelper.Inject(_originalMethodInfo, injectionMethod.Method);
        }
    }
}
