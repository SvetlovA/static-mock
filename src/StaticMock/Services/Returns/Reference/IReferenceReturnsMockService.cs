using StaticMock.Services.Injection;

namespace StaticMock.Services.Returns.Reference
{
    internal interface IReferenceReturnsMockService
    {
        IReturnable Returns(object value);
    }
}
