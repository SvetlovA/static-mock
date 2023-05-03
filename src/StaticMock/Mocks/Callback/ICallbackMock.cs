using System;
using StaticMock.Hooks;

namespace StaticMock.Mocks.Callback;

internal interface ICallbackMock
{
    IReturnable Callback(Action callback);
    IReturnable Callback<TArg>(Action<TArg> callback);
    IReturnable Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback);
    IReturnable Callback<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback);
    IReturnable Callback<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback);
    IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback);
    IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback);
    IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback);
    IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback);
    IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback);
}