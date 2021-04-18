namespace StaticMock.Services.Return.Generic
{
    internal interface IReturnMockService<in TValue> where TValue : unmanaged
    {
        void Returns(TValue value);
    }
}
