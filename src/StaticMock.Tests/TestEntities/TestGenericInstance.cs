namespace StaticMock.Tests.TestEntities
{
    public class TestGenericInstance<TEntity>
    {
        public TEntity GenericTestMethodReturnDefaultWithoutParameters()
        {
            return default;
        }
    }
}
