using System.Reflection;

namespace StaticMock.Hooks.Entities;

internal class HookParameter
{
    public Type Type { get; set; } = null!;
    public MethodInfo? Hook { get; set; }
}