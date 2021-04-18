using System.Threading.Tasks;

namespace StaticMock.Tests
{
    public static class TestStaticClass
    {
        public static int TestMethodReturn1WithoutParameters()
        {
            return 1;
        }

        public static int TestMethodReturnParameter(int parameter)
        {
            return parameter;
        }

        public static async Task<int> TestMethodReturn1WithoutParametersAsync()
        {
            return await Task.FromResult(1);
        }

        public static TestInstance TestMethodReturnInstanceObject()
        {
            return new();
        }
    }
}
