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
        private readonly IInjectionService _injectionService;

        public ValueReturnMockService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionService = new InjectionServiceX64(originalMethodInfo);
        }

        public IReturnable Returns(TValue value)
        {
            InjectionValuesByMethod[_originalMethodInfo] = value;
            _injectionValue = InjectionValuesByMethod[_originalMethodInfo];
            Func<TValue> injectionMethod = InjectionMethod;

            return _injectionService.Inject(injectionMethod.Method);
        }

        private static TValue InjectionMethod() => _injectionValue;
    }
}
