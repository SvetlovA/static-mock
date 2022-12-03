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

    public MethodInfo CreateReturnHook<TReturn>(Func<TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
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
        HookBuilderHelper.CreateReturnAsyncHook(value, HookMethodType.Instance, _itParameterExpressions);

    public MethodInfo CreateReturnAsyncHook<TReturn>(Func<TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg, TReturn>(Func<TArg, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TReturn>(Func<TArg1, TArg2, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TArg3, TReturn>(Func<TArg1, TArg2, TArg3, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TArg3, TArg4, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateReturnAsyncHook<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TReturn> getValue)
    {
        HookValidationHelper.Validate(_originalMethodInfo, getValue);
        return CreateReturnAsyncHookInternal<TReturn>(getValue);
    }

    public MethodInfo CreateThrowsHook<TException>(TException exception) where TException : Exception, new() =>
        HookBuilderHelper.CreateThrowsHook(exception, HookMethodType.Instance, _itParameterExpressions);

    private MethodInfo CreateReturnHookInternal<TReturn>(object getValue) =>
        HookBuilderHelper.CreateReturnHook<TReturn>(
            _originalMethodInfo,
            getValue,
            HookMethodType.Instance,
            _itParameterExpressions);

    private MethodInfo CreateReturnAsyncHookInternal<TReturn>(object getValue) =>
        HookBuilderHelper.CreateReturnAsyncHook<TReturn>(
            _originalMethodInfo,
            getValue,
            HookMethodType.Instance,
            _itParameterExpressions);
}