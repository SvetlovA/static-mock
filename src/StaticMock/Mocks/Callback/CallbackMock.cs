using System.Reflection;
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

    public IReturnable Callback(Action callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        MethodBase hookMethod;
        try
        {
            callback();
            hookMethod = _hookBuilder.CreateVoidHook();
        }
        catch (Exception ex)
        {
            hookMethod = _hookBuilder.CreateThrowsHook(ex);

        }

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable Callback<TReturnValue>(Func<TReturnValue> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        MethodBase hookMethod;
        try
        {
            hookMethod = _hookBuilder.CreateReturnHook(callback());
        }
        catch (Exception ex)
        {
            hookMethod = _hookBuilder.CreateThrowsHook(ex);
        }

        return _hookManager.ApplyHook(hookMethod);
    }

    public IReturnable CallbackAsync<TReturnValue>(Func<TReturnValue> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        MethodBase hookMethod;
        try
        {
            hookMethod = _hookBuilder.CreateReturnHook(Task.FromResult(callback()));
        }
        catch (Exception ex)
        {
            hookMethod = _hookBuilder.CreateThrowsHook(ex);
        }

        return _hookManager.ApplyHook(hookMethod);
    }
}