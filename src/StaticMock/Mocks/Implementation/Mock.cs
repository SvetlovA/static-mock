using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks;
using StaticMock.Mocks.Throws;

namespace StaticMock.Mocks.Implementation;

internal class Mock : IMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly Action _action;
    private readonly IHookManagerFactory _hookManagerFactory;
    private readonly SetupContextState _setupContextState;

    public Mock(
        IHookManagerFactory hookManagerFactory,
        MethodInfo originalMethodInfo,
        SetupContextState setupContextState,
        Action action)
    {
        _hookManagerFactory = hookManagerFactory;
        _originalMethodInfo = originalMethodInfo;
        _setupContextState = setupContextState;
        _action = action;
    }

    public void Throws(Type exceptionType) => ThrowsInternal(exceptionType, null);

    public void Throws(Type exceptionType, params object[] constructorArgs)
    {
        if (constructorArgs == null || !constructorArgs.Any())
        {
            throw new Exception(
                "Constructor args can't be null or empty. Try throws exception with parameterless constructor or put constructor args you want to use");
        }

        ThrowsInternal(exceptionType, constructorArgs);
    }

    public void Throws<TException>() where TException : Exception, new()
    {
        var throwService = new ThrowsMock(_originalMethodInfo, _hookManagerFactory, _setupContextState);
        using (throwService.Throws<TException>())
        {
            _action();
        }
    }

    public void Throws<TException>(object[] constructorArgs) where TException : Exception
    {
        if (constructorArgs == null || !constructorArgs.Any())
        {
            throw new Exception(
                "Constructor args can't be null or empty. Try throws exception with parameterless constructor or put constructor args you want to use");
        }

        ThrowsInternal(typeof(TException), constructorArgs);
    }

    private void ThrowsInternal(Type exceptionType, object[]? constructorArgs)
    {
        var throwService = new ThrowsMock(_originalMethodInfo, _hookManagerFactory, _setupContextState);
        using (throwService.Throws(exceptionType, constructorArgs))
        {
            _action();
        }
    }
}