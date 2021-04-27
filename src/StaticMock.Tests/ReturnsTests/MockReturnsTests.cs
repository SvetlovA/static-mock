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
            var originalResult = TestStaticClass.TestMethodReturnParameter(10);
            var expectedResult = 2;

            Mock.Setup(
                    typeof(TestStaticClass),
                    nameof(TestStaticClass.TestMethodReturnParameter),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturnParameter(10);

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
    }
}
