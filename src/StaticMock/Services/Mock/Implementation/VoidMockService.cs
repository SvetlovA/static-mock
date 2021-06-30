using System;
using System.Reflection;
using StaticMock.Services.Callback;
using StaticMock.Services.Hook;

namespace StaticMock.Services.Mock.Implementation
{
    internal class VoidMockService : MockService, IVoidMockService
    {
        private readonly MethodInfo _originalMethodInfo;
        private readonly Action _action;
        private readonly IHookServiceFactory _hookServiceFactory;

        public VoidMockService(IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder, MethodInfo originalMethodInfo, Action action)
            : base(hookServiceFactory, hookBuilder, originalMethodInfo, action)
        {
            _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Callback(Action callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var callbackService = new CallbackService(_originalMethodInfo, _hookServiceFactory);
            using (callbackService.Callback(callback))
            {
                _action();
            }
        }
    }
}
