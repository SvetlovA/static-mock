using System;

namespace StaticMock.Mocks;

public interface IActionMock : IMock
{
    void Callback(Action callback);
    void Callback<TArg>(Action<TArg> callback);
    void Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback);
    void Callback<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback);
    void Callback<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback);
    void Callback<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback);
    void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback);
    void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback);
    void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback);
    void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback);
}