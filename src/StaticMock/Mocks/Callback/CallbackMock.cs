using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;

namespace StaticMock.Mocks.Callback;

internal class CallbackMock : ICallbackMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public CallbackMock(IHookBuilder hookBuilder, IHookManager hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IReturnable Callback(Action callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg>(Action<TArg> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback)
    {
        var hookMethod = _hookBuilder.CreateCallbackHook(callback);

        return _hookManager.ApplyHook(hookMethod);
    }
}