using System.Reflection;
using StaticMock.Entities.Context;

namespace StaticMock.Helpers.Entities;

internal class MockSetupProperties
{
    public MethodInfo OriginalMethodInfo { get; set; } = null!;
    public SetupContextState SetupContextState { get; set; } = null!;
    public object? OriginalMethodCallInstance { get; set; }
}