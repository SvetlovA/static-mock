using StaticMock.Hooks;

namespace StaticMock.Mocks.Callback;

internal interface ICallbackMock
{
    IReturnable Callback(Action action);
}