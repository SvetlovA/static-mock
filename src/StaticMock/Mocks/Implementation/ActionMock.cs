using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Callback;

namespace StaticMock.Mocks.Implementation;

internal class ActionMock : Mock, IActionMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;
    private readonly Action _action;

    public ActionMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager,
        Action action)
        : base(hookBuilder, hookManager, action)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
        _action = action;
    }

    public void Callback(Action callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg>(Action<TArg> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2, TArg3, TArg4>(Action<TArg1, TArg2, TArg3, TArg4> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2, TArg3, TArg4, TArg5>(Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Callback<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Action<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }
}