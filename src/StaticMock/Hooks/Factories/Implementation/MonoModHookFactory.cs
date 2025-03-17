using System;
using System.Reflection;
using MonoMod.RuntimeDetour;
using StaticMock.Hooks.Entities;
#if !NETFRAMEWORK
using MonoMod.Core;
#endif

namespace StaticMock.Hooks.Factories.Implementation;

internal class MonoModHookFactory : IHookFactory
{
    private readonly MethodBase _originalMethod;
    private readonly HookSettings _settings;

    public MonoModHookFactory(MethodBase originalMethod, HookSettings settings)
    {
        _originalMethod = originalMethod;
        _settings = settings;
    }

    public IDisposable? CreateHook(MethodInfo transpiler)
    {
#if NETFRAMEWORK
        if (!_originalMethod.IsStatic && _settings.OriginalMethodCallInstance == null)
        {
            throw new Exception($"Can't take calling instance of {_originalMethod} to create hook");
        }
        return new Hook(_originalMethod, transpiler, _originalMethod.IsStatic ? null : _settings.OriginalMethodCallInstance);
#else
        return DetourFactory.Current.CreateDetour(_originalMethod, transpiler);
#endif
    }
}