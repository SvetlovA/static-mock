using System;
using System.Collections.Concurrent;
using System.Reflection;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Return.Value
{
    internal class ValueReturnMockService<TValue> : IValueReturnMockService<TValue>
    {
        private static TValue _injectionValue;
        private static readonly ConcurrentDictionary<MethodInfo, TValue> InjectionValuesByMethod = new ConcurrentDictionary<MethodInfo, TValue>();

        private readonly MethodInfo _originalMethodInfo;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public ValueReturnMockService(MethodInfo originalMethodInfo, IInjectionServiceFactory injectionServiceFactory)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
        }

        public IReturnable Returns(TValue value)
        {
            InjectionValuesByMethod[_originalMethodInfo] = value;
            _injectionValue = InjectionValuesByMethod[_originalMethodInfo];
            Func<TValue> injectionMethod = InjectionMethod;

            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(injectionMethod.Method);
        }

        private static TValue InjectionMethod() => _injectionValue;
    }
}
