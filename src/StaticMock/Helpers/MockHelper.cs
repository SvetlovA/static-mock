using System;
using System.Linq.Expressions;
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

        public static MethodInfo ValidateAndGetOriginalMethod<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression)
        {
            if (methodGetExpression == null)
            {
                throw new ArgumentNullException(nameof(methodGetExpression));
            }

            MethodInfo originalMethodInfo = null;

            if (methodGetExpression.Body is MemberExpression {Member: PropertyInfo propertyInfo})
            {
                originalMethodInfo = propertyInfo.GetMethod;
            }

            if (methodGetExpression.Body is MethodCallExpression methodExpression)
            {
                originalMethodInfo = methodExpression.Method;
            }

            if (originalMethodInfo == null)
            {
                throw new Exception("Get expression not contains method nor property to setup");
            }

            return originalMethodInfo;
        }
    }
}
