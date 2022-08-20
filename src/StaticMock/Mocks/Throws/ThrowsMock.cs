using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks;
using StaticMock.Hooks.Helpers;

namespace StaticMock.Mocks.Throws;

internal class ThrowsMock : IThrowsMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly SetupContextState _setupContextState;

    public ThrowsMock(
        MethodInfo originalMethodInfo,
        IHookManagerFactory hookManagerFactory,
        SetupContextState setupContextState)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
        _setupContextState = setupContextState;
    }

    public IReturnable Throws(Type exceptionType, params object[]? constructorArgs) =>
        ThrowsInternal(exceptionType, constructorArgs);

    public IReturnable Throws<TException>() where TException : Exception, new()
    {
        var hook = HookBuilder.CreateThrowsHook(new TException(), _setupContextState.ItParameterExpressions);

        return Inject(hook);
    }

    private IReturnable ThrowsInternal(Type exceptionType, object[]? constructorArgs)
    {
        if (exceptionType == null)
        {
            throw new ArgumentNullException(nameof(exceptionType));
        }

        var hookException = Activator.CreateInstance(exceptionType, constructorArgs) as Exception;

        if (hookException == null)
        {
            throw new Exception($"{exceptionType.FullName} is not an Exception");
        }

        var hook = HookBuilder.CreateThrowsHook(hookException, _setupContextState.ItParameterExpressions);

        return Inject(hook);
    }

    private IReturnable Inject(MethodBase methodToInject)
    {
        var hookManager = _hookManagerFactory.CreateHookService(_originalMethodInfo);
        return hookManager.ApplyHook(methodToInject);
    }
}