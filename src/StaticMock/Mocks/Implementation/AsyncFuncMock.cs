using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class AsyncFuncMock<TReturnValue> : FuncMock<Task<TReturnValue>>, IAsyncFuncMock<TReturnValue>
{
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly MethodInfo _originalMethodInfo;
    private readonly Action _action;

    public AsyncFuncMock(IHookManagerFactory hookManagerFactory, MethodInfo originalMethodInfo, Action action)
        : base(hookManagerFactory, originalMethodInfo, action)
    {
        _hookManagerFactory = hookManagerFactory;
        _originalMethodInfo = originalMethodInfo;
        _action = action;
    }

    public void ReturnsAsync(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_originalMethodInfo, _hookManagerFactory);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }

    public void CallbackAsync(Func<TReturnValue> callback)
    {
        var callbackService = new CallbackMock(_originalMethodInfo, _hookManagerFactory);
        using (callbackService.CallbackAsync(callback))
        {
            _action();
        }
    }
}