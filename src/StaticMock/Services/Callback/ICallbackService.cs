using System;
using StaticMock.Services.Injection;

namespace StaticMock.Services.Callback
{
    internal interface ICallbackService
    {
        IReturnable Callback(Action callback);
        IReturnable Callback<TReturn>(Func<TReturn> callback);
    }
}
