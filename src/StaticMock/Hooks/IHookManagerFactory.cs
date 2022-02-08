using System.Reflection;

namespace StaticMock.Hooks;

internal interface IHookManagerFactory
{
    IHookManager CreateHookService(MethodBase originalMethod);
}