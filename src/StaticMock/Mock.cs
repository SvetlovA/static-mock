using System.Threading.Tasks;
using StaticMock.Helpers;
using StaticMock.Services.Hook.Implementation;
using StaticMock.Services.Mock;
using StaticMock.Services.Mock.Implementation;
using System;
using System.Linq.Expressions;
using System.Reflection;
using StaticMock.Entities;

namespace StaticMock
{
    public static class Mock
    {
        public static IFuncMockService Setup(Type type, string methodName, Action action) =>
            SetupMockHelper.SetupInternal(type, methodName, action);
        public static IFuncMockService Setup(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
            SetupMockHelper.SetupInternal(type, methodName, action, new SetupProperties
            {
                BindingFlags = bindingFlags
            });
        public static IFuncMockService Setup(Type type, string methodName, SetupProperties setupProperties, Action action) =>
            SetupMockHelper.SetupInternal(type, methodName, action, setupProperties);

        public static IFuncMockService SetupProperty(Type type, string propertyName, Action action) =>
            SetupMockHelper.SetupPropertyInternal(type, propertyName, action);
        public static IFuncMockService SetupProperty(Type type, string propertyName, BindingFlags bindingFlags, Action action) =>
            SetupMockHelper.SetupPropertyInternal(type, propertyName, action, bindingFlags);

        public static IVoidMockService SetupVoid(Type type, string methodName, Action action) =>
            SetupMockHelper.SetupVoidInternal(type, methodName, action);
        public static IVoidMockService SetupVoid(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
            SetupMockHelper.SetupVoidInternal(type, methodName, action, new SetupProperties
            {
                BindingFlags = bindingFlags
            });
        public static IVoidMockService SetupVoid(Type type, string methodName, SetupProperties setupProperties, Action action) =>
            SetupMockHelper.SetupVoidInternal(type, methodName, action, setupProperties);

        public static void SetupDefault(Type type, string methodName, Action action) =>
            SetupMockHelper.SetupDefaultInternal(type, methodName, action);
        public static void SetupDefault(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
            SetupMockHelper.SetupDefaultInternal(type, methodName, action, new SetupProperties
            {
                BindingFlags = bindingFlags
            });
        public static void SetupDefault(Type type, string methodName, SetupProperties setupProperties, Action action) =>
            SetupMockHelper.SetupDefaultInternal(type, methodName, action, setupProperties);

        public static IFuncMockService<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalMethodInfo = SetupMockHelper.ValidateAndGetOriginalMethodInfo(methodGetExpression);
            return new FuncMockService<TReturnValue>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
        }

        public static IAsyncFuncMockService<TReturnValue> Setup<TReturnValue>(Expression<Func<Task<TReturnValue>>> methodGetExpression, Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var originalMethodInfo = SetupMockHelper.ValidateAndGetOriginalMethodInfo(methodGetExpression);
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

            SetupMockHelper.SetupDefault(methodExpression.Method, action);
        }
    }
}
