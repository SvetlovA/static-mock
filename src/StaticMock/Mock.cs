using StaticMock.Helpers;
using StaticMock.Services.Hook.Implementation;
using StaticMock.Services.Mock;
using StaticMock.Services.Mock.Implementation;
using System;
using System.Linq.Expressions;
using System.Reflection;

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

            var methodToReplace = type.GetMethod(methodName);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find method {methodName} of type {type.FullName}");
            }

            if (methodToReplace.ReturnType == typeof(void))
            {
                throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(SetupVoid)} setup");
            }

            return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), methodToReplace, action);
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

            var propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new Exception($"Can't find property {propertyName} of type {type.FullName}");
            }

            var methodToReplace = propertyInfo.GetMethod;
            if (methodToReplace.ReturnType == typeof(void))
            {
                throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(SetupVoid)} setup");
            }

            return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), methodToReplace, action);
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

            var methodToReplace = type.GetMethod(methodName);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find methodGetExpression {methodName} of type {type.FullName}");
            }

            return new VoidMockService(new HookServiceFactory(), new HookBuilder(), methodToReplace, action);
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

            var methodToReplace = type.GetMethod(methodName);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find methodGetExpression {methodName} of type {type.FullName}");
            }

            if (methodToReplace.ReturnType != typeof(void))
            {
                throw new Exception("Default setup supported only for void methods");
            }

            MockHelper.SetupDefault(methodToReplace, action);
        }

        public static IFuncMockService Setup(Type type, string methodName, BindingFlags bindingFlags, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            string[] values = bindingFlags.ToString().Split(new[] { ", " }, StringSplitOptions.None);
            foreach (string value in values)
            {
                if (!Enum.IsDefined(typeof(BindingFlags), value))
                {
                    throw new ArgumentException(nameof(bindingFlags));
                }
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var methodToReplace = type.GetMethod(methodName, bindingFlags);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find method {methodName} of type {type.FullName}");
            }

            if (methodToReplace.ReturnType == typeof(void))
            {
                throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(SetupVoid)} setup");
            }

            return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), methodToReplace, action);
        }

        public static IFuncMockService SetupProperty(Type type, string propertyName, BindingFlags bindingFlags, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            string[] values = bindingFlags.ToString().Split(new[] { ", " }, StringSplitOptions.None);
            foreach (string value in values)
            {
                if (!Enum.IsDefined(typeof(BindingFlags), value))
                {
                    throw new ArgumentException(nameof(bindingFlags));
                }
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var propertyInfo = type.GetProperty(propertyName, bindingFlags);
            if (propertyInfo == null)
            {
                throw new Exception($"Can't find property {propertyName} of type {type.FullName}");
            }

            var methodToReplace = propertyInfo.GetMethod;
            if (methodToReplace.ReturnType == typeof(void))
            {
                throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(SetupVoid)} setup");
            }

            return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), methodToReplace, action);
        }

        public static IVoidMockService SetupVoid(Type type, string methodName, BindingFlags bindingFlags, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            string[] values = bindingFlags.ToString().Split(new[] { ", " }, StringSplitOptions.None);
            foreach (string value in values)
            {
                if (!Enum.IsDefined(typeof(BindingFlags), value))
                {
                    throw new ArgumentException(nameof(bindingFlags));
                }
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var methodToReplace = type.GetMethod(methodName, bindingFlags);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find methodGetExpression {methodName} of type {type.FullName}");
            }

            return new VoidMockService(new HookServiceFactory(), new HookBuilder(), methodToReplace, action);
        }

        public static void SetupDefault(Type type, string methodName, BindingFlags bindingFlags, Action action)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (methodName == null)
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            string[] values = bindingFlags.ToString().Split(new[] { ", " }, StringSplitOptions.None);
            foreach (string value in values)
            {
                if (!Enum.IsDefined(typeof(BindingFlags), value))
                {
                    throw new ArgumentException(nameof(bindingFlags));
                }
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var methodToReplace = type.GetMethod(methodName, bindingFlags);

            if (methodToReplace == null)
            {
                throw new Exception($"Can't find methodGetExpression {methodName} of type {type.FullName}");
            }

            if (methodToReplace.ReturnType != typeof(void))
            {
                throw new Exception("Default setup supported only for void methods");
            }

            MockHelper.SetupDefault(methodToReplace, action);
        }

        public static IFuncMockService<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression, Action action)
        {
            if (methodGetExpression == null)
            {
                throw new ArgumentNullException(nameof(methodGetExpression));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            MethodInfo originalMethodInfo = null;

            if (methodGetExpression.Body is MemberExpression { Member: PropertyInfo propertyInfo })
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

            return new FuncMockService<TReturnValue>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
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

            var methodToReplace = methodExpression.Method;
            if (methodToReplace.ReturnType != typeof(void))
            {
                throw new Exception("Default setup supported only for void methods");
            }

            MockHelper.SetupDefault(methodExpression.Method, action);
        }
    }
}
