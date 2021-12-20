using System.Reflection;
using StaticMock.Services.Callback;
using StaticMock.Services.Hook;
using StaticMock.Services.Returns;

namespace StaticMock.Services.Mock.Implementation;

internal class AsyncFuncMockService<TReturnValue> : FuncMockService<Task<TReturnValue>>, IAsyncFuncMockService<TReturnValue>
{
    private readonly IHookServiceFactory _hookServiceFactory;
    private readonly IHookBuilder _hookBuilder;
    private readonly MethodInfo _originalMethodInfo;
    private readonly Action _action;

    public AsyncFuncMockService(IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder, MethodInfo originalMethodInfo, Action action)
        : base(hookServiceFactory, hookBuilder, originalMethodInfo, action)
    {
        _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
        _hookBuilder = hookBuilder ?? throw new ArgumentNullException(nameof(hookBuilder));
        _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }

    public void ReturnsAsync(TReturnValue value)
    {
        var returnService = new ReturnsMockService<TReturnValue>(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }

    public void CallbackAsync(Func<TReturnValue> callback)
    {
        var callbackService = new CallbackService(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
        using (callbackService.CallbackAsync(callback))
        {
            _action();
        }
    }
}