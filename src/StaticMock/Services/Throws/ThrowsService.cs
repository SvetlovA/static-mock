using System;
using System.Collections.Concurrent;
using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Throws
{
    internal class ThrowsService : IThrowsService
    {
        private static readonly ConcurrentDictionary<object, Exception> PreviousInjectionExceptions = new ConcurrentDictionary<object, Exception>();
        private static Exception _injectionException;

        private readonly MethodInfo _originalMethodInfo;
        private readonly IInjectionServiceFactory _injectionServiceFactory;

        public ThrowsService(MethodInfo originalMethodInfo, IInjectionServiceFactory injectionServiceFactory)
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

            PreviousInjectionExceptions[this] = _injectionException;
            _injectionException = Activator.CreateInstance(exceptionType) as Exception;

            if (_injectionException == null)
            {
                throw new Exception($"{exceptionType.FullName} is not an Exception");
            }

            Action injectionMethod = () => throw _injectionException;

            return Inject(injectionMethod.Method);
        }

        public IReturnable Throws<TException>() where TException : Exception, new()
        {
            PreviousInjectionExceptions[this] = _injectionException;
            _injectionException = new TException();

            Action injectionMethod = () => throw _injectionException;

            return Inject(injectionMethod.Method);
        }

        private IReturnable Inject(MethodBase methodToInject)
        {
            var injectionService = _injectionServiceFactory.CreateInjectionService(_originalMethodInfo);
            return injectionService.Inject(methodToInject);
        }

        public void Dispose()
        {
            Return();
        }

        public void Return()
        {
            if (PreviousInjectionExceptions.TryRemove(this, out var previousInjectionException))
            {
                _injectionException = previousInjectionException;
                return;
            }

            throw new Exception($"{nameof(ThrowsService)} previous injection value isn't exists");
        }
    }
}
