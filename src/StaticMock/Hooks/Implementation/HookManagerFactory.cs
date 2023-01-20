using System.Reflection;

namespace StaticMock.Hooks.Implementation;

internal class HookManagerFactory : IHookManagerFactory
{
    private readonly MethodBase _originalMethod;

    public HookManagerFactory(MethodBase originalMethod)
    {
        _originalMethod = originalMethod;
    }

    public IHookManager CreateHookManager() => new HarmonyHookManager(_originalMethod);
    //IntPtr.Size == sizeof(int)
    //    ? (IHookManager) new HookManagerX32(_originalMethod)
    //    : new HookManagerX64(_originalMethod);
}