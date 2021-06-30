using System;
using StaticMock.Services.Common;

namespace StaticMock.Services.Callback
{
    internal interface ICallbackService
    {
        IReturnable Callback(Action callback);
        IReturnable Callback<TReturn>(Func<TReturn> callback);
    }
}
