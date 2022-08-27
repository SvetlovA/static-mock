﻿using StaticMock.Hooks;
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

    public void CallbackAsync(Func<TReturnValue> callback)
    {
        var callbackService = new CallbackMock(_hookBuilder, _hookManager);
        using (callbackService.CallbackAsync(callback))
        {
            _action();
        }
    }
}