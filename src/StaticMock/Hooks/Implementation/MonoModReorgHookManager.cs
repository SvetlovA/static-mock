extern alias MonoModReorg;

using System.Reflection;
using MonoModReorg::MonoMod.RuntimeDetour;
using StaticMock.Hooks.Entities;

namespace StaticMock.Hooks.Implementation;

internal class MonoModReorgHookManager : IHookManager
{
    private readonly MethodBase _originalMethod;
    private readonly HookSettings _settings;

    private Hook? _hook;

    public MonoModReorgHookManager(MethodBase originalMethod, HookSettings settings)
    {
        _originalMethod = originalMethod;
        _settings = settings;
    }

    public IReturnable ApplyHook(MethodInfo transpiler)
    {
        if (_originalMethod.IsStatic)
        {
            _hook = new Hook(_originalMethod, transpiler);
        }
        else
        {
            if (_settings.OriginalMethodCallInstance != null)
            {
                _hook = new Hook(_originalMethod, transpiler, _settings.OriginalMethodCallInstance);
            }
            else
            {
                throw new Exception($"Can't take calling instance of {_originalMethod} to create hook");
            }
        }

        return this;
    }

    public void Return()
    {
        _hook?.Dispose();
    }

    public void Dispose()
    {
        Return();
    }
}