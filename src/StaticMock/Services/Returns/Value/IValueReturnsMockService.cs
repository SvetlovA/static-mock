using StaticMock.Services.Injection;

namespace StaticMock.Services.Returns.Value
{
    internal interface IValueReturnsMockService<in TValue>
    {
        IReturnable Returns(TValue value);
    }
}
