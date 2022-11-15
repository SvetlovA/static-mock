using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class FuncMock<TReturn> : Mock, IFuncMock, IFuncMock<TReturn>
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;
    private readonly Action _action;

    public FuncMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager,
        Action action)
        : base(hookBuilder, hookManager, action)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
        _action = action;
    }

    public void Callback(Func<TReturn> callback)
    {
        Callback<TReturn>(callback);
    }

    public void Returns(TReturn value)
    {
        Returns<TReturn>(value);
    }

    public void Returns<TArg>(Func<TArg, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2>(Func<TArg1, TArg2, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Callback<TReturnValue>(Func<TReturnValue> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Returns<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.Returns(value))
        {
            _action();
        }
    }

    public void ReturnsAsync<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }
}