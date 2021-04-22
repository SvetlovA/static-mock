using System.Reflection;

namespace StaticMock.Services.Injection
{
    internal interface IInjectionService : IReturnable
    {
        IReturnable Inject(MethodBase methodToInject);
    }
}
