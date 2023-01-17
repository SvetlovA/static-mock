namespace StaticMock.Tests.Common.TestEntities;

public class TestGenericInstance<TEntity>
{
    public TEntity GenericTestMethodReturnDefaultWithoutParameters()
    {
        return default;
    }
}