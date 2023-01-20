using HarmonyLib;
using StaticMock.Entities.Context;
using StaticMock.Hooks.HookBuilders.Entities;
using StaticMock.Hooks.HookBuilders.Helpers;

namespace StaticMock.Hooks.HookBuilders.Transpilers;

internal static class ThrowsHookTranspiler<TException> where TException : Exception, new()
{
    public static TException Exception { get; set; } = default!;
    public static HookMethodType HookMethodType { get; set; }
    public static IReadOnlyList<ItParameterExpression> ItParameterExpressions { get; set; } = null!;

    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions) =>
        TranspilerBuilderHelper.GetThrowsHookCodeInstructions(
            Exception,
            HookMethodType,
            ItParameterExpressions);
}