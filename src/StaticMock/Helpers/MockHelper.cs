using System;
using System.Reflection;
using StaticMock.Services.Hook.Implementation;

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
            var injectionServiceFactory = new HookServiceFactory();
            using var injectionService = injectionServiceFactory.CreateHookService(methodToReplace);
            injectionService.Hook(injectionMethod.Method);
            action();
        }
    }
}
