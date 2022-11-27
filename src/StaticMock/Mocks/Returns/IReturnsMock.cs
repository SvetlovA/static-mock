using StaticMock.Hooks;

namespace StaticMock.Mocks.Returns;

internal interface IReturnsMock<in TValue>
{
    IReturnable Returns(TValue value);
    IReturnable Returns(Func<TValue> getValue);
    IReturnable Returns<TArg>(Func<TArg, TValue> getValue);
    IReturnable Returns<TArg1, TArg2>(Func<TArg1, TArg2, TValue> getValue);
    IReturnable Returns<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TValue> getValue);
    IReturnable Returns<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TValue> getValue);
    IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TValue> getValue);
    IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TValue> getValue);
    IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TValue> getValue);
    IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TValue> getValue);
    IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TValue> getValue);

    IReturnable ReturnsAsync(TValue value);
    IReturnable ReturnsAsync(Func<TValue> getValue);
    IReturnable ReturnsAsync<TArg>(Func<TArg, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2>(Func<TArg1, TArg2, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TValue> getValue);
    IReturnable ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TValue> getValue);
}