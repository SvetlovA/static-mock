using NUnit.Framework;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests;

[TestFixture]
public class SetupWithContextTests
{
    [Test]
    public void TestSetupItIsWithOneParameter()
    {
        const int expectedValue = 2;
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns(expectedValue);
    }
}