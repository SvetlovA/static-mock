using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Hook;

namespace StaticMock.Services.Callback;

internal class CallbackService : ICallbackService
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookServiceFactory _hookServiceFactory;
    private readonly IHookBuilder _hookBuilder;

    public CallbackService(MethodInfo originalMethodInfo, IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder)
    {
        _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
        _hookBuilder = hookBuilder ?? throw new ArgumentNullException(nameof(hookBuilder));
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

        var hook = _hookBuilder.CreateReturnHook(Task.FromResult(callback()));
        return Inject(hook);
    }

    private IReturnable Inject(MethodBase methodInfoToInject)
    {
        var injectionService = _hookServiceFactory.CreateHookService(_originalMethodInfo);
        return injectionService.Hook(methodInfoToInject);
    }
}