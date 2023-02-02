using StaticMock.Entities.Enums;

namespace StaticMock.Entities;

public class GlobalSettings
{
    /// <summary>
    /// Hook manager type for selecting implementation of hook manager, default is Harmony implementation
    /// </summary>
    public HookManagerType HookManagerType { get; set; } = HookManagerType.Harmony;
}