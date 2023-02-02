﻿using System.Reflection;
using StaticMock.Entities.Enums;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Implementation;

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
            HookManagerType.Harmony => new HarmonyHookManager(_originalMethod),
            HookManagerType.Native => IntPtr.Size == sizeof(int) ? new HookManagerX32(_originalMethod) : new HookManagerX64(_originalMethod),
            HookManagerType.MonoMod => new MonoModHookManager(_originalMethod),
            _ => throw new ArgumentOutOfRangeException(nameof(_settings.HookManagerType), _settings.HookManagerType,
                $"{_settings.HookManagerType} not exists in {nameof(HookManagerType)}")
        };
}