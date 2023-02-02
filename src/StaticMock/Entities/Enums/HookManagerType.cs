namespace StaticMock.Entities.Enums;

public enum HookManagerType
{
    /// <summary>
    /// Harmony implementation of hook manger
    /// </summary>
    Harmony = 0,
    /// <summary>
    /// Native implementation of hook manager
    /// </summary>
    Native = 1,
    /// <summary>
    /// MonoMod implementation of hook manager
    /// </summary>
    MonoMod = 2
}