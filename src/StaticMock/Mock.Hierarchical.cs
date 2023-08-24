using System;
using StaticMock.Helpers;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using StaticMock.Entities;
using StaticMock.Entities.Context;
using StaticMock.Mocks;

namespace StaticMock;

public static partial class Mock
{
    public static GlobalSettings GlobalSettings { get; } = new();

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

    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression,
        Action action) => SetupMockHelper.SetupInternal(methodGetExpression, action);

    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<Task<TReturnValue>>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, TReturnValue>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, Task<TReturnValue>>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    public static IActionMock Setup(Expression<Action> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    public static IActionMock Setup(Expression<Action<SetupContext>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    public static void SetupDefault(Expression<Action> methodGetExpression, Action action) =>
        SetupMockHelper.SetupDefaultInternal(methodGetExpression, action);
}