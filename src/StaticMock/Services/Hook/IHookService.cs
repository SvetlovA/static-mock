using System.Reflection;
using StaticMock.Services.Common;

namespace StaticMock.Services.Hook;

internal interface IHookService : IReturnable
{
    IReturnable Hook(MethodBase hookMethod);
}