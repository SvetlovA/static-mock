using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Hooks.Entities;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class AsyncFuncMock<TReturnValue> : FuncMock<Task<TReturnValue>>, IAsyncFuncMock<TReturnValue>
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly HookParameter[] _hookParameters;
    private readonly Action _action;

    public AsyncFuncMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        HookParameter[] hookParameters,
        Action action)
        : base(originalMethodInfo, hookManagerFactory, hookParameters, action)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _hookParameters = hookParameters;
        _action = action;
    }

    public void ReturnsAsync(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_originalMethodInfo, _hookManagerFactory, _hookParameters);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }

    public void CallbackAsync(Func<TReturnValue> callback)
    {
        var callbackService = new CallbackMock(_originalMethodInfo, _hookManagerFactory, _hookParameters);
        using (callbackService.CallbackAsync(callback))
        {
            _action();
        }
    }
}