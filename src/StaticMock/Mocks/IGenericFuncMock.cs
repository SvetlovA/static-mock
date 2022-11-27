namespace StaticMock.Mocks;

public interface IGenericFuncMock<in TReturnValue> : IMock
{
    void Callback(Func<TReturnValue> callback);

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