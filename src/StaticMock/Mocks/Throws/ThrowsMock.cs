using System.Reflection;
using StaticMock.Hooks;
using StaticMock.Hooks.Helpers;

namespace StaticMock.Mocks.Throws;

internal class ThrowsMock : IThrowsMock
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookManagerFactory _hookManagerFactory;

    public ThrowsMock(MethodInfo originalMethodInfo, IHookManagerFactory hookManagerFactory)
    {
        _originalMethodInfo = originalMethodInfo;
        _hookManagerFactory = hookManagerFactory;
    }

    public IReturnable Throws(Type exceptionType, params object[]? constructorArgs) => ThrowsInternal(exceptionType, constructorArgs);

    public IReturnable Throws<TException>() where TException : Exception, new()
    {
        var hook = HookBuilder.CreateThrowsHook(new TException());

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

        var hook = HookBuilder.CreateThrowsHook(hookException);

        return Inject(hook);
    }

    private IReturnable Inject(MethodBase methodToInject)
    {
        var hookManager = _hookManagerFactory.CreateHookService(_originalMethodInfo);
        return hookManager.ApplyHook(methodToInject);
    }
}