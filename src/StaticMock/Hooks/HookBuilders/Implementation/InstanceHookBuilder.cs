using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;
using System.Reflection;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class InstanceHookBuilder : IHookBuilder
{
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;
    private readonly MethodInfo _originalMethodInfo;

    public InstanceHookBuilder(MethodInfo originalMethodInfo, IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _itParameterExpressions = itParameterExpressions;
        _originalMethodInfo = originalMethodInfo;
    }

    public MethodInfo CreateVoidHook() =>
        HookBuilderHelper.CreateVoidHook(HookMethodType.Instance, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnHook(value, HookMethodType.Instance, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TArg, TReturn>(Func<TArg, TReturn> getValue) =>
        HookBuilderHelper.CreateReturnHook<TReturn>(
            _originalMethodInfo,
            getValue,
            HookMethodType.Instance,
            _itParameterExpressions);

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new() =>
        HookBuilderHelper.CreateThrowsHook(exception, HookMethodType.Instance, _itParameterExpressions);
}