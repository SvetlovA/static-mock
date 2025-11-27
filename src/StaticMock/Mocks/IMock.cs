using System;

namespace StaticMock.Mocks;

/// <summary>
/// Defines the base interface for all mock types. Provides methods to configure exception throwing behaviors.
/// </summary>
public interface IMock
{
    /// <summary>
    /// Configures the mock to throw an exception of the specified type when the mocked method is called.
    /// </summary>
    /// <param name="exceptionType">The type of exception to throw. Must derive from <see cref="Exception"/>.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Throws(Type exceptionType);

    /// <summary>
    /// Configures the mock to throw an exception of the specified type with constructor arguments when the mocked method is called.
    /// </summary>
    /// <param name="exceptionType">The type of exception to throw. Must derive from <see cref="Exception"/>.</param>
    /// <param name="constructorArgs">The arguments to pass to the exception constructor.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Throws(Type exceptionType, params object[] constructorArgs);

    /// <summary>
    /// Configures the mock to throw an exception of the specified type when the mocked method is called.
    /// </summary>
    /// <typeparam name="TException">The type of exception to throw. Must derive from <see cref="Exception"/> and have a parameterless constructor.</typeparam>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Throws<TException>() where TException : Exception, new();

    /// <summary>
    /// Configures the mock to throw an exception of the specified type with constructor arguments when the mocked method is called.
    /// </summary>
    /// <typeparam name="TException">The type of exception to throw. Must derive from <see cref="Exception"/>.</typeparam>
    /// <param name="constructorArgs">The arguments to pass to the exception constructor.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable Throws<TException>(object[] constructorArgs) where TException : Exception;
}