using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using StaticMock.Entities;
using StaticMock.Entities.Context;
using StaticMock.Mocks;
using StaticMock.Helpers;

namespace StaticMock
{
    /// <summary>
    /// Provides a set of static methods for setting up and configuring method and property mocks.
    /// </summary>
    public static partial class Mock
    {
        /// <summary>
        /// Sets up a method mock for the specified type and method name. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <returns>An <see cref="IFuncMock"/> representing the configured method mock.</returns>
        public static IFuncMock Setup(Type type, string methodName) =>
            SetupMockHelper.SetupInternal(type, methodName);

        /// <summary>
        /// Sets up a method mock for the specified type, method name, and binding flags. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <param name="bindingFlags">The binding flags to be used when searching for the method.</param>
        /// <returns>An <see cref="IFuncMock"/> representing the configured method mock.</returns>
        public static IFuncMock Setup(Type type, string methodName, BindingFlags bindingFlags) =>
            SetupMockHelper.SetupInternal(type, methodName, setupProperties: new SetupProperties
            {
                BindingFlags = bindingFlags
            });

        /// <summary>
        /// Sets up a method mock for the specified type, method name, and setup properties. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <param name="setupProperties">The setup properties for configuring the mock.</param>
        /// <returns>An <see cref="IFuncMock"/> representing the configured method mock.</returns>
        public static IFuncMock Setup(Type type, string methodName, SetupProperties setupProperties) =>
            SetupMockHelper.SetupInternal(type, methodName, setupProperties: setupProperties);

        /// <summary>
        /// Sets up a property mock for the specified type and property name. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the property to be mocked.</param>
        /// <param name="propertyName">The name of the property to be mocked.</param>
        /// <returns>An <see cref="IFuncMock"/> representing the configured property mock.</returns>
        public static IFuncMock SetupProperty(Type type, string propertyName) =>
            SetupMockHelper.SetupPropertyInternal(type, propertyName);

        /// <summary>
        /// Sets up a property mock for the specified type, property name, and binding flags. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the property to be mocked.</param>
        /// <param name="propertyName">The name of the property to be mocked.</param>
        /// <param name="bindingFlags">The binding flags to be used when searching for the property.</param>
        /// <returns>An <see cref="IFuncMock"/> representing the configured property mock.</returns>
        public static IFuncMock SetupProperty(Type type, string propertyName, BindingFlags bindingFlags) =>
            SetupMockHelper.SetupPropertyInternal(type, propertyName, setupProperties: new SetupProperties
            {
                BindingFlags = bindingFlags
            });

        /// <summary>
        /// Sets up a property mock for the specified type, property name, and setup properties. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the property to be mocked.</param>
        /// <param name="propertyName">The name of the property to be mocked.</param>
        /// <param name="setupProperties">The setup properties for configuring the mock.</param>
        /// <returns>An <see cref="IFuncMock"/> representing the configured property mock.</returns>
        public static IFuncMock SetupProperty(Type type, string propertyName, SetupProperties setupProperties) =>
            SetupMockHelper.SetupPropertyInternal(type, propertyName, setupProperties: setupProperties);

        /// <summary>
        /// Sets up an action (void method) mock for the specified type and method name. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
        public static IActionMock SetupAction(Type type, string methodName) =>
            SetupMockHelper.SetupVoidInternal(type, methodName);

        /// <summary>
        /// Sets up an action (void method) mock for the specified type, method name, and binding flags. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <param name="bindingFlags">The binding flags to be used when searching for the method.</param>
        /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
        public static IActionMock SetupAction(Type type, string methodName, BindingFlags bindingFlags) =>
            SetupMockHelper.SetupVoidInternal(type, methodName, setupProperties: new SetupProperties
            {
                BindingFlags = bindingFlags
            });

        /// <summary>
        /// Sets up an action (void method) mock for the specified type, method name, and setup properties. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <param name="setupProperties">The setup properties for configuring the mock.</param>
        /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
        public static IActionMock SetupAction(Type type, string methodName, SetupProperties setupProperties) =>
            SetupMockHelper.SetupVoidInternal(type, methodName, setupProperties: setupProperties);

