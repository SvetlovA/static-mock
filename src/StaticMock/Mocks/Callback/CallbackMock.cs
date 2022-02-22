using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Hooks.Entities;
using StaticMock.Hooks.Helpers;

namespace StaticMock.Mocks.Callback;

internal class CallbackMock : ICallbackMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly HookParameter[] _hookParameters;

    public CallbackMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        HookParameter[] hookParameters)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _hookParameters = hookParameters;
    }

    public IReturnable Callback(Action callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        return Inject(callback.Method);
    }

    public IReturnable Callback<TReturnValue>(Func<TReturnValue> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        return Inject(callback.Method);
    }

    public IReturnable CallbackAsync<TReturnValue>(Func<TReturnValue> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var hook = HookBuilder.CreateReturnHook(Task.FromResult(callback()), _hookParameters);
        return Inject(hook);
    }

    private IReturnable Inject(MethodBase methodInfoToInject)
    {
        var hookManager = _hookManagerFactory.CreateHookService(_originalMethodInfo);
        return hookManager.ApplyHook(methodInfoToInject);
    }
}