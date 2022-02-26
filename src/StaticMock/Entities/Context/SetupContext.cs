namespace StaticMock.Entities.Context;

public class SetupContext
{
    internal SetupContextState State = new();

    public It It => new(State);
}