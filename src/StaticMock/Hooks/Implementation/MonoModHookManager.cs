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
        if (_originalMethod.IsStatic)
        {
            _hook = new Hook(_originalMethod, transpiler);
        }
        else
        {
            var declaringType = _originalMethod.DeclaringType;
            var declaringInstance = Activator.CreateInstance(declaringType);
            _hook = new Hook(_originalMethod, transpiler, declaringInstance);
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