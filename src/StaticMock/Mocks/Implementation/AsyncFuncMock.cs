﻿using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class AsyncFuncMock : FuncMock, IAsyncFuncMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;
    private readonly Action _action;

    public AsyncFuncMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager,
        Action action) : base(hookBuilder, hookManager, action)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
        _action = action;
    }

    public void ReturnsAsync<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg, TReturnValue>(Func<TArg, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TReturnValue>(Func<TArg1, TArg2, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TReturnValue>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }

    public void ReturnsAsync<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue)
    {
        var returnService = new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager);
        using (returnService.ReturnsAsync(getValue))
        {
            _action();
        }
    }
}