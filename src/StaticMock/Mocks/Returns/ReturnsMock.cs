using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;

namespace StaticMock.Mocks.Returns;

internal class ReturnsMock<TValue> : IReturnsMock<TValue>
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public ReturnsMock(IHookBuilder hookBuilder, IHookManager hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IReturnable Returns(TValue value)
    {
        var hook = _hookBuilder.CreateReturnHook(value);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns(Func<TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg>(Func<TArg, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2>(Func<TArg1, TArg2, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2, TArg3, TArg4>(Func<TArg1, TArg2, TArg3, TArg4, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable Returns<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable ReturnsAsync(TValue value)
    {
        var hook = _hookBuilder.CreateReturnAsyncHook(value);

        return _hookManager.ApplyHook(hook);
    }
}