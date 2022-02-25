using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks;
using StaticMock.Hooks.Helpers;

namespace StaticMock.Mocks.Callback;

internal class CallbackMock : ICallbackMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly SetupContextState _setupContextState;

    public CallbackMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        SetupContextState setupContextState)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _setupContextState = setupContextState;
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

        var hook = HookBuilder.CreateReturnHook(Task.FromResult(callback()), _setupContextState.ItParameterExpressions);
        return Inject(hook);
    }

    private IReturnable Inject(MethodBase methodInfoToInject)
    {
        var hookManager = _hookManagerFactory.CreateHookService(_originalMethodInfo);
        return hookManager.ApplyHook(methodInfoToInject);
    }
}