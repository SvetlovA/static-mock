using System;
using System.Linq;
using System.Reflection;
using StaticMock.Services.Hook;
using StaticMock.Services.Throws;

namespace StaticMock.Services.Mock.Implementation;

internal class MockService : IMockService
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly Action _action;
    private readonly IHookServiceFactory _hookServiceFactory;
    private readonly IHookBuilder _hookBuilder;

    public MockService(IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder, MethodInfo originalMethodInfo, Action action)
    {
        _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
        _hookBuilder = hookBuilder ?? throw new ArgumentNullException(nameof(hookBuilder));
        _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        _action = action ?? throw new ArgumentNullException(nameof(action));
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
        var throwService = new ThrowsService(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
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
        var throwService = new ThrowsService(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
        using (throwService.Throws(exceptionType, constructorArgs))
        {
            _action();
        }
    }
}