using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Hooks.Entities;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class FuncMock<TReturn> : Mock, IFuncMock, IFuncMock<TReturn>
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly HookParameter[] _hookParameters;
    private readonly Action _action;

    public FuncMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        HookParameter[] hookParameters,
        Action action)
        : base(hookManagerFactory, originalMethodInfo, action)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _hookParameters = hookParameters;
        _action = action;
    }

    public void Callback(Func<TReturn> callback)
    {
        Callback<TReturn>(callback);
    }

    public void Returns(TReturn value)
    {
        Returns<TReturn>(value);
    }

    public void Callback<TReturnValue>(Func<TReturnValue> callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var callbackService = new CallbackMock(_originalMethodInfo, _hookManagerFactory, _hookParameters);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Returns<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_originalMethodInfo, _hookManagerFactory, _hookParameters);
        using (returnService.Returns(value))
        {
            _action();
        }
    }

    public void ReturnsAsync<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_originalMethodInfo, _hookManagerFactory, _hookParameters);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }
}