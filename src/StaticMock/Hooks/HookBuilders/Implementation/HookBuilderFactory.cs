using System;
using System.Reflection;
using StaticMock.Entities.Enums;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class HookBuilderFactory : IHookBuilderFactory
{
    private readonly MethodInfo _originalMethodInfo;
    private readonly HookSettings _settings;

    public HookBuilderFactory(MethodInfo originalMethodInfo, HookSettings settings)
    {
        _originalMethodInfo = originalMethodInfo;
        _settings = settings;
    }

    public IHookBuilder CreateHookBuilder() =>
        _settings.HookManagerType switch
        {
            HookManagerType.MonoMod => _originalMethodInfo.IsStatic
                ? new StaticHookBuilder(_originalMethodInfo, _settings.ItParameterExpressions)
                : new InstanceHookBuilder(_originalMethodInfo, _settings.ItParameterExpressions),
            _ => throw new ArgumentOutOfRangeException(nameof(_settings.HookManagerType), _settings.HookManagerType,
                $"{_settings.HookManagerType} not exists in {nameof(HookManagerType)}")
        };
}