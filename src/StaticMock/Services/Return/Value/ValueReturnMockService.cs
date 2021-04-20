using System;
using System.Reflection;
using StaticMock.Helpers;

namespace StaticMock.Services.Return.Value
{
    internal class ValueReturnMockService<TValue> : IValueReturnMockService<TValue>
    {
        private static TValue _injectionValue;

        private readonly MethodInfo _originalMethodInfo;

        public ValueReturnMockService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        }

        public void Returns(TValue value)
        {
            _injectionValue = value;
            Func<TValue> injectionMethod = () => _injectionValue;

            CodeInjectionHelper.Inject(_originalMethodInfo, injectionMethod.Method);
        }
    }
}
