using System.Reflection;
using StaticMock.Services.Callback;
using StaticMock.Services.Hook;

namespace StaticMock.Services.Mock.Implementation;

internal class VoidMockService : MockService, IVoidMockService
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly Action _action;
    private readonly IHookServiceFactory _hookServiceFactory;
    private readonly IHookBuilder _hookBuilder;

    public VoidMockService(IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder, MethodInfo originalMethodInfo, Action action)
        : base(hookServiceFactory, hookBuilder, originalMethodInfo, action)
    {
        _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
        _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _hookBuilder = hookBuilder ?? throw new ArgumentNullException(nameof(hookBuilder));
    }

    public void Callback(Action callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var callbackService = new CallbackService(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }
}