using System;
using System.Reflection;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Callback
{
    internal class CallbackService : ICallbackService
    {
        private readonly MethodInfo _originalMethodInfo;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public CallbackService(MethodInfo originalMethodInfo, IInjectionServiceFactory injectionServiceFactory)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
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
            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(methodInfoToInject);
        }
    }
}
