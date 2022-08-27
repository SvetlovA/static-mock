﻿namespace StaticMock.Mocks;

public interface IAsyncFuncMock<TReturnValue> : IFuncMock<Task<TReturnValue>>
{
    void ReturnsAsync(TReturnValue value);
    void CallbackAsync(Func<TReturnValue> callback);
}