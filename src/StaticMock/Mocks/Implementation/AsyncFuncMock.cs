using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class AsyncFuncMock<TReturnValue> : FuncMock<Task<TReturnValue>>, IAsyncFuncMock<TReturnValue>
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;
    private readonly Action _action;

    public AsyncFuncMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager,
        Action action)
        : base(hookBuilder, hookManager, action)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
        _action = action;
    }

    public void ReturnsAsync(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg>(Func<TArg, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2>(Func<TArg1, TArg2, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void CallbackAsync(Func<TReturnValue> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.CallbackAsync(callback))
        {
            _action();
        }
    }
}