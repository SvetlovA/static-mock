using System;
using System.Reflection;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Throw
{
    internal class ThrowService : IThrowService
    {
        private static Exception _injectedException;

        private readonly MethodInfo _originalMethodInfo;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public ThrowService(MethodInfo originalMethodInfo, IInjectionServiceFactory injectionServiceFactory)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _injectionServiceFactory = injectionServiceFactory ?? throw new ArgumentNullException(nameof(injectionServiceFactory));
        }

        public IReturnable Throws(Type exceptionType)
        {
            if (exceptionType == null)
            {
                throw new ArgumentNullException(nameof(exceptionType));
            }

            _injectedException = Activator.CreateInstance(exceptionType) as Exception;

            if (_injectedException == null)
            {
                throw new Exception($"{exceptionType.FullName} is not an Exception");
            }

            Action injectionMethod = () => throw _injectedException;

            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(injectionMethod.Method);
        }

        public IReturnable Throws<TException>() where TException : Exception, new()
        {
            _injectedException = new TException();

            Action injectionMethod = () => throw _injectedException;

            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(injectionMethod.Method);
        }
    }
}
