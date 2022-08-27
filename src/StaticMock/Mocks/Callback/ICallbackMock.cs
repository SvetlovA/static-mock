using StaticMock.Hooks;

namespace StaticMock.Mocks.Callback;

internal interface ICallbackMock
{
    IReturnable Callback(Action callback);
    IReturnable Callback<TReturnValue>(Func<TReturnValue> callback);
    IReturnable CallbackAsync<TReturnValue>(Func<TReturnValue> callback);
}