﻿using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using StaticMock.Entities;
using StaticMock.Entities.Context;
using StaticMock.Helpers.Entities;
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

    public static MockSetupProperties GetMockSetupProperties<TReturnValue>(
        Expression<Func<TReturnValue>> methodGetExpression)
    {
        MethodInfo? originalMethodInfo = null;
        var setupContext = new SetupContext();

        if (methodGetExpression.Body is MemberExpression { Member: PropertyInfo propertyInfo })
        {
            originalMethodInfo = propertyInfo.GetMethod;
        }

        if (methodGetExpression.Body is MethodCallExpression methodExpression)
        {
            originalMethodInfo = methodExpression.Method;
            setupContext.State.ItParameterExpressions.AddRange(
                methodExpression.Arguments.Select(x => new ItParameterExpression
                {
                    ParameterType = x.Type
                }));
        }

        if (originalMethodInfo == null)
        {
            throw new Exception("Get expression not contains method nor property to setup");
        }

        return new MockSetupProperties
        {
            OriginalMethodInfo = originalMethodInfo,
            SetupContextState = setupContext.State
        };
    }

    public static MockSetupProperties GetMockSetupProperties<TReturnValue>(
        Expression<Func<SetupContext, TReturnValue>> methodGetExpression)
    {
        MethodInfo? originalMethodInfo = null;
        var setupContext = new SetupContext();

        if (methodGetExpression.Body is MemberExpression { Member: PropertyInfo propertyInfo })
        {
            originalMethodInfo = propertyInfo.GetMethod;
        }

        if (methodGetExpression.Body is MethodCallExpression methodExpression)
        {
            originalMethodInfo = methodExpression.Method;
            foreach (var methodExpressionArgument in methodExpression.Arguments.OfType<MethodCallExpression>())
            {
                var argumentMethod = methodExpressionArgument.Method;
                if (argumentMethod.DeclaringType == typeof(It))
                {
                    argumentMethod.Invoke(
                        setupContext.It,
                        BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public,
                        null,
                        methodExpressionArgument.Arguments
                            .Cast<UnaryExpression>()
                            .Select(x => x.Operand)
                            .Cast<Expression<Func<TReturnValue, bool>>>()
                            .ToArray(),
                        CultureInfo.InvariantCulture);
                }
            }
        }

        if (originalMethodInfo == null)
        {
            throw new Exception("Get expression not contains method nor property to setup");
        }

        return new MockSetupProperties
        {
            OriginalMethodInfo = originalMethodInfo,
            SetupContextState = setupContext.State
        };
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
            new SetupContext().State,
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

        return new FuncMock<object>(
            originalMethodInfo,
            new HookManagerFactory(),
            new SetupContext().State,
            action);
    }

    public static IVoidMock SetupVoidInternal(Type type, string methodName, Action action, SetupProperties? setupProperties = null)
    {
        var originalMethodInfo = GetOriginalMethodInfo(type, methodName, setupProperties);

        return new VoidMock(
            originalMethodInfo,
            new HookManagerFactory(),
            new SetupContext().State,
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
}