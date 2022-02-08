using System.Reflection;

namespace StaticMock.Hooks.Implementation;

internal class HookManagerFactory : IHookManagerFactory
{
    public IHookManager CreateHookService(MethodBase originalMethod) =>
        IntPtr.Size == sizeof(int)
            ? (IHookManager) new HookManagerX32(originalMethod)
            : new HookManagerX64(originalMethod);
}