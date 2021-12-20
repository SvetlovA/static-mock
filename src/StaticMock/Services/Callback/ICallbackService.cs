using System;
using StaticMock.Services.Common;

namespace StaticMock.Services.Callback;

internal interface ICallbackService
{
    IReturnable Callback(Action callback);
    IReturnable Callback<TReturnValue>(Func<TReturnValue> callback);
    IReturnable CallbackAsync<TReturnValue>(Func<TReturnValue> callback);
}