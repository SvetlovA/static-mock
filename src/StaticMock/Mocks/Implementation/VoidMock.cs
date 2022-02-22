using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Hooks.Entities;
using StaticMock.Mocks.Callback;

namespace StaticMock.Mocks.Implementation;

internal class VoidMock : Mock, IVoidMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly HookParameter[] _hookParameters;
    private readonly Action _action;

    public VoidMock(
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

    public void Callback(Action callback)
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
}