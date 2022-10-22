using System.Reflection;

namespace StaticMock.Hooks.HookBuilders;

internal interface IHookBuilder
{
    MethodInfo CreateVoidHook();
    MethodInfo CreateReturnHook<TReturn>(TReturn value);
    MethodInfo CreateReturnHook<TArg, TReturn>(Func<TArg, TReturn> getValue);
    MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new();
}