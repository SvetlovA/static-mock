using NUnit.Framework;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests;

[TestFixture]
public class SetupWithContextTests
{
    [Test]
    public void TestReturnSetupItIsWithParameterPositive()
    {
        const int expectedValue = 2;
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.Is<int>(x => x == 1)), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns(expectedValue);
    }

    [Test]
    public void TestReturnSetupItIsWithParameterNegative()
    {
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }).Returns(default);
    }

    [Test]
    public void TestReturnSetupItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.IsAny<int>()), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns(expectedValue);
    }

    [Test]
    public void TestCallbackSetupItIsWithParameterPositive()
    {
        const int expectedValue = 2;
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.Is<int>(x => x == 1)), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Callback(() => expectedValue);
    }

    [Test]
    public void TestCallbackSetupItIsWithParameterNegative()
    {
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }).Callback(() => default);
    }

    [Test]
    public void TestCallbackSetupItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.IsAny<int>()), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Callback(() => expectedValue);
    }

    [Test]
    public void TestThrowsSetupItIsWithParameterPositive()
    {
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<ArgumentException>(() => TestStaticClass.TestMethodReturnWithParameter(1));
        }).Throws<ArgumentException>();
    }

    [Test]
    public void TestThrowsSetupItIsWithParameterNegative()
    {
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }).Throws<ArgumentException>();
    }

    [Test]
    public void TestThrowsSetupItIsAnyWithParameterPositive()
    {
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.IsAny<int>()), () =>
        {
            Assert.Throws<ArgumentException>(() => TestStaticClass.TestMethodReturnWithParameter(1));
        }).Throws<ArgumentException>();
    }
}