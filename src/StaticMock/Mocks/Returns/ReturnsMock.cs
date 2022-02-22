using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Hooks.Entities;
using StaticMock.Hooks.Helpers;

namespace StaticMock.Mocks.Returns;

internal class ReturnsMock<TValue> : IReturnsMock<TValue>
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly HookParameter[] _hookParameters;

    public ReturnsMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        HookParameter[] hookParameters)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _hookParameters = hookParameters;
    }

    public IReturnable Returns(TValue value) => InternalReturns(value);

    public IReturnable ReturnsAsync(TValue value) => InternalReturns(Task.FromResult(value));

    private IReturnable InternalReturns<TInternalValue>(TInternalValue value)
    {
        var hook = HookBuilder.CreateReturnHook(value, _hookParameters);

        var hookManager = _hookManagerFactory.CreateHookService(_originalMethodInfo);
        return hookManager.ApplyHook(hook);
    }
}