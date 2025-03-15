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
        if (!_originalMethod.IsStatic && _settings.OriginalMethodCallInstance == null)
        {
            throw new Exception($"Can't take calling instance of {_originalMethod} to create hook");
        }
        
#if NETFRAMEWORK
        return new Hook(_originalMethod, transpiler, _originalMethod.IsStatic ? null : _settings.OriginalMethodCallInstance);
#else
        if (_originalMethod.IsGenericMethod ||
            _originalMethod.DeclaringType!.IsGenericType ||
            _originalMethod.IsStatic)
        {
            return DetourFactory.Current.CreateDetour(_originalMethod, transpiler);
        }

        return new Hook(_originalMethod, transpiler, _settings.OriginalMethodCallInstance);
#endif
    }
}