namespace StaticMock.Services
{
    public interface IMockService : IVoidMockService
    {
        void Returns<TValue>(TValue value);
    }
}
