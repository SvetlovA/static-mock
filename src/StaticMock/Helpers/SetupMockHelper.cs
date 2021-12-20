using System.Linq.Expressions;
using System.Reflection;
using StaticMock.Entities;
using StaticMock.Services.Hook.Implementation;
using StaticMock.Services.Mock;
using StaticMock.Services.Mock.Implementation;

namespace StaticMock.Helpers;

internal static class SetupMockHelper
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

    public static MethodInfo ValidateAndGetOriginalMethodInfo<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression)
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

    public static IFuncMockService SetupInternal(Type type, string methodName, Action action, SetupProperties setupProperties = null)
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

        var originalMethodInfo = ValidateAndGetOriginalMethodInfo(type, methodName, setupProperties);

        if (originalMethodInfo.ReturnType == typeof(void))
        {
            throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(Mock.SetupVoid)} setup");
        }

        return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
    }

    public static IFuncMockService SetupPropertyInternal(Type type, string propertyName, Action action, BindingFlags? bindingFlags = null)
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

        var originalPropertyInfo = bindingFlags.HasValue ? type.GetProperty(propertyName, bindingFlags.Value) : type.GetProperty(propertyName);
        if (originalPropertyInfo == null)
        {
            throw new Exception($"Can't find property {propertyName} of type {type.FullName}");
        }

        var originalMethodInfo = originalPropertyInfo.GetMethod;
        if (originalMethodInfo.ReturnType == typeof(void))
        {
            throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(Mock.SetupVoid)} setup");
        }

        return new FuncMockService<object>(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
    }

    public static IVoidMockService SetupVoidInternal(Type type, string methodName, Action action, SetupProperties setupProperties = null)
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

        var originalMethodInfo = ValidateAndGetOriginalMethodInfo(type, methodName, setupProperties);

        return new VoidMockService(new HookServiceFactory(), new HookBuilder(), originalMethodInfo, action);
    }

    public static void SetupDefaultInternal(Type type, string methodName, Action action, SetupProperties setupProperties = null)
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

        var originalMethodInfo = ValidateAndGetOriginalMethodInfo(type, methodName, setupProperties);

        if (originalMethodInfo.ReturnType != typeof(void))
        {
            throw new Exception("Default setup supported only for void methods");
        }

        SetupDefault(originalMethodInfo, action);
    }


    private static MethodInfo ValidateAndGetOriginalMethodInfo(Type type, string methodName, SetupProperties setupProperties)
    {
        var bindingFlags = setupProperties?.BindingFlags;
        var originalMethodInfo = bindingFlags.HasValue ? type.GetMethod(methodName, bindingFlags.Value) : type.GetMethod(methodName);

        if (originalMethodInfo == null)
        {
            throw new Exception($"Can't find method {methodName} of type {type.FullName}");
        }

        if (!originalMethodInfo.IsGenericMethodDefinition)
        {
            return originalMethodInfo;
        }

        var genericTypes = setupProperties?.GenericTypes;

        if (genericTypes == null)
        {
            throw new ArgumentNullException(nameof(SetupProperties.GenericTypes));
        }

        var genericArguments = originalMethodInfo.GetGenericArguments();
        if (genericTypes.Length != genericArguments.Length)
        {
            throw new Exception(
                $"Length and order of {nameof(SetupProperties.GenericTypes)} must be like count and order of generics in setup method");
        }

        originalMethodInfo = originalMethodInfo.MakeGenericMethod(genericTypes);

        return originalMethodInfo;
    }
}