using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticMock.Tests.CallbackTests
{
    [TestFixture]
    public class GenericSetupMockCallbackTests
    {
        [Test]
        public void TestFuncCallback()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;
            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
            {
                var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(() =>
            {
                var x = expectedResult;
                return x;
            });
        }

        [Test]
        public void TestActionCallback()
        {
            static void DoSomething() { }

            Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException(), () =>
            {
                TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
            }).Callback(() =>
            {
                DoSomething();
            });
        }

        [Test]
        public async Task TestCallbackAsync()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(async () =>
            {
                return await Task.FromResult(2);
            });
        }

        [Test]
        public void TestFuncCallbackInstance()
        {
            var instance = new TestInstance();
            var originalResult = instance.TestMethodReturn1WithoutParameters();
            Assert.AreNotEqual(2, originalResult);

            Mock.Setup(() => instance.TestMethodReturn1WithoutParameters(), () =>
            {
                var actualResult = instance.TestMethodReturn1WithoutParameters();
                Assert.AreEqual(2, actualResult);
            }).Callback(() =>
            {
                return 2; // if not static scope you can't use closure
            });
        }

        [Test]
        public void TestActionCallbackInstance()
        {
            var instance = new TestInstance();

            static void DoSomething() { }

            Mock.Setup(() => instance.TestVoidMethodWithoutParametersThrowsException(), () =>
            {
                instance.TestVoidMethodWithoutParametersThrowsException();
            }).Callback(() =>
            {
                DoSomething();
            });
        }

        [Test]
        public async Task TestCallbackInstanceAsync()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(async () =>
            {
                return await Task.FromResult(2);
            });
        }
    }
}
