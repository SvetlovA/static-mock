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
        private readonly IInjectionService _injectionService;

        public ReferenceReturnMockService(MethodInfo originalMethodInfo)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionService = new InjectionServiceX64(_originalMethodInfo);
        }

        public IReturnable Returns(object value)
        {
            InjectionValuesByMethod[_originalMethodInfo] = value;
            _injectionValue = InjectionValuesByMethod[_originalMethodInfo];
            Func<object> injectionMethod = InjectionMethod;

            return _injectionService.Inject(injectionMethod.Method);
        }

        private static object InjectionMethod() => _injectionValue;
    }
}
