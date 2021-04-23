using NUnit.Framework;

namespace StaticMock.Tests
{
    [TestFixture]
    public class MockReturnsTests
    {
        [Test]
        public void TestSetupWithTestMethodReturn1WithoutParameters()
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
        public void TestSetupWithTestMethodReturnParameter()
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
        public void TestSetupWithTestMethodReturnInstanceObject()
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
        public void TestSetupWithTestMethodOutParameter()
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
        public void TestSetupWithTestMethodRefParameter()
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
        public void TestGenericSetupWithTestMethodReturn1WithoutParameters()
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
        public void TestGenericSetupWithTestMethodReturnParameter()
        {
            var originalResult = TestStaticClass.TestMethodReturnParameter(10);
            var expectedResult = 2;

            Mock.Setup(
                    () => TestStaticClass.TestMethodReturnParameter(10),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturnParameter(10);

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }

        [Test]
        public void TestGenericSetupWithTestMethodReturnInstanceObject()
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
        public void TestGenericSetupWithTestMethodOutParameter()
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
        public void TestGenericSetupWithTestMethodRefParameter()
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
    }
}
