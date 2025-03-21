using System;
using System.Reflection;
using StaticMock.Hooks.Factories;

namespace StaticMock.Hooks.Implementation;

internal class MonoModHookManager : IHookManager
{
    private readonly IHookFactory _hookFactory;

    private IDisposable? _hook;

    public MonoModHookManager(IHookFactory hookFactory)
    {
        _hookFactory = hookFactory;
    }

    public IReturnable ApplyHook(MethodInfo transpiler)
    {
        _hook = _hookFactory.CreateHook(transpiler);
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