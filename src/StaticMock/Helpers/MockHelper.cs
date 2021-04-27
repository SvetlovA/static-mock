using System;
using System.Reflection;
using StaticMock.Services.Injection.Implementation;

namespace StaticMock.Helpers
{
    internal static class MockHelper
    {
        public static void SetupDefault(MethodBase methodToReplace, Action action)
        {
            if (methodToReplace == null)
            {
                throw new ArgumentNullException(nameof(methodToReplace));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Action injectionMethod = () => { };
            var injectionServiceFactory = new InjectionServiceFactory();
            using var injectionService = injectionServiceFactory.CreateInjectionService(methodToReplace);
            injectionService.Inject(injectionMethod.Method);
            action();
        }
    }
}
