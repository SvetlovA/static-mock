namespace StaticMock.Tests
{
    public class TestGenericInstance<TEntity>
    {
        public TEntity GenericTestMethodReturnDefaultWithoutParameters()
        {
            return default;
        }
    }
}
