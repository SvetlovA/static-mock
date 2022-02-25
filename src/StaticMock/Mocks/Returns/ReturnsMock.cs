using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks;
using StaticMock.Hooks.Helpers;

namespace StaticMock.Mocks.Returns;

internal class ReturnsMock<TValue> : IReturnsMock<TValue>
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly SetupContextState _setupContextState;

    public ReturnsMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        SetupContextState setupContextState)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _setupContextState = setupContextState;
    }

    public IReturnable Returns(TValue value) => InternalReturns(value);

    public IReturnable ReturnsAsync(TValue value) => InternalReturns(Task.FromResult(value));

    private IReturnable InternalReturns<TInternalValue>(TInternalValue value)
    {
        var hook = HookBuilder.CreateReturnHook(value, _setupContextState.ItParameterExpressions);

        var hookManager = _hookManagerFactory.CreateHookService(_originalMethodInfo);
        return hookManager.ApplyHook(hook);
    }
}