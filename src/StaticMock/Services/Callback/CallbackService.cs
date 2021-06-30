using System;
using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Hook;

namespace StaticMock.Services.Callback
{
    internal class CallbackService : ICallbackService
    {
        private readonly MethodInfo _originalMethodInfo;
        private readonly IHookServiceFactory _hookServiceFactory;

        public CallbackService(MethodInfo originalMethodInfo, IHookServiceFactory hookServiceFactory)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
        }

        public IReturnable Callback(Action callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return Inject(callback.Method);
        }

        public IReturnable Callback<TReturn>(Func<TReturn> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return Inject(callback.Method);
        }

        private IReturnable Inject(MethodBase methodInfoToInject)
        {
            var injectionService = _hookServiceFactory.CreateHookService(_originalMethodInfo);
            return injectionService.Hook(methodInfoToInject);
        }
    }
}
