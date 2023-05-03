using System;
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

    /// <summary>
    /// Types of mocking method parameters.
    /// </summary>
    public Type[]? MethodParametersTypes { get; set; }

    /// <summary>
    /// Instance of testing class, if you are testing instance method or property
    /// </summary>
    public object? Instance { get; set; }
}