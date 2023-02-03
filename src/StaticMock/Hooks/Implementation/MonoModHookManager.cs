using System.Reflection;
using MonoMod.RuntimeDetour;

namespace StaticMock.Hooks.Implementation;

internal class MonoModHookManager : IHookManager
{
    private readonly MethodBase _originalMethod;

    private IDetour? _hook;

    public MonoModHookManager(MethodBase originalMethod)
    {
        _originalMethod = originalMethod;
    }

    public IReturnable ApplyHook(MethodInfo transpiler)
    {
        _hook = new Detour(_originalMethod, transpiler);

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