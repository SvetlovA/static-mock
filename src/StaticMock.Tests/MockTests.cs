using NUnit.Framework;

namespace StaticMock.Tests
{
    [TestFixture]
    public class MockTests
    {
        [Test]
        public void TestSetup()
        {
            var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            var expectedResult = 2;

            Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters))
                .Return(expectedResult);

            var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

            Assert.AreNotEqual(originalResult, actualResult);
            Assert.AreNotEqual(expectedResult, actualResult);
        }
    }
}
