using System;
using StaticMock.Helpers;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using StaticMock.Entities;
using StaticMock.Entities.Context;
using StaticMock.Mocks;

namespace StaticMock;

/// <summary>
/// Provides a set of static methods for setting up and configuring method and property mocks. (Hierarchical)
/// </summary>
public static partial class Mock
{
    /// <summary>
    /// Gets or sets the global settings for the StaticMock library.
    /// </summary>
    public static GlobalSettings GlobalSettings { get; } = new();

    /// <summary>
    /// Sets up a method mock for the specified type, method name, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IFuncMock"/> representing the configured method mock.</returns>
    public static IFuncMock Setup(Type type, string methodName, Action action) => SetupMockHelper.SetupInternal(type, methodName, action);
    
    /// <summary>
    /// Sets up a method mock for the specified type, method name, binding flags, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="bindingFlags">The binding flags to be used when searching for the method.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IFuncMock"/> representing the configured method mock.</returns>
    public static IFuncMock Setup(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupInternal(type, methodName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    /// <summary>
    /// Sets up a method mock for the specified type, method name, setup properties, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="setupProperties">The setup properties for configuring the mock.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IFuncMock"/> representing the configured method mock.</returns>
    public static IFuncMock Setup(Type type, string methodName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupInternal(type, methodName, action, setupProperties);

    /// <summary>
    /// Sets up a property mock for the specified type, property name, and action.
    /// </summary>
    /// <param name="type">The type containing the property to be mocked.</param>
    /// <param name="propertyName">The name of the property to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked property is accessed or modified.</param>
    /// <returns>An <see cref="IFuncMock"/> representing the configured property mock.</returns>
    public static IFuncMock SetupProperty(Type type, string propertyName, Action action) => SetupMockHelper.SetupPropertyInternal(type, propertyName, action);

    /// <summary>
    /// Sets up a property mock for the specified type, property name, binding flags, and action.
    /// </summary>
    /// <param name="type">The type containing the property to be mocked.</param>
    /// <param name="propertyName">The name of the property to be mocked.</param>
    /// <param name="bindingFlags">The binding flags to be used when searching for the property.</param>
    /// <param name="action">The action to be executed when the mocked property is accessed or modified.</param>
    /// <returns>An <see cref="IFuncMock"/> representing the configured property mock.</returns>
    public static IFuncMock SetupProperty(Type type, string propertyName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupPropertyInternal(type, propertyName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    /// <summary>
    /// Sets up a property mock for the specified type, property name, setup properties, and action.
    /// </summary>
    /// <param name="type">The type containing the property to be mocked.</param>
    /// <param name="propertyName">The name of the property to be mocked.</param>
    /// <param name="setupProperties">The setup properties for configuring the mock.</param>
    /// <param name="action">The action to be executed when the mocked property is accessed or modified.</param>
    /// <returns>An <see cref="IFuncMock"/> representing the configured property mock.</returns>
    public static IFuncMock SetupProperty(Type type, string propertyName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupPropertyInternal(type, propertyName, action, setupProperties);

    /// <summary>
    /// Sets up an action (void method) mock for the specified type, method name, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
    public static IActionMock SetupAction(Type type, string methodName, Action action) => SetupMockHelper.SetupVoidInternal(type, methodName, action);

    /// <summary>
    /// Sets up an action (void method) mock for the specified type, method name, binding flags, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="bindingFlags">The binding flags to be used when searching for the method.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
    public static IActionMock SetupAction(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupVoidInternal(type, methodName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    /// <summary>
    /// Sets up an action (void method) mock for the specified type, method name, setup properties, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="setupProperties">The setup properties for configuring the mock.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
    public static IActionMock SetupAction(Type type, string methodName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupVoidInternal(type, methodName, action, setupProperties);

    /// <summary>
    /// Sets up a default action for the specified type, method name, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    public static void SetupDefault(Type type, string methodName, Action action) => SetupMockHelper.SetupDefaultInternal(type, methodName, action);

    /// <summary>
    /// Sets up a default action for the specified type, method name, binding flags, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="bindingFlags">The binding flags to be used when searching for the method.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    public static void SetupDefault(Type type, string methodName, BindingFlags bindingFlags, Action action) =>
        SetupMockHelper.SetupDefaultInternal(type, methodName, action, new SetupProperties
        {
            BindingFlags = bindingFlags
        });

    /// <summary>
    /// Sets up a default action for the specified type, method name, setup properties, and action.
    /// </summary>
    /// <param name="type">The type containing the method to be mocked.</param>
    /// <param name="methodName">The name of the method to be mocked.</param>
    /// <param name="setupProperties">The setup properties for configuring the mock.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    public static void SetupDefault(Type type, string methodName, SetupProperties setupProperties, Action action) =>
        SetupMockHelper.SetupDefaultInternal(type, methodName, action, setupProperties);

    /// <summary>
    /// Sets up a function mock for the specified expression and action.
    /// </summary>
    /// <typeparam name="TReturnValue">The return type of the function to be mocked.</typeparam>
    /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IFuncMock{TReturnValue}"/> representing the configured function mock.</returns>
    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression,
        Action action) => SetupMockHelper.SetupInternal(methodGetExpression, action);

    /// <summary>
    /// Sets up an asynchronous function mock for the specified expression and action.
    /// </summary>
    /// <typeparam name="TReturnValue">The return type of the asynchronous function to be mocked.</typeparam>
    /// <param name="methodGetExpression">The expression representing the asynchronous method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked asynchronous method is called.</param>
    /// <returns>An <see cref="IAsyncFuncMock{TReturnValue}"/> representing the configured asynchronous function mock.</returns>
    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<Task<TReturnValue>>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    /// <summary>
    /// Sets up a function mock for the specified expression and action, including setup context.
    /// </summary>
    /// <typeparam name="TReturnValue">The return type of the function to be mocked.</typeparam>
    /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IFuncMock{TReturnValue}"/> representing the configured function mock.</returns>
    public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, TReturnValue>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    /// <summary>
    /// Sets up an asynchronous function mock for the specified expression and action, including setup context.
    /// </summary>
    /// <typeparam name="TReturnValue">The return type of the asynchronous function to be mocked.</typeparam>
    /// <param name="methodGetExpression">The expression representing the asynchronous method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked asynchronous method is called.</param>
    /// <returns>An <see cref="IAsyncFuncMock{TReturnValue}"/> representing the configured asynchronous function mock.</returns>
    public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, Task<TReturnValue>>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    /// <summary>
    /// Sets up an action (void method) mock for the specified expression and action.
    /// </summary>
    /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
    public static IActionMock Setup(Expression<Action> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    /// <summary>
    /// Sets up an action (void method) mock for the specified expression and action, including setup context.
    /// </summary>
    /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
    public static IActionMock Setup(Expression<Action<SetupContext>> methodGetExpression, Action action) =>
        SetupMockHelper.SetupInternal(methodGetExpression, action);

    /// <summary>
    /// Sets up a default action for the specified expression and action.
    /// </summary>
    /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
    /// <param name="action">The action to be executed when the mocked method is called.</param>
    public static void SetupDefault(Expression<Action> methodGetExpression, Action action) =>
        SetupMockHelper.SetupDefaultInternal(methodGetExpression, action);
}