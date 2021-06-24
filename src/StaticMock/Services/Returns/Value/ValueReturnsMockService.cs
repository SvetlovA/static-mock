using System;
using System.Collections.Concurrent;
using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Returns.Value
{
    internal class ValueReturnsMockService<TValue> : IValueReturnsMockService<TValue>
    {
        private static readonly ConcurrentDictionary<object, TValue> PreviousInjectionValues = new ConcurrentDictionary<object, TValue>();
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
            PreviousInjectionValues[this] = _injectionValue;
            _injectionValue = value;

            Func<TValue> injectionMethod = () => _injectionValue;

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

            throw new Exception($"{nameof(ValueReturnsMockService<TValue>)} previous injection value isn't exists");
        }
    }
}
