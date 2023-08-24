using System;
using System.Threading.Tasks;

namespace StaticMock.Mocks;

public interface IAsyncFuncMock<TReturnValue> : IFuncMock<Task<TReturnValue>>
{
    IDisposable ReturnsAsync(TReturnValue value);
}