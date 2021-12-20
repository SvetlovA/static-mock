using System;
using System.Reflection;

namespace StaticMock.Services.Hook.Implementation;

internal class HookServiceFactory : IHookServiceFactory
{
    public IHookService CreateHookService(MethodBase originalMethod) =>
        IntPtr.Size == sizeof(int)
            ? (IHookService) new HookServiceX32(originalMethod)
            : new HookServiceX64(originalMethod);
}