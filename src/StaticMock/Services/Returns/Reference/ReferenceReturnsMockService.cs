using System;
using System.Collections.Concurrent;
using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Returns.Reference
{
    internal class ReferenceReturnsMockService : IReferenceReturnsMockService
    {
        private static readonly ConcurrentDictionary<object, object> PreviousInjectionValues = new ConcurrentDictionary<object, object>();
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
            PreviousInjectionValues[this] = _injectionValue;
            _injectionValue = value;

            Func<object> injectionMethod = () => _injectionValue;

            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(injectionMethod.Method);
        }

        public void Dispose()
        {
            Return();
        }

        public void Return()
        {
            if (PreviousInjectionValues.TryRemove(this, out var previousInjectionValue))
            {
                _injectionValue = previousInjectionValue;
                return;
            }

            throw new Exception($"{nameof(ReferenceReturnsMockService)} previous injection value isn't exists");
        }
    }
}
