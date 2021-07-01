using System;
using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Hook;

namespace StaticMock.Services.Throws
{
    internal class ThrowsService : IThrowsService
    {
        private readonly MethodInfo _originalMethodInfo;
        private readonly IHookServiceFactory _hookServiceFactory;
        private readonly IHookBuilder _hookBuilder;

        public ThrowsService(MethodInfo originalMethodInfo, IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder)
        {
            _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
            _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
            _hookBuilder = hookBuilder ?? throw new ArgumentNullException(nameof(hookBuilder));
        }

        public IReturnable Throws(Type exceptionType)
        {
            if (exceptionType == null)
            {
                throw new ArgumentNullException(nameof(exceptionType));
            }

            var hookException = Activator.CreateInstance(exceptionType) as Exception;

            if (hookException == null)
            {
                throw new Exception($"{exceptionType.FullName} is not an Exception");
            }

            var hook = _hookBuilder.CreateThrowsHook(hookException);

            return Inject(hook);
        }

        public IReturnable Throws<TException>() where TException : Exception, new()
        {
            var hook = _hookBuilder.CreateThrowsHook(new TException());

            return Inject(hook);
        }

        private IReturnable Inject(MethodBase methodToInject)
        {
            var injectionService = _hookServiceFactory.CreateHookService(_originalMethodInfo);
            return injectionService.Hook(methodToInject);
        }
    }
}
