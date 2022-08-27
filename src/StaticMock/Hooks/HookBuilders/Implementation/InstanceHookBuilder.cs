using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;
using System.Reflection;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class InstanceHookBuilder : IHookBuilder
{
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;

    public InstanceHookBuilder(IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _itParameterExpressions = itParameterExpressions;
    }

    public MethodInfo CreateVoidHook() =>
        HookBuilderHelper.CreateVoidHook(HookMethodType.Instance, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnHook(value, HookMethodType.Instance, _itParameterExpressions);

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new() =>
        HookBuilderHelper.CreateThrowsHook(exception, HookMethodType.Instance, _itParameterExpressions);
}