        /// <summary>
        /// Sets up a default action for the specified type and method name. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        public static void SetupDefault(Type type, string methodName) =>
            SetupMockHelper.SetupDefaultInternal(type, methodName);

        /// <summary>
        /// Sets up a default action for the specified type, method name, and binding flags. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <param name="bindingFlags">The binding flags to be used when searching for the method.</param>
        public static void SetupDefault(Type type, string methodName, BindingFlags bindingFlags) =>
            SetupMockHelper.SetupDefaultInternal(type, methodName, setupProperties: new SetupProperties
            {
                BindingFlags = bindingFlags
            });

        /// <summary>
        /// Sets up a default action for the specified type, method name, and setup properties. (Sequential)
        /// </summary>
        /// <param name="type">The type containing the method to be mocked.</param>
        /// <param name="methodName">The name of the method to be mocked.</param>
        /// <param name="setupProperties">The setup properties for configuring the mock.</param>
        public static void SetupDefault(Type type, string methodName, SetupProperties setupProperties) =>
            SetupMockHelper.SetupDefaultInternal(type, methodName, setupProperties: setupProperties);

        /// <summary>
        /// Sets up a function mock for the specified expression. (Sequential)
        /// </summary>
        /// <typeparam name="TReturnValue">The return type of the function to be mocked.</typeparam>
        /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
        /// <returns>An <see cref="IFuncMock{TReturnValue}"/> representing the configured function mock.</returns>
        public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<TReturnValue>> methodGetExpression) =>
            SetupMockHelper.SetupInternal(methodGetExpression);

        /// <summary>
        /// Sets up an asynchronous function mock for the specified expression. (Sequential)
        /// </summary>
        /// <typeparam name="TReturnValue">The return type of the asynchronous function to be mocked.</typeparam>
        /// <param name="methodGetExpression">The expression representing the asynchronous method to be mocked.</param>
        /// <returns>An <see cref="IAsyncFuncMock{TReturnValue}"/> representing the configured asynchronous function mock.</returns>
        public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(
            Expression<Func<Task<TReturnValue>>> methodGetExpression) =>
            SetupMockHelper.SetupInternal(methodGetExpression);

        /// <summary>
        /// Sets up a function mock for the specified expression, including setup context. (Sequential)
        /// </summary>
        /// <typeparam name="TReturnValue">The return type of the function to be mocked.</typeparam>
        /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
        /// <returns>An <see cref="IFuncMock{TReturnValue}"/> representing the configured function mock.</returns>
        public static IFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, TReturnValue>> methodGetExpression) =>
            SetupMockHelper.SetupInternal(methodGetExpression);

        /// <summary>
        /// Sets up an asynchronous function mock for the specified expression, including setup context. (Sequential)
        /// </summary>
        /// <typeparam name="TReturnValue">The return type of the asynchronous function to be mocked.</typeparam>
        /// <param name="methodGetExpression">The expression representing the asynchronous method to be mocked.</param>
        /// <returns>An <see cref="IAsyncFuncMock{TReturnValue}"/> representing the configured asynchronous function mock.</returns>
        public static IAsyncFuncMock<TReturnValue> Setup<TReturnValue>(Expression<Func<SetupContext, Task<TReturnValue>>> methodGetExpression) =>
            SetupMockHelper.SetupInternal(methodGetExpression);

        /// <summary>
        /// Sets up an action (void method) mock for the specified expression. (Sequential)
        /// </summary>
        /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
        /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
        public static IActionMock Setup(Expression<Action> methodGetExpression) =>
            SetupMockHelper.SetupInternal(methodGetExpression);

        /// <summary>
        /// Sets up an action (void method) mock for the specified expression, including setup context. (Sequential)
        /// </summary>
        /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
        /// <returns>An <see cref="IActionMock"/> representing the configured action mock.</returns>
        public static IActionMock Setup(Expression<Action<SetupContext>> methodGetExpression) =>
            SetupMockHelper.SetupInternal(methodGetExpression);

        /// <summary>
        /// Sets up a default action for the specified expression. (Sequential)
        /// </summary>
        /// <param name="methodGetExpression">The expression representing the method to be mocked.</param>
        public static void SetupDefault(Expression<Action> methodGetExpression) =>
            SetupMockHelper.SetupDefaultInternal(methodGetExpression);
    }
}
