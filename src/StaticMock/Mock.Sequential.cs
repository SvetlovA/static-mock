using StaticMock.Helpers;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System;
using StaticMock.Entities;
using StaticMock.Entities.Context;
using StaticMock.Mocks;

namespace StaticMock;

public static partial class Mock
{
    public static IFuncMock Setup(Type type, string methodName) => SetupMockHelper.SetupInternal(type, methodName);

    public static IFuncMock Setup(Type type, string methodName, BindingFlags bindingFlags) =>
        SetupMockHelper.SetupInternal(type, methodName, setupProperties: new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static IFuncMock Setup(Type type, string methodName, SetupProperties setupProperties) =>
        SetupMockHelper.SetupInternal(type, methodName, setupProperties: setupProperties);

    public static IFuncMock SetupProperty(Type type, string propertyName) => SetupMockHelper.SetupPropertyInternal(type, propertyName);

    public static IFuncMock SetupProperty(Type type, string propertyName, BindingFlags bindingFlags) =>
        SetupMockHelper.SetupPropertyInternal(type, propertyName, setupProperties: new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static IFuncMock SetupProperty(Type type, string propertyName, SetupProperties setupProperties) =>
        SetupMockHelper.SetupPropertyInternal(type, propertyName, setupProperties: setupProperties);

    public static IActionMock SetupAction(Type type, string methodName) => SetupMockHelper.SetupVoidInternal(type, methodName);

    public static IActionMock SetupAction(Type type, string methodName, BindingFlags bindingFlags) =>
        SetupMockHelper.SetupVoidInternal(type, methodName, setupProperties: new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static IActionMock SetupAction(Type type, string methodName, SetupProperties setupProperties) =>
        SetupMockHelper.SetupVoidInternal(type, methodName, setupProperties: setupProperties);

    public static void SetupDefault(Type type, string methodName) => SetupMockHelper.SetupDefaultInternal(type, methodName);

    public static void SetupDefault(Type type, string methodName, BindingFlags bindingFlags) =>
        SetupMockHelper.SetupDefaultInternal(type, methodName, setupProperties: new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    public static void SetupDefault(Type type, string methodName, SetupProperties setupProperties) =>
        SetupMockHelper.SetupDefaultInternal(type, methodName, setupProperties: setupProperties);

    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression) =>
        SetupMockHelper.SetupInternal(methodGetExpression);

    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(
        Expression<Func<Task<TReturnValue>>> methodGetExpression) => SetupMockHelper.SetupInternal(methodGetExpression);

    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, TReturnValue>> methodGetExpression) =>
        SetupMockHelper.SetupInternal(methodGetExpression);

    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, Task<TReturnValue>>> methodGetExpression) =>
        SetupMockHelper.SetupInternal(methodGetExpression);

    public static IActionMock Setup(Expression<Action> methodGetExpression) =>
        SetupMockHelper.SetupInternal(methodGetExpression);

    public static IActionMock Setup(Expression<Action<SetupContext>> methodGetExpression) =>
        SetupMockHelper.SetupInternal(methodGetExpression);

    public static void SetupDefault(Expression<Action> methodGetExpression) =>
        SetupMockHelper.SetupDefaultInternal(methodGetExpression);
}