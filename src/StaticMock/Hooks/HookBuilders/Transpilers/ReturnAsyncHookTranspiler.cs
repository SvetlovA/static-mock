using System.Reflection;
using HarmonyLib;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;

namespace StaticMock.Hooks.HookBuilders.Transpilers;

internal static class ReturnAsyncHookTranspiler<TReturn>
{
    public static TReturn ReturnValue { get; set; } = default!;
    public static HookMethodType HookMethodType { get; set; }
    public static IReadOnlyList<ItParameterExpression> ItParameterExpressions { get; set; } = null!;

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions) =>
        TranspilerBuilderHelper.GetReturnAsyncHookCodeInstructions(
            ReturnValue,
            HookMethodType,
            ItParameterExpressions);
}