using System;
using System.Collections.Concurrent;
using System.Reflection;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Return.Reference
{
    internal class ReferenceReturnMockService : IReferenceReturnMockService
    {
        private static object _injectionValue;
        private static readonly ConcurrentDictionary<MethodInfo, object> InjectionValuesByMethod = new ConcurrentDictionary<MethodInfo, object>();

        private readonly MethodInfo _originalMethodInfo;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public ReferenceReturnMockService(MethodInfo originalMethodInfo, IInjectionServiceFactory injectionServiceFactory)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
        }

        public IReturnable Returns(object value)
        {
            InjectionValuesByMethod[_originalMethodInfo] = value;
            _injectionValue = InjectionValuesByMethod[_originalMethodInfo];
            Func<object> injectionMethod = InjectionMethod;

            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(injectionMethod.Method);
        }

        private static object InjectionMethod() => _injectionValue;
    }
}
