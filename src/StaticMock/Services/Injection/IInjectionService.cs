using System.Reflection;
using StaticMock.Services.Common;

namespace StaticMock.Services.Injection
{
    internal interface IInjectionService : IReturnable
    {
        IReturnable Inject(MethodBase methodToInject);
    }
}
