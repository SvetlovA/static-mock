using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class StaticHookBuilder : IHookBuilder
{
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;

    public StaticHookBuilder(IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _itParameterExpressions = itParameterExpressions;
    }

    public MethodInfo CreateVoidHook() =>
        HookBuilderHelper.CreateVoidHook(HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnHook(value, HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new() =>
        HookBuilderHelper.CreateThrowsHook(exception, HookMethodType.Static, _itParameterExpressions);
}