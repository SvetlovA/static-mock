using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticMock.Tests.ReturnsTests
{
    [TestFixture]
    public class MockReturnsTests
    {
        [Test]
        public void TestReturnsWithTestMethodReturn1WithoutParameters()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;

            Mock.Setup(
                    typeof(TestStaticClass),
                    nameof(TestStaticClass.TestMethodReturn1WithoutParameters),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestReturnsWithTestMethodReturnParameter()
        {
            var originalResult = TestStaticClass.TestMethodReturnWithParameter(10);
            var expectedResult = 2;

            Mock.Setup(
                    typeof(TestStaticClass),
                    nameof(TestStaticClass.TestMethodReturnWithParameter),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturnWithParameter(10);

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestReturnsWithTestMethodReturnInstanceObject()
        {
            var originalResult = TestStaticClass.TestMethodReturnReferenceObject();
            var expectedResult = new TestInstance
            {
                IntProperty = 1,
                ObjectProperty = new object()
            };

            Mock.Setup(
                    typeof(TestStaticClass),
                    nameof(TestStaticClass.TestMethodReturnReferenceObject),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturnReferenceObject();

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestReturnsWithTestMethodOutParameter()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);
            var expectedResult = 2;

            Mock.Setup(
                    typeof(TestStaticClass),
                    nameof(TestStaticClass.TestMethodReturn1WithOutParameter),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestReturnsWithTestMethodRefParameter()
        {
            var x = 1;
            var originalResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref x);
            var expectedResult = 2;

            Mock.Setup(
                    typeof(TestStaticClass),
                    nameof(TestStaticClass.TestMethodReturn1WithRefParameter),
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

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), () =>
            {
                var actualResult = testInstance.TestMethodReturn1WithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
        }

        [Test]
        public async Task TestSetupReturnsMethodsReturnTask()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskWithoutParameters), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public async Task TestSetupReturnsMethodsReturnTaskAsync()
        {
            var originalResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
            {
                var actualResult = await TestStaticClass.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public async Task TestSetupInstanceReturnsMethodsReturnTask()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public async Task TestSetupInstanceReturnsMethodsReturnTaskAsync()
        {
            var instance = new TestInstance();
            var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(1, originalResult);
            var expectedResult = 2;

            Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), async () =>
            {
                var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(Task.FromResult(expectedResult));
        }

        [Test]
        public void TestReturnsStaticIntProperty()
        {
            var originalValue = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(default(int), originalValue);
            var expectedResult = 2;

            Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () =>
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

            Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty), () =>
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

            Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), () =>
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

            Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), () =>
            {
                var actualResult = instance.ObjectProperty;
                Assert.AreEqual(typeof(int), actualResult);
            }).Returns(typeof(int));
        }
    }
}
