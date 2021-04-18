namespace StaticMock.Services.Generic
{
    public interface IMockService<in TValue> where TValue : unmanaged
    {
        void Returns(TValue value);
    }
}
