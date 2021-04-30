using System;
using System.Reflection;
using StaticMock.Services.Callback;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Mock.Implementation
{
    internal class VoidMockService : MockService, IVoidMockService
    {
        private readonly MethodInfo _originalMethodInfo;
        private readonly Action _action;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public VoidMockService(IInjectionServiceFactory injectionServiceFactory, MethodInfo originalMethodInfo, Action action)
            : base(injectionServiceFactory, originalMethodInfo, action)
        {
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Callback(Action callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var callbackService = new CallbackService(_originalMethodInfo, _injectionServiceFactory);
            using (callbackService.Callback(callback))
            {
                _action();
            }
        }
    }
}
