using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;

namespace StaticMock.Hooks.HookBuilders.Transpilers;

internal static class CallbackHookTranspiler
{
    public static MethodInfo OriginalMethodInfo { get; set; } = null!;
    public static object Callback { get; set; } = null!;
    public static HookMethodType HookMethodType { get; set; }
    public static IReadOnlyList<ItParameterExpression> ItParameterExpressions { get; set; } = null!;

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions) =>
        TranspilerBuilderHelper.GetCallbackHookInstructions(
            OriginalMethodInfo,
            Callback,
            HookMethodType,
            ItParameterExpressions);
}