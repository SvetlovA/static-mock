using StaticMock.Services.Common;

namespace StaticMock.Services.Returns
{
    internal interface IReturnsMockService<in TValue>
    {
        IReturnable Returns(TValue value);
        IReturnable ReturnsAsync(TValue value);
    }
}
