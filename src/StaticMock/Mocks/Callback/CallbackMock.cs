using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;

namespace StaticMock.Mocks.Callback;

internal class CallbackMock : ICallbackMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public CallbackMock(IHookBuilder hookBuilder, IHookManager hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IReturnable Callback(Action action)
    {
        var hookMethod = _hookBuilder.CreateVoidHook(action);

        return _hookManager.ApplyHook(hookMethod);
    }
}