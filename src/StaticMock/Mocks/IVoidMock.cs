namespace StaticMock.Mocks;

public interface IVoidMock : IMock
{
    void Callback(Action callback);
}