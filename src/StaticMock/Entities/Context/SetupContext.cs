namespace StaticMock.Entities.Context;

/// <summary>
/// Provides context for setting up method mocks with parameter matching capabilities.
/// This class is used in mock expressions to access parameter matching utilities.
/// </summary>
public class SetupContext
{
    internal SetupContextState State { get; set; } = new();

    /// <summary>
    /// Gets the parameter matching utility that allows for flexible argument matching in mock setups.
    /// </summary>
    /// <value>An instance of <see cref="Context.It"/> that provides parameter matching methods like IsAny and Is.</value>
    /// <example>
    /// <code>
    /// // Use SetupContext to access parameter matching
    /// Mock.Setup((SetupContext context) => MyClass.ProcessData(context.It.IsAny&lt;string&gt;()));
    /// Mock.Setup((SetupContext context) => MyClass.ProcessNumber(context.It.Is&lt;int&gt;(x => x > 0)));
    /// </code>
    /// </example>
    public It It => new(State);
}