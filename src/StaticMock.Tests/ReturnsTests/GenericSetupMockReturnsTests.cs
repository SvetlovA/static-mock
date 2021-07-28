using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticMock.Tests.ReturnsTests
{
    [TestFixture]
    public class GenericSetupMockReturnsTests
    {
        [Test]
        public void TestGenericSetupReturnsWithTestMethodReturn1WithoutParameters()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;

            Mock.Setup(
                    () => TestStaticClass.TestMethodReturn1WithoutParameters(),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestGenericSetupReturnsWithTestMethodReturn1WithoutParametersAfterCheck()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;

            Assert.AreEqual(1, originalResult);

            Mock.Setup(
                    () => TestStaticClass.TestMethodReturn1WithoutParameters(),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);

            var afterMockActualResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(1, afterMockActualResult);
        }

        [Test]
        public void TestGenericSetupReturnsWithTestMethodReturnParameter()
        {
            var originalResult = TestStaticClass.TestMethodReturnWithParameter(10);
            var expectedResult = 2;

            Mock.Setup(
                    () => TestStaticClass.TestMethodReturnWithParameter(10),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturnWithParameter(10);

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestGenericSetupReturnsWithTestMethodReturnInstanceObject()
        {
            var originalResult = TestStaticClass.TestMethodReturnReferenceObject();
            var expectedResult = new TestInstance
            {
                IntProperty = 1,
                ObjectProperty = new object()
            };

            Mock.Setup(
                    () => TestStaticClass.TestMethodReturnReferenceObject(),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturnReferenceObject();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestGenericSetupReturnsWithTestMethodOutParameter()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);
            var expectedResult = 2;

            var x = 1;
            Mock.Setup(
                    () => TestStaticClass.TestMethodReturn1WithOutParameter(out x),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestGenericSetupReturnsWithTestMethodRefParameter()
        {
            var x = 1;
            var originalResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref x);
            var expectedResult = 2;

            Mock.Setup(
                    () => TestStaticClass.TestMethodReturn1WithRefParameter(ref x),
                    () =>
                    {
                        var y = 1;
                        var actualResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref y);

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestInstanceMethodReturns()
        {
            var testInstance = new TestInstance();
            Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
            var expectedResult = 2;

            Mock.Setup(() => testInstance.TestMethodReturn1WithoutParameters(), () =>
            {
                var actualResult = testInstance.TestMethodReturn1WithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
        }

        [Test]
        public async Task TestGenericSetupReturnsMethodsReturnTask()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParameters(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public async Task TestGenericSetupReturnsMethodsReturnTaskAsync()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public async Task TestGenericSetupInstanceReturnsMethodsReturnTask()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParameters(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public async Task TestGenericSetupInstanceReturnsMethodsReturnTaskAsync()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public async Task TestGenericSetupReturnsAsyncMethodsReturnTask()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParameters(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).ReturnsAsync(expectedResult);
        }

        [Test]
        public async Task TestGenericSetupReturnsAsyncMethodsReturnTaskAsync()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => TestStaticClass.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).ReturnsAsync(expectedResult);
        }

        [Test]
        public async Task TestGenericSetupInstanceReturnsAsyncMethodsReturnTask()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParameters(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).ReturnsAsync(expectedResult);
        }

        [Test]
        public async Task TestGenericSetupInstanceReturnsAsyncMethodsReturnTaskAsync()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync(), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).ReturnsAsync(expectedResult);
        }

        [Test]
        public void TestReturnsStaticIntProperty()
        {
            var originalValue = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(default(int), originalValue);
            var expectedResult = 2;

            Mock.Setup(() => TestStaticClass.StaticIntProperty, () =>
            {
                var actualResult = TestStaticClass.StaticIntProperty;
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
        }

        [Test]
        public void TestReturnsStaticObjectProperty()
        {
            var originalValue = TestStaticClass.StaticObjectProperty;
            Assert.AreEqual(default, originalValue);
            var expectedResult = new TestInstance();

            Mock.Setup(() => TestStaticClass.StaticObjectProperty, () =>
            {
                var actualResult = TestStaticClass.StaticObjectProperty;
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
        }

        [Test]
        public void TestReturnsStaticIntPropertyInstance()
        {
            var instance = new TestInstance();
            var originalValue = instance.IntProperty;
            Assert.AreEqual(default(int), originalValue);

            Mock.Setup(() => instance.IntProperty, () =>
            {
                var actualResult = instance.IntProperty;
                Assert.AreEqual(2, actualResult);
            }).Returns(2);
        }

        [Test]
        public void TestReturnsStaticObjectPropertyInstance()
        {
            var instance = new TestInstance();
            var originalValue = instance.ObjectProperty;
            Assert.AreEqual(default, originalValue);

            Mock.Setup(() => instance.ObjectProperty, () =>
            {
                var actualResult = instance.ObjectProperty;
                Assert.AreEqual(typeof(int), actualResult);
            }).Returns(typeof(int));
        }

        [Test]
        public void TestGenericSetupReturnsWithGenericTestMethodReturn1WithoutParameters()
        {
            var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
            Assert.AreEqual(0, originalResult);
            var expectedResult = 2;

            Mock.Setup(
                    () => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>(),
                    () =>
                    {
                        var actualResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestGenericSetupReturnsWithGenericTestMethodReturn1WithoutParametersInstance()
        {
            var testInstance = new TestInstance();
            var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
            Assert.AreEqual(0, originalResult);
            var expectedResult = 2;

            Mock.Setup(
                    () => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>(),
                    () =>
                    {
                        var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }
    }
}
