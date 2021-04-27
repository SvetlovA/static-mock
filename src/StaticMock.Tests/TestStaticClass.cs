using System;
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

        public static TestInstance TestMethodReturnReferenceObject()
        {
            return new();
        }

        public static int TestMethodReturn1WithOutParameter(out int x)
        {
            x = 1;
            return 1;
        }

        public static int TestMethodReturn1WithRefParameter(ref int x)
        {
            x = 1;
            return 1;
        }

        public static void TestVoidMethodWithoutParameters()
        { }

        public static void TestVoidMethodWithoutParametersThrowsException()
        {
            throw new Exception("Test exception");
        }

        public static void TestVoidMethodWithParameters(int parameter)
        { }
    }
}
