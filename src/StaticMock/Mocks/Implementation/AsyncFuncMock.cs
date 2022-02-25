using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks;
using StaticMock.Hooks.Entities;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class AsyncFuncMock<TReturnValue> : FuncMock<Task<TReturnValue>>, IAsyncFuncMock<TReturnValue>
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly SetupContextState _setupContextState;
    private readonly Action _action;

    public AsyncFuncMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        SetupContextState setupContextState,
        Action action)
        : base(originalMethodInfo, hookManagerFactory, setupContextState, action)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _setupContextState = setupContextState;
        _action = action;
    }

    public void ReturnsAsync(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_originalMethodInfo, _hookManagerFactory, _setupContextState);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }

    public void CallbackAsync(Func<TReturnValue> callback)
    {
        var callbackService = new CallbackMock(_originalMethodInfo, _hookManagerFactory, _setupContextState);
        using (callbackService.CallbackAsync(callback))
        {
            _action();
        }
    }
}