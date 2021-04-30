using System;
using System.Reflection;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Returns.Reference
{
    internal class ReferenceReturnsMockService : IReferenceReturnsMockService
    {
        private static object _injectionValue;

        private readonly MethodInfo _originalMethodInfo;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public ReferenceReturnsMockService(MethodInfo originalMethodInfo, IInjectionServiceFactory injectionServiceFactory)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
        }

        public IReturnable Returns(object value)
        {
            _injectionValue = value;
            Func<object> injectionMethod = InjectionMethod;

            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(injectionMethod.Method);
        }

        private static object InjectionMethod() => _injectionValue;
    }
}
