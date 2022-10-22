﻿namespace StaticMock.Mocks;

public interface IFuncMock<in TReturnValue> : IMock
{
    void Callback(Func<TReturnValue> callback);
    void Returns(TReturnValue value);
    void Returns<TArg>(Func<TArg, TReturnValue> getValue);
}

public interface IFuncMock : IMock
{
    void Callback<TReturnValue>(Func<TReturnValue> callback);
    void Returns<TReturnValue>(TReturnValue value);
    void ReturnsAsync<TReturnValue>(TReturnValue value);
}