using System.Threading.Tasks;
using NUnit.Framework;

namespace StaticMock.Tests
{
    [TestFixture]
    public class MockTests
    {
        [Test]
        public void TestSetupWithTestMethodReturn1WithoutParameters()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters))
                .Returns(expectedResult);

            var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

            Assert.AreNotEqual(originalResult, actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestSetupWithTestMethodReturnParameter()
        {
            var originalResult = TestStaticClass.TestMethodReturnParameter(10);
            var expectedResult = 2;

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnParameter))
                .Returns(expectedResult);

            var actualResult = TestStaticClass.TestMethodReturnParameter(10);

            Assert.AreNotEqual(originalResult, actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public async Task TestSetupWithTestMethodReturn1WithoutParametersAsync()
        {
            var originalResult = await TestStaticClass.TestMethodReturn1WithoutParametersAsync();
            var expectedResult = 2;

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParametersAsync))
                .Returns(expectedResult);

            var actualResult = await TestStaticClass.TestMethodReturn1WithoutParametersAsync();

            Assert.AreNotEqual(originalResult, actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestSetupWithTestMethodReturnInstanceObject()
        {
            var originalResult = TestStaticClass.TestMethodReturnInstanceObject();
            var expectedResult = new TestInstance
            {
                IntProperty = 1,
                ObjectProperty = new object()
            };

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnInstanceObject))
                .Returns(expectedResult);

            var actualResult = TestStaticClass.TestMethodReturnInstanceObject();

            Assert.AreNotEqual(originalResult, actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestGenericSetupWithTestMethodReturn1WithoutParameters()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;

            Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
                .Returns(expectedResult);

            var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

            Assert.AreNotEqual(originalResult, actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
