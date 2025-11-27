using System;
using System.Threading.Tasks;

namespace StaticMock.Mocks;

/// <summary>
/// Defines a mock for asynchronous function methods that return a Task with a specific value type.
/// Provides methods to configure asynchronous return behaviors.
/// </summary>
/// <typeparam name="TReturnValue">The type of the value returned by the asynchronous operation.</typeparam>
public interface IAsyncFuncMock<TReturnValue> : IFuncMock<Task<TReturnValue>>
{
    /// <summary>
    /// Configures the mock to return a value asynchronously wrapped in a completed Task when the mocked method is called.
    /// </summary>
    /// <param name="value">The value to return asynchronously when the mocked method is called.</param>
    /// <returns>An <see cref="IDisposable"/> that can be used to clean up the mock configuration.</returns>
    IDisposable ReturnsAsync(TReturnValue value);
}