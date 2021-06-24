using StaticMock.Services.Common;

namespace StaticMock.Services.Returns.Value
{
    internal interface IValueReturnsMockService<in TValue> : IReturnable
    {
        IReturnable Returns(TValue value);
    }
}
