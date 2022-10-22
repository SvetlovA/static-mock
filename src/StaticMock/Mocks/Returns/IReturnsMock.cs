using StaticMock.Hooks;

namespace StaticMock.Mocks.Returns;

internal interface IReturnsMock<in TValue>
{
    IReturnable Returns(TValue value);
    IReturnable Returns<TArg>(Func<TArg, TValue> getValue);
    IReturnable ReturnsAsync(TValue value);
}