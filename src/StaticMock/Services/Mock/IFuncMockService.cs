using System;

namespace StaticMock.Services.Mock;

public interface IFuncMockService<in TReturnValue> : IMockService
{
    void Callback(Func<TReturnValue> callback);
    void Returns(TReturnValue value);
}

public interface IFuncMockService : IMockService
{
    void Callback<TReturnValue>(Func<TReturnValue> callback);
    void Returns<TReturnValue>(TReturnValue value);
    void ReturnsAsync<TReturnValue>(TReturnValue value);
}