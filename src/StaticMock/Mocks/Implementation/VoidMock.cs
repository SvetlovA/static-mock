using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks;
using StaticMock.Mocks.Callback;

namespace StaticMock.Mocks.Implementation;

internal class VoidMock : Mock, IVoidMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly SetupContextState _setupContextState;
    private readonly Action _action;

    public VoidMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        SetupContextState setupContextState,
        Action action)
        : base(hookManagerFactory, originalMethodInfo, setupContextState, action)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _setupContextState = setupContextState;
        _action = action;
    }

    public void Callback(Action callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        var callbackService = new CallbackMock(_originalMethodInfo, _hookManagerFactory, _setupContextState);
        using (callbackService.Callback(callback))
        {
            _action();
        }
    }
}