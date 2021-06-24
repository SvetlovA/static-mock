using System;
using System.Reflection;
using StaticMock.Services.Callback;
using StaticMock.Services.Injection;
using StaticMock.Services.Returns.Reference;
using StaticMock.Services.Returns.Value;

namespace StaticMock.Services.Mock.Implementation
{
    internal class FuncMockService<TReturn> : MockService, IFuncMockService, IFuncMockService<TReturn>
    {
        private readonly IInjectionServiceFactory _injectionServiceFactory;
        private readonly MethodInfo _originalMethodInfo;
        private readonly Action _action;

        public FuncMockService(IInjectionServiceFactory injectionServiceFactory, MethodInfo originalMethodInfo, Action action)
            : base(injectionServiceFactory, originalMethodInfo, action)
        {
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
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

            var callbackService = new CallbackService(_originalMethodInfo, _injectionServiceFactory);
            using (callbackService.Callback(callback))
            {
                _action();
            }
        }

        public void Returns<TValue>(TValue value)
        {
            if (typeof(TValue).IsValueType)
            {
                using var valueReturnService = new ValueReturnsMockService<TValue>(_originalMethodInfo, _injectionServiceFactory);
                using (valueReturnService.Returns(value))
                {
                    _action();
                }

                return;
            }

            using var referenceReturnService = new ReferenceReturnsMockService(_originalMethodInfo, _injectionServiceFactory);
            using (referenceReturnService.Returns(value))
            {
                _action();
            }
        }
    }
}
