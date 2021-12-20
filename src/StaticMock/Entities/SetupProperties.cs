using System.Reflection;

namespace StaticMock.Entities;

public class SetupProperties
{
    /// <summary>
    /// Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.
    /// </summary>
    public BindingFlags? BindingFlags { get; set; }

    /// <summary>
    /// Generic types to set for generic method. Length and order must be like count and order of generics in method.
    /// </summary>
    public Type[]? GenericTypes { get; set; }
}