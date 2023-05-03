using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;

namespace StaticMock.Hooks.HookBuilders.Transpilers;

internal static class ReturnHookTranspiler
{
    public static MethodInfo OriginalMethodInfo { get; set; } = null!;
    public static object GetValue { get; set; } = null!;
    public static HookMethodType HookMethodType { get; set; }
    public static IReadOnlyList<ItParameterExpression> ItParameterExpressions { get; set; } = null!;

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions) =>
        TranspilerBuilderHelper.GetReturnHookCodeInstructions(
            OriginalMethodInfo,
            GetValue,
            HookMethodType,
            ItParameterExpressions);
}

internal static class ReturnHookTranspiler<TReturn>
{
    public static TReturn ReturnValue { get; set; } = default!;
    public static HookMethodType HookMethodType { get; set; }
    public static IReadOnlyList<ItParameterExpression> ItParameterExpressions { get; set; } = null!;

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions) =>
        TranspilerBuilderHelper.GetReturnHookCodeInstructions(
            ReturnValue,
            HookMethodType,
            ItParameterExpressions);
}