using System;
using System.Reflection;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Returns.Value
{
    internal class ValueReturnsMockService<TValue> : IValueReturnsMockService<TValue>
    {
        private static TValue _injectionValue;

        private readonly MethodInfo _originalMethodInfo;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public ValueReturnsMockService(MethodInfo originalMethodInfo, IInjectionServiceFactory injectionServiceFactory)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
        }

        public IReturnable Returns(TValue value)
        {
            _injectionValue = value;
            Func<TValue> injectionMethod = InjectionMethod;

            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(injectionMethod.Method);
        }

        private static TValue InjectionMethod() => _injectionValue;
    }
}
