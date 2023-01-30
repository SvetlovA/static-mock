using System.Reflection;
using HarmonyLib;

namespace StaticMock.Hooks.Entities;

internal class Patch
{
    public PatchProcessor? PatchProcessor { get; set; }
    public MethodInfo? Transpiler { get; set; }
    public string? HarmonyId { get; set; }
}