using StaticMock.Services.Injection;

namespace StaticMock.Services.Return.Value
{
    internal interface IValueReturnMockService<in TValue>
    {
        IReturnable Returns(TValue value);
    }
}
