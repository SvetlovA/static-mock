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
        public async Task TestCallbackReturnTaskAsync()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(async () => await Task.FromResult(2));
        }

        [Test]
        public async Task TestCallbackReturnTask()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(() => Task.FromResult(2));
        }

        [Test]
        public async Task TestCallbackAsyncReturnTask()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).CallbackAsync(() => 2);
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
        public async Task TestCallbackInstanceReturnTaskAsync()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(async () => await Task.FromResult(2));
        }

        [Test]
        public async Task TestCallbackInstanceReturnTask()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Callback(() => Task.FromResult(2));
        }

        [Test]
        public async Task TestCallbackAsyncInstanceReturnTask()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            var expectedResult = 2;

            Assert.AreNotEqual(expectedResult, originalResult);

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).CallbackAsync(() => 2);
        }

        [Test]
        public void TestCallbackStaticIntProperty()
        {
            var originalValue = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(default(int), originalValue);
            var expectedResult = 2;

            Mock.Setup(() => TestStaticClass.StaticIntProperty, () =>
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

            Mock.Setup(() => TestStaticClass.StaticObjectProperty, () =>
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

            Mock.Setup(() => instance.IntProperty, () =>
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

            Mock.Setup(() => instance.ObjectProperty, () =>
            {
                var actualResult = instance.ObjectProperty;
                Assert.AreEqual(typeof(int), actualResult);
            }).Callback(() => typeof(int));
        }

        [Test]
        public void TestGenericSetupCallbackWithGenericTestMethodReturn1WithoutParameters()
        {
            var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
            Assert.AreEqual(0, originalResult);

            Mock.Setup(
                    () => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>(),
                    () =>
                    {
                        var actualResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(2, actualResult);
                    })
                .Callback(() => 2);
        }

        [Test]
        public void TestGenericSetupCallbackWithGenericTestMethodReturn1WithoutParametersInstance()
        {
            var testInstance = new TestInstance();
            var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
            Assert.AreEqual(0, originalResult);

            Mock.Setup(
                    () => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>(),
                    () =>
                    {
                        var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(2, actualResult);
                    })
                .Callback(() => 2);
        }
    }
}
