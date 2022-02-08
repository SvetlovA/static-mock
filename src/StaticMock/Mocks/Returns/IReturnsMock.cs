using StaticMock.Hooks;

namespace StaticMock.Mocks.Returns;

internal interface IReturnsMock<in TValue>
{
    IReturnable Returns(TValue value);
    IReturnable ReturnsAsync(TValue value);
}