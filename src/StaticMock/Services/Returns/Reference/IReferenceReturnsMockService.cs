using StaticMock.Services.Common;

namespace StaticMock.Services.Returns.Reference
{
    internal interface IReferenceReturnsMockService : IReturnable
    {
        IReturnable Returns(object value);
    }
}
