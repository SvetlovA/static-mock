using System;

namespace StaticMock.Mocks;

/// <summary>
/// Defines a mock for function methods that return values. Provides methods to configure return behaviors.
/// </summary>
public interface IFuncMock : IMock
{
    /// <summary>
    /// Configures the mock to return a specific value when the mocked method is called.
    /// </summary>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="value">The value to return when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TReturnValue>(TReturnValue value);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function when the mocked method is called.
    /// </summary>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the value to return when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TReturnValue>(Func<TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's single argument.
    /// </summary>
    /// <typeparam name="TArg">The type of the method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method argument.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg, TReturnValue>(Func<TArg, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's two arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TReturnValue>(Func<TArg1, TArg2, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's three arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TReturnValue>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's four arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's five arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's six arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's seven arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's eight arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="TArg8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's nine arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="TArg8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="TArg9">The type of the ninth method argument.</typeparam>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value asynchronously when the mocked method is called.
    /// </summary>
    /// <typeparam name="TReturnValue">The type of the return value.</typeparam>
    /// <param name="value">The value to return asynchronously when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable ReturnsAsync<TReturnValue>(TReturnValue value);
}

/// <summary>
/// Defines a strongly-typed mock for function methods that return a specific type. Provides methods to configure return behaviors with type safety.
/// </summary>
/// <typeparam name="TReturnValue">The type of the return value for the mocked function.</typeparam>
public interface IFuncMock<in TReturnValue> : IMock
{
    /// <summary>
    /// Configures the mock to return a specific value when the mocked method is called.
    /// </summary>
    /// <param name="value">The value to return when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns(TReturnValue value);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function when the mocked method is called.
    /// </summary>
    /// <param name="getValue">The function that computes the value to return when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns(Func<TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's single argument.
    /// </summary>
    /// <typeparam name="TArg">The type of the method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method argument.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg>(Func<TArg, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's two arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2>(Func<TArg1, TArg2, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's three arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's four arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's five arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's six arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's seven arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's eight arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="TArg8">The type of the eighth method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue);

    /// <summary>
    /// Configures the mock to return a value computed by the provided function using the method's nine arguments.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="TArg8">The type of the eighth method argument.</typeparam>
    /// <typeparam name="TArg9">The type of the ninth method argument.</typeparam>
    /// <param name="getValue">The function that computes the return value using the method arguments.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue);
}