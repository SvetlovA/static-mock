using System;
using System.Reflection;
using StaticMock.Entities.Enums;
using StaticMock.Hooks.Entities;
using StaticMock.Hooks.Implementation;

namespace StaticMock.Hooks.Factories.Implementation;

internal class HookManagerFactory : IHookManagerFactory
{
    private readonly MethodBase _originalMethod;
    private readonly HookSettings _settings;

    public HookManagerFactory(MethodBase originalMethod, HookSettings settings)
    {
        _originalMethod = originalMethod;
        _settings = settings;
    }

    public IHookManager CreateHookManager() =>
        _settings.HookManagerType switch
        {
            HookManagerType.MonoMod => new MonoModHookManager(new MonoModHookFactory(_originalMethod, _settings)),
            _ => throw new ArgumentOutOfRangeException(nameof(_settings.HookManagerType), _settings.HookManagerType,
                $"{_settings.HookManagerType} not exists in {nameof(HookManagerType)}")
        };
}