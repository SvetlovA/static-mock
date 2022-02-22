using System.Linq.Expressions;
using System.Reflection;
using StaticMock.Entities;
using StaticMock.Helpers.Entities;
using StaticMock.Hooks.Entities;
using StaticMock.Hooks.Implementation;
using StaticMock.Mocks;
using StaticMock.Mocks.Implementation;

namespace StaticMock.Helpers;

internal static class SetupMockHelper
{
    public static void SetupDefault(MethodBase methodToReplace, Action action)
    {
        var injectionMethod = () => { };
        var injectionServiceFactory = new HookManagerFactory();
        using var injectionService = injectionServiceFactory.CreateHookService(methodToReplace);
        injectionService.ApplyHook(injectionMethod.Method);
        action();
    }

    public static MockSetupProperties GetMockSetupProperties<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression)
    {
        MethodInfo? originalMethodInfo = null;
        var hookParameters = Array.Empty<HookParameter>();

        if (methodGetExpression.Body is MemberExpression {Member: PropertyInfo propertyInfo})
        {
            originalMethodInfo = propertyInfo.GetMethod;
        }

        if (methodGetExpression.Body is MethodCallExpression methodExpression)
        {
            originalMethodInfo = methodExpression.Method;
            hookParameters = GetHookParameters(methodExpression).ToArray();
        }

        if (originalMethodInfo == null)
        {
            throw new Exception("Get expression not contains method nor property to setup");
        }

        return new MockSetupProperties
        {
            OriginalMethodInfo = originalMethodInfo,
            HookParameters = hookParameters
        };
    }

    public static IEnumerable<HookParameter> GetHookParameters(MethodCallExpression methodCallExpression)
    {
        foreach (var expressionArgument in methodCallExpression.Arguments)
        {
            var hookParameter = new HookParameter { Type = expressionArgument.Type };
            if (expressionArgument is MethodCallExpression argumentMethodExpression)
            {
                hookParameter.Hook = argumentMethodExpression.Method;
            }

            yield return hookParameter;
        }
    }

    public static IFuncMock SetupInternal(Type type, string methodName, Action action, SetupProperties? setupProperties = null)
    {
        var originalMethodInfo = GetOriginalMethodInfo(type, methodName, setupProperties);

        if (originalMethodInfo.ReturnType == typeof(void))
        {
            throw new Exception($"Can't use some features of this setup for void return. To Setup void method us {nameof(Mock.SetupVoid)} setup");
        }

        return new FuncMock<object>(
            originalMethodInfo,
            new HookManagerFactory(),
            GetHookParameters(originalMethodInfo),
            action);
    }

    public static IFuncMock SetupPropertyInternal(Type type, string propertyName, Action action, BindingFlags? bindingFlags = null)
    {
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

        return new FuncMock<object>(originalMethodInfo, new HookManagerFactory(), GetHookParameters(originalMethodInfo), action);
    }

    public static IVoidMock SetupVoidInternal(Type type, string methodName, Action action, SetupProperties? setupProperties = null)
    {
        var originalMethodInfo = GetOriginalMethodInfo(type, methodName, setupProperties);

        return new VoidMock(
            originalMethodInfo,
            new HookManagerFactory(),
            GetHookParameters(originalMethodInfo),
            action);
    }

    public static void SetupDefaultInternal(Type type, string methodName, Action action, SetupProperties? setupProperties = null)
    {
        var originalMethodInfo = GetOriginalMethodInfo(type, methodName, setupProperties);

        if (originalMethodInfo.ReturnType != typeof(void))
        {
            throw new Exception("Default setup supported only for void methods");
        }

        SetupDefault(originalMethodInfo, action);
    }


    private static MethodInfo GetOriginalMethodInfo(Type type, string methodName, SetupProperties? setupProperties)
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

    private static HookParameter[] GetHookParameters(MethodInfo methodInfo) =>
        methodInfo.GetParameters()
            .Select(x => new HookParameter
            {
                Type = x.ParameterType
            }).ToArray();
}