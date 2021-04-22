using StaticMock.Services.Injection;

namespace StaticMock.Services.Return.Reference
{
    internal interface IReferenceReturnMockService
    {
        IReturnable Returns(object value);
    }
}
