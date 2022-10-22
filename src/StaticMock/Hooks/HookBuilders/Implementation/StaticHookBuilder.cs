using System.Reflection;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;

namespace StaticMock.Hooks.HookBuilders.Implementation;

internal class StaticHookBuilder : IHookBuilder
{
    private readonly IReadOnlyList<ItParameterExpression> _itParameterExpressions;
    private readonly MethodInfo _originalMethodInfo;

    public StaticHookBuilder(MethodInfo originalMethodInfo, IReadOnlyList<ItParameterExpression> itParameterExpressions)
    {
        _itParameterExpressions = itParameterExpressions;
        _originalMethodInfo = originalMethodInfo;
    }

    public MethodInfo CreateVoidHook() =>
        HookBuilderHelper.CreateVoidHook(HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnHook(value, HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TArg, TReturn>(Func<TArg, TReturn> getValue) =>
        HookBuilderHelper.CreateReturnHook<TReturn>(
            _originalMethodInfo,
            getValue,
            HookMethodType.Static,
            _itParameterExpressions);

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new() =>
        HookBuilderHelper.CreateThrowsHook(exception, HookMethodType.Static, _itParameterExpressions);
}