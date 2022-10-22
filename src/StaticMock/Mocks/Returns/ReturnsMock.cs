using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;

namespace StaticMock.Mocks.Returns;

internal class ReturnsMock<TValue> : IReturnsMock<TValue>
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public ReturnsMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IReturnable Returns(TValue value) => InternalReturns(value);

    public IReturnable Returns<TArg>(Func<TArg, TValue> getValue)
    {
        var hook = _hookBuilder.CreateReturnHook(getValue);

        return _hookManager.ApplyHook(hook);
    }

    public IReturnable ReturnsAsync(TValue value) => InternalReturns(Task.FromResult(value));

    private IReturnable InternalReturns<TInternalValue>(TInternalValue value)
    {
        var hook = _hookBuilder.CreateReturnHook(value);

        return _hookManager.ApplyHook(hook);
    }
}