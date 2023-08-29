using System;
using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation.Sequential;

internal class FuncMock : Mock, IFuncMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public FuncMock(IHookBuilder hookBuilder, IHookManager hookManager) : base(hookBuilder, hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IDisposable Returns<TReturnValue>(TReturnValue value) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(value);

    public IDisposable Returns<TReturnValue>(Func<TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable ReturnsAsync<TReturnValue>(TReturnValue value) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).ReturnsAsync(value);

    public IDisposable Returns<TArg, TReturnValue>(Func<TArg, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TReturnValue>(Func<TArg1, TArg2, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TReturnValue>(Func<TArg1, TArg2, TArg3, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturnValue> getValue) =>
        new ReturnsMock<TReturnValue>(_hookBuilder, _hookManager).Returns(getValue);
}

internal class FuncMock<TReturn> : Mock, IFuncMock<TReturn>
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public FuncMock(IHookBuilder hookBuilder, IHookManager hookManager) : base(hookBuilder, hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IDisposable Returns(TReturn value) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(value);

    public IDisposable Returns(Func<TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg>(Func<TArg, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2>(Func<TArg1, TArg2, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);

    public IDisposable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue) =>
        new ReturnsMock<TReturn>(_hookBuilder, _hookManager).Returns(getValue);
}