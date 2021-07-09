using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticMock.Tests.CallbackTests
{
    [TestFixture]
    public class SetupMockCallbackTests
    {
        [Test]
        public void TestFuncCallback()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;
            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters), () =>
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

            Mock.SetupVoid(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException), () =>
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

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
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

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), () =>
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

            Mock.SetupVoid(typeof(TestInstance), nameof(TestInstance.TestVoidMethodWithoutParametersThrowsException), () =>
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

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(async () =>
            {
                return await Task.FromResult(2);
            });
        }

        [Test]
        public void TestCallbackStaticIntProperty()
        {
            var originalValue = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(default(int), originalValue);
            var expectedResult = 2;

            Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () =>
            {
                var actualResult = TestStaticClass.StaticIntProperty;
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(() => expectedResult);
        }

        [Test]
        public void TestCallbackStaticObjectProperty()
        {
            var originalValue = TestStaticClass.StaticObjectProperty;
            Assert.AreEqual(default, originalValue);
            var expectedResult = new TestInstance();

            Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty), () =>
            {
                var actualResult = TestStaticClass.StaticObjectProperty;
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(() => expectedResult);
        }

        [Test]
        public void TestCallbackStaticIntPropertyInstance()
        {
            var instance = new TestInstance();
            var originalValue = instance.IntProperty;
            Assert.AreEqual(default(int), originalValue);

            Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), () =>
            {
                var actualResult = instance.IntProperty;
                Assert.AreEqual(2, actualResult);
            }).Callback(() => 2);
        }

        [Test]
        public void TestCallbackStaticObjectPropertyInstance()
        {
            var instance = new TestInstance();
            var originalValue = instance.ObjectProperty;
            Assert.AreEqual(default, originalValue);

            Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), () =>
            {
                var actualResult = instance.ObjectProperty;
                Assert.AreEqual(typeof(int), actualResult);
            }).Callback(() => typeof(int));
        }
    }
}
