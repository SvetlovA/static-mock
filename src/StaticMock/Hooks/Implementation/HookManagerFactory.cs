using System.Reflection;
using System.Runtime.InteropServices;
using StaticMock.Entities.Enums;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Implementation;

internal class HookManagerFactory : IHookManagerFactory
{
    private const string NetFrameworkName = ".NET Framework";

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
            HookManagerType.MonoMod => CreateMonoModHookManager(_originalMethod, _settings),
            HookManagerType.Harmony => new HarmonyHookManager(_originalMethod),
            _ => throw new ArgumentOutOfRangeException(nameof(_settings.HookManagerType), _settings.HookManagerType,
                $"{_settings.HookManagerType} not exists in {nameof(HookManagerType)}")
        };

    private static IHookManager CreateMonoModHookManager(MethodBase originalMethod, HookSettings settings)
    {
        if (RuntimeInformation.FrameworkDescription.Contains(NetFrameworkName, StringComparison.OrdinalIgnoreCase))
        {
            return new MonoModHookManager(originalMethod, settings);
        }

        return new MonoModReorgHookManager(originalMethod, settings);
    }
}