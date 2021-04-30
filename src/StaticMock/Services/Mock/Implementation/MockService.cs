using System;
using System.Reflection;
using StaticMock.Services.Injection;
using StaticMock.Services.Throws;

namespace StaticMock.Services.Mock.Implementation
{
    internal class MockService : IMockService
    {
        private readonly MethodInfo _originalMethodInfo;
        private readonly Action _action;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public MockService(IInjectionServiceFactory injectionServiceFactory, MethodInfo originalMethodInfo, Action action)
        {
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }


        public void Throws(Type exceptionType)
        {
            var throwService = new ThrowsService(_originalMethodInfo, _injectionServiceFactory);
            using (throwService.Throws(exceptionType))
            {
                _action();
            }
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            var throwService = new ThrowsService(_originalMethodInfo, _injectionServiceFactory);
            using (throwService.Throws<TException>())
            {
                _action();
            }
        }
    }
}
