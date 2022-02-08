using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Mocks.Callback;
using StaticMock.Mocks.Returns;

namespace StaticMock.Mocks.Implementation;

internal class FuncMock<TReturn> : Mock, IFuncMock, IFuncMock<TReturn>
{
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly MethodInfo _originalMethodInfo;
    private readonly Action _action;

    public FuncMock(IHookManagerFactory hookManagerFactory, MethodInfo originalMethodInfo, Action action)
        : base(hookManagerFactory, originalMethodInfo, action)
    {
        _hookManagerFactory = hookManagerFactory;
        _originalMethodInfo = originalMethodInfo;
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

        var callbackService = new CallbackMock(_originalMethodInfo, _hookManagerFactory);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }

    public void Returns<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_originalMethodInfo, _hookManagerFactory);
        using (returnService.Returns(value))
        {
            _action();
        }
    }

    public void ReturnsAsync<TReturnValue>(TReturnValue value)
    {
        var returnService = new ReturnsMock<TReturnValue>(_originalMethodInfo, _hookManagerFactory);
        using (returnService.ReturnsAsync(value))
        {
            _action();
        }
    }
}