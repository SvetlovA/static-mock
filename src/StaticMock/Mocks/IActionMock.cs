using System;

namespace StaticMock.Mocks;

public interface IActionMock : IMock
{
    IDisposable Callback(Action callback);
    IDisposable Callback<TArg>(Action<TArg> callback);
    IDisposable Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback);
    IDisposable Callback<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback);
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback);
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback);
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback);
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback);
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback);
    IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback);
}