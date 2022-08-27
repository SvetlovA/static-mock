using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;
using StaticMock.Mocks.Throws;

namespace StaticMock.Mocks.Implementation;

internal class Mock : IMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;
    private readonly Action _action;

    public Mock(
        IHookBuilder hookBuilder,
        IHookManager hookManager,
        Action action)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
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
        var throwService = new ThrowsMock(_hookBuilder, _hookManager);
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
        var throwService = new ThrowsMock(_hookBuilder, _hookManager);
        using (throwService.Throws(exceptionType, constructorArgs))
        {
            _action();
        }
    }
}