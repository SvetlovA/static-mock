using System;

namespace StaticMock.Mocks;

public interface IFuncMock : IMock
{
    void Returns<TReturnValue>(TReturnValue value);
    void Returns<TReturnValue>(Func<TReturnValue> getValue);
    void Returns<TArg, TReturnValue>(Func<TArg, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TReturnValue>(Func<TArg1, TArg2, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TReturnValue>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue);

    void ReturnsAsync<TReturnValue>(TReturnValue value);
}

public interface IFuncMock<in TReturnValue> : IMock
{
    void Returns(TReturnValue value);
    void Returns(Func<TReturnValue> getValue);
    void Returns<TArg>(Func<TArg, TReturnValue> getValue);
    void Returns<TArg1, TArg2>(Func<TArg1, TArg2, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue);
    void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue);
}