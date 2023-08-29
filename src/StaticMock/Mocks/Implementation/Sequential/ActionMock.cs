using System;
using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Callback;

namespace StaticMock.Mocks.Implementation.Sequential;

internal class ActionMock : Mock, IActionMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public ActionMock(IHookBuilder hookBuilder, IHookManager hookManager) : base(hookBuilder, hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IDisposable Callback(Action callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg>(Action<TArg> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);

    public IDisposable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback) =>
        new CallbackMock(_hookBuilder, _hookManager).Callback(callback);
}