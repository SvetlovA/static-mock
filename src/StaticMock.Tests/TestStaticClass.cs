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

        public static Task TestMethodReturnTask()
        {
            return Task.CompletedTask;
        }

        public static async Task TestMethodReturnTaskAsync()
        {
            await Task.CompletedTask;
        }

        public static async void TestVoidMethodReturnTaskAsync()
        {
            await Task.CompletedTask;
        }

        public static async Task<int> TestMethodReturnTaskWithoutParametersAsync()
        {
            return await Task.FromResult(1);
        }

        public static Task<int> TestMethodReturnTaskWithoutParameters()
        {
            return Task.FromResult(1);
        }

        public static async Task TestMethodThrowsExceptionAsync()
        {
            throw new Exception("Test exception");
        }

        public static Task TestMethodThrowsException()
        {
            throw new Exception("Test exception");
        }

        public static int TestMethodReturnWithParameter(int parameter)
        {
            return parameter;
        }

        public static int StaticIntProperty { get; set; }
        public static object StaticObjectProperty { get; set; }
    }
}
