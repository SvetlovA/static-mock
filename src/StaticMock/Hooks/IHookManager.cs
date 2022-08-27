using System.Reflection;

namespace StaticMock.Hooks;

internal interface IHookManager : IReturnable
{
    IReturnable ApplyHook(MethodBase hookMethod);
}