using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Mocks.Callback;

namespace StaticMock.Mocks.Implementation;

internal class VoidMock : Mock, IVoidMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly Action _action;
    private readonly IHookManagerFactory _hookManagerFactory;

    public VoidMock(IHookManagerFactory hookManagerFactory, MethodInfo originalMethodInfo, Action action)
        : base(hookManagerFactory, originalMethodInfo, action)
    {
        _hookManagerFactory = hookManagerFactory;
        _originalMethodInfo = originalMethodInfo;
        _action = action;
    }

    public void Callback(Action callback)
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
}