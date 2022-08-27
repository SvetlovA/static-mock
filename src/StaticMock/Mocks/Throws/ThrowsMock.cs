using StaticMock.Hooks;
using StaticMock.Hooks.HookBuilders;

namespace StaticMock.Mocks.Throws;

internal class ThrowsMock : IThrowsMock
{
    private readonly IHookBuilder _hookBuilder;
    private readonly IHookManager _hookManager;

    public ThrowsMock(
        IHookBuilder hookBuilder,
        IHookManager hookManager)
    {
        _hookBuilder = hookBuilder;
        _hookManager = hookManager;
    }

    public IReturnable Throws(Type exceptionType, params object[]? constructorArgs) =>
        ThrowsInternal(exceptionType, constructorArgs);

    public IReturnable Throws<TException>() where TException : Exception, new()
    {
        var hook = _hookBuilder.CreateThrowsHook(new TException());

        return _hookManager.ApplyHook(hook);
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

        var hook = _hookBuilder.CreateThrowsHook(hookException);

        return _hookManager.ApplyHook(hook);
    }
}