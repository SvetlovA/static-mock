using System;
using System.Reflection;
using StaticMock.Helpers;

namespace StaticMock.Services.Return.Reference
{
    internal class ReferenceReturnMockService : IReferenceReturnMockService
    {
        private static object _injectionValue;

        private readonly MethodInfo _originalMethodInfo;

        public ReferenceReturnMockService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        }

        public void Returns(object value)
        {
            _injectionValue = value;
            Func<object> injectionMethod = () => _injectionValue;

            CodeInjectionHelper.Inject(_originalMethodInfo, injectionMethod.Method);
        }
    }
}
