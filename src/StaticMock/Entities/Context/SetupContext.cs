namespace StaticMock.Entities.Context;

public class SetupContext
{
    internal SetupContextState State { get; set; } = new();

    public It It => new(State);
}