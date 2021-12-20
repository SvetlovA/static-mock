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


    public void Throws(Type exceptionType)
    {
        var throwService = new ThrowsService(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
        using (throwService.Throws(exceptionType))
        {
            _action();
        }
    }

    public void Throws<TException>() where TException : Exception, new()
    {
        var throwService = new ThrowsService(_originalMethodInfo, _hookServiceFactory, _hookBuilder);
        using (throwService.Throws<TException>())
        {
            _action();
        }
    }
}