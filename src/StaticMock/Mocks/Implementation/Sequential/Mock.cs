using System;
using System.Linq;
using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Throws;

namespace StaticMock.Mocks.Implementation.Sequential;

internal class Mock : IMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public Mock(IHookBuilder hookBuilder, IHookManager hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IDisposable Throws(Type exceptionType) => ThrowsInternal(exceptionType, null);

    public IDisposable Throws(Type exceptionType, params object[] constructorArgs)
    {
        if (constructorArgs == null || !constructorArgs.Any())
        {
            throw new Exception(
                "Constructor args can't be null or empty. Try throws exception with parameterless constructor or put constructor args you want to use");
        }

        return ThrowsInternal(exceptionType, constructorArgs);
    }

    public IDisposable Throws<TException>() where TException : Exception, new() =>
        new ThrowsMock(_hookBuilder, _hookManager).Throws<TException>();

    public IDisposable Throws<TException>(object[] constructorArgs) where TException : Exception
    {
        if (constructorArgs == null || !constructorArgs.Any())
        {
            throw new Exception(
                "Constructor args can't be null or empty. Try throws exception with parameterless constructor or put constructor args you want to use");
        }

        return ThrowsInternal(typeof(TException), constructorArgs);
    }

    private IDisposable ThrowsInternal(Type exceptionType, object[]? constructorArgs) =>
        new ThrowsMock(_hookBuilder, _hookManager).Throws(exceptionType, constructorArgs);
}