using System.Reflection;
using StaticMock.Hooks.Entities;

namespace StaticMock.Helpers.Entities;

internal class MockSetupProperties
{
    public MethodInfo OriginalMethodInfo { get; set; } = null!;
    public HookParameter[] HookParameters { get; set; } = null!;
}