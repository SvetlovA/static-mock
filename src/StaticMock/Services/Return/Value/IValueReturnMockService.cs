namespace StaticMock.Services.Return.Value
{
    internal interface IValueReturnMockService<in TValue>
    {
        void Returns(TValue value);
    }
}
