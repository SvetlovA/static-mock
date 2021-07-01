using System.Reflection;

namespace StaticMock.Services.Hook
{
    internal interface IHookServiceFactory
    {
        IHookService CreateHookService(MethodBase originalMethod);
    }
}
