using System;
using System.Reflection;

namespace StaticMock.Services.Injection.Implementation
{
    internal class InjectionServiceFactory : IInjectionServiceFactory
    {
        public IInjectionService CreateInjectionService(MethodBase method) =>
             IntPtr.Size == sizeof(int)
                ? (IInjectionService) new InjectionServiceX32(method)
                : new InjectionServiceX64(method);
    }
}
