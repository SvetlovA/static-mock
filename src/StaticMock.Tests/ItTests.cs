using NUnit.Framework;

namespace StaticMock.Tests
{
    [TestFixture]
    public class ItTests
    {
        [Test]
        public void TestGenericSetupReturnsWithTestMethodReturnParameter()
        {
            var originalResult = TestStaticClass.TestMethodReturnWithParameter(10);
            var expectedResult = 2;
            Mock.Setup(
                    () => TestStaticClass.TestMethodReturnWithParameter(It.IsAny<int>()),
                    () =>
                    {
                        var actualResult = TestStaticClass.TestMethodReturnWithParameter(10);

                        Assert.AreNotEqual(originalResult, actualResult);
                        Assert.AreEqual(expectedResult, actualResult);
                    })
                .Returns(expectedResult);
        }
    }
}
