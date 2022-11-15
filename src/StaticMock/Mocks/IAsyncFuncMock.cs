namespace StaticMock.Mocks;

public interface IAsyncFuncMock<TReturnValue> : IFuncMock<Task<TReturnValue>>
{
    void ReturnsAsync(TReturnValue value);
    void ReturnsAsync<TArg>(Func<TArg, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2>(Func<TArg1, TArg2, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue);
    void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue);

    void CallbackAsync(Func<TReturnValue> callback);
}