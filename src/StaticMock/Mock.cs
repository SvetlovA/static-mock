using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StaticMock.Helpers;
using StaticMock.Services.Hook.Implementation;
using StaticMock.Services.Mock;
using StaticMock.Services.Mock.Implementation;

namespace StaticMock
{
    public static class Mock
    {
        public static IFuncMockService Setup(Type type, string methodName, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalMethodInfo = type.GetMethod(methodName);

            if (originalMethodInfo == null)
            {
                throw new Exception($"Can't find method {methodName} of type {type.FullName}");
            }

            if (originalMethodInfo.ReturnType == typeof(void))
            {
                throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(SetupVoid)} setup");
            }

            return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
        }

        public static IFuncMockService SetupProperty(Type type, string propertyName, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalPropertyInfo = type.GetProperty(propertyName);
            if (originalPropertyInfo == null)
            {
                throw new Exception($"Can't find property {propertyName} of type {type.FullName}");
            }

            var originalMethodInfo = originalPropertyInfo.GetMethod;
            if (originalMethodInfo.ReturnType == typeof(void))
            {
                throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(SetupVoid)} setup");
            }

            return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
        }

        public static IVoidMockService SetupVoid(Type type, string methodName, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalMethodInfo = type.GetMethod(methodName);

            if (originalMethodInfo == null)
            {
                throw new Exception($"Can't find methodGetExpression {methodName} of type {type.FullName}");
            }

            return new VoidMockService(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
        }

        public static void SetupDefault(Type type, string methodName, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalMethodInfo = type.GetMethod(methodName);

            if (originalMethodInfo == null)
            {
                throw new Exception($"Can't find methodGetExpression {methodName} of type {type.FullName}");
            }

            if (originalMethodInfo.ReturnType != typeof(void))
            {
                throw new Exception("Default setup supported only for void methods");
            }

            MockHelper.SetupDefault(originalMethodInfo, action);
        }

        public static IFuncMockService<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalMethodInfo = MockHelper.ValidateAndGetOriginalMethod(methodGetExpression);
            return new FuncMockService<TReturnValue>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
        }

        public static IAsyncFuncMockService<TReturnValue> Setup<TReturnValue>(Expression<Func<Task<TReturnValue>>> methodGetExpression, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalMethodInfo = MockHelper.ValidateAndGetOriginalMethod(methodGetExpression);
            return new AsyncFuncMockService<TReturnValue>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
        }

        public static IVoidMockService Setup(Expression<Action> methodGetExpression, Action action)
        {
            if (methodGetExpression == null)
            {
                throw new ArgumentNullException(nameof(methodGetExpression));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!(methodGetExpression.Body is MethodCallExpression methodExpression))
            {
                throw new Exception("Get expression not contains method to setup");
            }

            return new VoidMockService(new HookServiceFactory(), new HookBuilder(), methodExpression.Method, action);
        }

        public static void SetupDefault(Expression<Action> methodGetExpression, Action action)
        {
            if (methodGetExpression == null)
            {
                throw new ArgumentNullException(nameof(methodGetExpression));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (!(methodGetExpression.Body is MethodCallExpression methodExpression))
            {
                throw new Exception("Get expression not contains method to setup");
            }

            var originalMethodInfo = methodExpression.Method;
            if (originalMethodInfo.ReturnType != typeof(void))
            {
                throw new Exception("Default setup supported only for void methods");
            }

            MockHelper.SetupDefault(methodExpression.Method, action);
        }
    }
}
