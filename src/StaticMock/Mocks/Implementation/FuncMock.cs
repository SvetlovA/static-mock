using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class FuncMock : Mock, IFuncMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;
    private readonly Action _action;

    public FuncMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager,
        Action action) : base(hookBuilder, hookManager, action)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
        _action = action;
    }

    public void Callback<TReturnValue>(Func<TReturnValue> callback)
    {
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

    public void Returns<TArg, TReturnValue>(Func<TArg, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TReturnValue>(Func<TArg1, TArg2, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TReturnValue>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }

    public void Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);

        using (returnMock.Returns(getValue))
        {
            _action();
        }
    }
}