using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Callback;

namespace StaticMock.Mocks.Implementation;

internal class VoidMock : Mock, IVoidMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;
    private readonly Action _action;

    public VoidMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager,
        Action action)
        : base(hookBuilder, hookManager, action)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
        _action = action;
    }

    public void Callback(Action callback)
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
}