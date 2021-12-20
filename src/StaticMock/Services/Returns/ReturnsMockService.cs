using System.Reflection;
using StaticMock.Services.Common;
using StaticMock.Services.Hook;

namespace StaticMock.Services.Returns;

internal class ReturnsMockService<TValue> : IReturnsMockService<TValue>
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly IHookServiceFactory _hookServiceFactory;
    private readonly IHookBuilder _hookBuilder;

    public ReturnsMockService(MethodInfo originalMethodInfo, IHookServiceFactory hookServiceFactory, IHookBuilder hookBuilder)
    {
        _originalMethodInfo = originalMethodInfo ?? throw new ArgumentNullException(nameof(originalMethodInfo));
        _hookServiceFactory = hookServiceFactory ?? throw new ArgumentNullException(nameof(hookServiceFactory));
        _hookBuilder = hookBuilder ?? throw new ArgumentNullException(nameof(hookBuilder));
    }

    public IReturnable Returns(TValue value) => InternalReturns(value);

    public IReturnable ReturnsAsync(TValue value) => InternalReturns(Task.FromResult(value));

    private IReturnable InternalReturns<TInternalValue>(TInternalValue value)
    {
        var hook = _hookBuilder.CreateReturnHook(value);

        var injectionService = _hookServiceFactory.CreateHookService(_originalMethodInfo);
        return injectionService.Hook(hook);
    }
}