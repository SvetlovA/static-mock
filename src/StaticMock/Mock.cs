using StaticMock.Helpers;
using System.Linq.Expressions;
using System.Reflection;
using StaticMock.Entities;
using StaticMock.Entities.Context;
using StaticMock.Entities.Enums;
using StaticMock.Hooks.Entities;
using StaticMock.Hooks.HookBuilders.Implementation;
using StaticMock.Hooks.Implementation;
using StaticMock.Mocks;
using StaticMock.Mocks.Implementation;

namespace StaticMock;

public static class Mock
{
    public static GlobalSettings GlobalSettings { get; } = new();

    public static void SetHookManagerType(HookManagerType hookManagerType) => GlobalSettings.HookManagerType = hookManagerType;

    public static IFuncMock Setup(Type type, string methodName, Action action) => SetupMockHelper.SetupInternal(type, methodName, action);

    public static IFuncMock Setup(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupInternal(type, methodName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static IFuncMock Setup(Type type, string methodName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupInternal(type, methodName, action, setupProperties);

    public static IFuncMock SetupProperty(Type type, string propertyName, Action action) => SetupMockHelper.SetupPropertyInternal(type, propertyName, action);

    public static IFuncMock SetupProperty(Type type, string propertyName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupPropertyInternal(type, propertyName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static IFuncMock SetupProperty(Type type, string propertyName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupPropertyInternal(type, propertyName, action, setupProperties);

    public static IActionMock SetupAction(Type type, string methodName, Action action) => SetupMockHelper.SetupVoidInternal(type, methodName, action);

    public static IActionMock SetupAction(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupVoidInternal(type, methodName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static IActionMock SetupAction(Type type, string methodName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupVoidInternal(type, methodName, action, setupProperties);

    public static void SetupDefault(Type type, string methodName, Action action) => SetupMockHelper.SetupDefaultInternal(type, methodName, action);

    public static void SetupDefault(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupDefaultInternal(type, methodName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static void SetupDefault(Type type, string methodName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupDefaultInternal(type, methodName, action, setupProperties);

    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression, Action action)
    {
        var mockSetupProperties = SetupMockHelper.GetMockSetupProperties(methodGetExpression);
        var hookSettings = new HookSettings
        {
            HookManagerType = GlobalSettings.HookManagerType,
            ItParameterExpressions = mockSetupProperties.SetupContextState.ItParameterExpressions,
            OriginalMethodCallInstance = mockSetupProperties.OriginalMethodCallInstance
        };

        return new FuncMock<TReturnValue>(
            new HookBuilderFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookBuilder(),
            new HookManagerFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookManager(),
            action);
    }

    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<Task<TReturnValue>>> methodGetExpression, Action action)
    {
        var mockSetupProperties = SetupMockHelper.GetMockSetupProperties(methodGetExpression);
        var hookSettings = new HookSettings
        {
            HookManagerType = GlobalSettings.HookManagerType,
            ItParameterExpressions = mockSetupProperties.SetupContextState.ItParameterExpressions,
            OriginalMethodCallInstance = mockSetupProperties.OriginalMethodCallInstance
        };

        return new AsyncFuncMock<TReturnValue>(
            new HookBuilderFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookBuilder(),
            new HookManagerFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookManager(),
            action);
    }

    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, TReturnValue>> methodGetExpression, Action action)
    {
        var mockSetupProperties = SetupMockHelper.GetMockSetupProperties(methodGetExpression);
        var hookSettings = new HookSettings
        {
            HookManagerType = GlobalSettings.HookManagerType,
            ItParameterExpressions = mockSetupProperties.SetupContextState.ItParameterExpressions,
            OriginalMethodCallInstance = mockSetupProperties.OriginalMethodCallInstance
        };

        return new FuncMock<TReturnValue>(
            new HookBuilderFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookBuilder(),
            new HookManagerFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookManager(),
            action);
    }

    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, Task<TReturnValue>>> methodGetExpression, Action action)
    {
        var mockSetupProperties = SetupMockHelper.GetMockSetupProperties(methodGetExpression);
        var hookSettings = new HookSettings
        {
            HookManagerType = GlobalSettings.HookManagerType,
            ItParameterExpressions = mockSetupProperties.SetupContextState.ItParameterExpressions,
            OriginalMethodCallInstance = mockSetupProperties.OriginalMethodCallInstance
        };

        return new AsyncFuncMock<TReturnValue>(
            new HookBuilderFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookBuilder(),
            new HookManagerFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookManager(),
            action);
    }

    public static IActionMock Setup(Expression<Action> methodGetExpression, Action action)
    {
        if (!(methodGetExpression.Body is MethodCallExpression methodExpression))
        {
            throw new Exception("Get expression not contains method to setup");
        }

        var context = new SetupContext();
        var hookSettings = new HookSettings
        {
            HookManagerType = GlobalSettings.HookManagerType,
            ItParameterExpressions = context.State.ItParameterExpressions,
            OriginalMethodCallInstance = SetupMockHelper.GetOriginalMethodCallInstance(methodExpression)
        };

        return new ActionMock(
            new HookBuilderFactory(methodExpression.Method, hookSettings).CreateHookBuilder(),
            new HookManagerFactory(methodExpression.Method, hookSettings).CreateHookManager(),
            action);
    }

    public static IActionMock Setup(Expression<Action<SetupContext>> methodGetExpression, Action action)
    {
        var mockSetupProperties = SetupMockHelper.GetMockSetupProperties(methodGetExpression);
        var hookSettings = new HookSettings
        {
            HookManagerType = GlobalSettings.HookManagerType,
            ItParameterExpressions = mockSetupProperties.SetupContextState.ItParameterExpressions,
            OriginalMethodCallInstance = mockSetupProperties.OriginalMethodCallInstance
        };

        return new ActionMock(
            new HookBuilderFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookBuilder(),
            new HookManagerFactory(mockSetupProperties.OriginalMethodInfo, hookSettings).CreateHookManager(),
            action);
    }

    public static void SetupDefault(Expression<Action> methodGetExpression, Action action)
    {
        if (!(methodGetExpression.Body is MethodCallExpression methodExpression))
        {
            throw new Exception("Get expression not contains method to setup");
        }

        var originalMethodInfo = methodExpression.Method;
        if (originalMethodInfo.ReturnType != typeof(void))
        {
            throw new Exception("Default setup supported only for void methods");
        }

        var context = new SetupContext();
        var hookSettings = new HookSettings
        {
            HookManagerType = GlobalSettings.HookManagerType,
            ItParameterExpressions = context.State.ItParameterExpressions,
            OriginalMethodCallInstance = SetupMockHelper.GetOriginalMethodCallInstance(methodExpression)
        };

        var actionMock = new ActionMock(
            new HookBuilderFactory(originalMethodInfo, hookSettings).CreateHookBuilder(),
            new HookManagerFactory(originalMethodInfo, hookSettings).CreateHookManager(),
            action);

        actionMock.Callback(() => { });
    }
}