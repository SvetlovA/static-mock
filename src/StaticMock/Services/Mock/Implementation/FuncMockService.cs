using System;
using System.Linq;
using System.Reflection;
using StaticMock.Services.Callback;
using StaticMock.Services.Hook;
using StaticMock.Services.Returns;

namespace StaticMock.Services.Mock.Implementation
{
    internal class FuncMockService<TReturn> : MockService, IFuncMockService, IFuncMockService<TReturn>
    {
        private readonly IHookServiceFactory _hookServiceFactory;
        private readonly IHookBuilder _hookBuilder;
        private readonly MethodInfo _originalMethodInfo;
        private readonly Action _action;

        public FuncMockService(IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder, MethodInfo originalMethodInfo, Action action)
            : base(hookServiceFactory, hookBuilder, originalMethodInfo, action)
        {
            _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
            _hookBuilder = hookBuilder ?? throw new ArgumentNullException(nameof(hookBuilder));
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Callback(Func<TReturn> callback)
        {
            Callback<TReturn>(callback);
        }

        public void Returns(TReturn value)
        {
            Returns<TReturn>(value);
        }

        public void Callback<TReturnValue>(Func<TReturnValue> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var callbackService = new CallbackService(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
            using (callbackService.Callback(callback))
            {
                _action();
            }
        }

        public void Returns<TReturnValue>(TReturnValue value)
        {
            var returnService = new ReturnsMockService<TReturnValue>(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
            using (returnService.Returns(value))
            {
                _action();
            }
        }

        public void ReturnsAsync<TReturnValue>(TReturnValue value)
        {
            var returnService = new ReturnsMockService<TReturnValue>(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
            using (returnService.ReturnsAsync(value))
            {
                _action();
            }
        }
    }
}
