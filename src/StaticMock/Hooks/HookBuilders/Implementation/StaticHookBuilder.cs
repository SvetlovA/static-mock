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

    public MethodInfo CreateVoidHook(Action action) =>
        HookBuilderHelper.CreateReturnHook(
            _originalMethodInfo,
            action,
            HookMethodType.Static,
            _itParameterExpressions,
            typeof(void));

    public MethodInfo CreateReturnHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnHook(value, HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateReturnHook<TReturn>(Func<TReturn> getValue)
    {
        HookValidationHelper.ValidateReturnType(_originalMethodInfo.ReturnType, getValue.Method.ReturnType);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg, TReturn>(Func<TArg, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TReturn>(Func<TArg1, TArg2, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TReturn>(Func<TArg1, TArg2, TArg3, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TReturn>(TReturn value) =>
        HookBuilderHelper.CreateReturnAsyncHook(value, HookMethodType.Static, _itParameterExpressions);

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new() =>
        HookBuilderHelper.CreateThrowsHook(exception, HookMethodType.Static, _itParameterExpressions);

    private MethodInfo CreateReturnHookInternal<TReturn>(object getValue) =>
        HookBuilderHelper.CreateReturnHook<TReturn>(
            _originalMethodInfo,
            getValue,
            HookMethodType.Static,
            _itParameterExpressions);
}