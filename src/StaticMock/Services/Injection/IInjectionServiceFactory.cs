using System.Reflection;

namespace StaticMock.Services.Injection
{
    internal interface IInjectionServiceFactory
    {
        IInjectionService CreateInjectionService(MethodBase method);
    }
}
