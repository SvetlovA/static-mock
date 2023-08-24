using System;
using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation.Hierarchical;

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

    public IDisposable Returns<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.Returns(value))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TReturnValue>(Func<TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable ReturnsAsync<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg, TReturnValue>(Func<TArg, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TReturnValue>(Func<TArg1, TArg2, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TReturnValue>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue)
    {
        var returnMock = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }
}

internal class FuncMock<TReturn> : Mock, IFuncMock<TReturn>
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

    public IDisposable Returns(TReturn value)
    {
        var returnService = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnService.Returns(value))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns(Func<TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg>(Func<TArg, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2>(Func<TArg1, TArg2, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue)
    {
        var returnMock = new ReturnsMock<TReturn>(_hookBuilder, _hookManager);
        using (returnMock.Returns(getValue))
        {
            _action();
        }

        return new Disposable();
    }
}