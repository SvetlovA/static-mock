namespace StaticMock.Entities.Context;

internal class SetupContext
{
    public SetupContextState State = new();

    public It It => new(State);
}