using System;

namespace StaticMock.Mocks;

/// <summary>
/// Defines a mock for action methods (void methods). Provides methods to configure callback behaviors.
/// </summary>
public interface IActionMock : IMock
{
    /// <summary>
    /// Configures the mock to execute a callback action when the mocked method is called.
    /// </summary>
    /// <param name="callback">The action to execute when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback(Action callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's single argument when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg">The type of the method argument.</typeparam>
    /// <param name="callback">The action to execute using the method argument when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg>(Action<TArg> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's two arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's three arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's four arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's five arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's six arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's seven arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's eight arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TArg1">The type of the first method argument.</typeparam>
    /// <typeparam name="TArg2">The type of the second method argument.</typeparam>
    /// <typeparam name="TArg3">The type of the third method argument.</typeparam>
    /// <typeparam name="TArg4">The type of the fourth method argument.</typeparam>
    /// <typeparam name="TArg5">The type of the fifth method argument.</typeparam>
    /// <typeparam name="TArg6">The type of the sixth method argument.</typeparam>
    /// <typeparam name="TArg7">The type of the seventh method argument.</typeparam>
    /// <typeparam name="TArg8">The type of the eighth method argument.</typeparam>
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback);

    /// <summary>
    /// Configures the mock to execute a callback action using the method's nine arguments when the mocked method is called.
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
    /// <param name="callback">The action to execute using the method arguments when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback);
}