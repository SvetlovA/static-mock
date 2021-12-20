namespace StaticMock.Services.Common;

internal interface IReturnable : IDisposable
{
    void Return();
}