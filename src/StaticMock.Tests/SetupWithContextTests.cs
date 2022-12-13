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
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns(expectedValue);
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestReturnSetupItIsWithParameterNegative()
    {
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }).Returns(default(int));
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestReturnSetupItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns(expectedValue);
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestReturnSetupItIsWithParametersPositive()
    {
        const int expectedValue = 2;
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(
            context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0)), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>());
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns(expectedValue);
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestReturnSetupItIsWithParametersNegative()
    {
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(
            context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0)), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameters(2, new []{ 0 }));
        }).Returns(default(int));
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestReturnSetupItIsAnyWithParametersPositive()
    {
        const int expectedValue = 2;
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(
            context.It.IsAny<int>(), context.It.IsAny<int[]>()), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameters(1, new []{ 0 });
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns(expectedValue);
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestCallbackSetupItIsWithParameterPositive()
    {
        const int expectedValue = 2;
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns<int>(_ => expectedValue);
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupItIsWithParameterNegative()
    {
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }).Returns<int>(_ => default);
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()), () =>
        {
            var actualValue = TestStaticClass.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns<int>(_ => expectedValue);
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupInstanceItIsWithParameterPositive()
    {
        const int expectedValue = 2;

        var instance = new TestInstance();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
        Mock.Setup(context => instance.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            var actualValue = instance.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns<int>(_ => expectedValue);
        Assert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupInstanceItIsWithParameterNegative()
    {
        var instance = new TestInstance();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
        Mock.Setup(context => instance.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<Exception>(() => instance.TestMethodReturnWithParameter(2));
        }).Returns<int>(_ => default);
        Assert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestCallbackSetupInstanceItIsAnyWithParameterPositive()
    {
        const int expectedValue = 2;
        var instance = new TestInstance();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
        Mock.Setup(context => instance.TestMethodReturnWithParameter(context.It.IsAny<int>()), () =>
        {
            var actualValue = instance.TestMethodReturnWithParameter(1);
            Assert.AreEqual(expectedValue, actualValue);
        }).Returns<int>(_ => expectedValue);
        Assert.AreEqual(1, instance.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupItIsWithParameterPositive()
    {
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(
            context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<ArgumentException>(() => TestStaticClass.TestMethodReturnWithParameter(1));
        }).Throws<ArgumentException>();
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupItIsWithParameterNegative()
    {
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x == 1)), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturnWithParameter(2));
        }).Throws<ArgumentException>();
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupItIsAnyWithParameterPositive()
    {
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
        Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()), () =>
        {
            Assert.Throws<ArgumentException>(() => TestStaticClass.TestMethodReturnWithParameter(1));
        }).Throws<ArgumentException>();
        Assert.AreEqual(1, TestStaticClass.TestMethodReturnWithParameter(1));
    }

    [Test]
    public void TestThrowsSetupInstanceItIsWithParametersPositive()
    {
        var instance = new TestInstance();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        Mock.Setup(context => instance.TestMethodReturnWithParameters(
            context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0)), () =>
        {
            Assert.Throws<ArgumentException>(() => instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        }).Throws<ArgumentException>();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestThrowsSetupInstanceItIsWithParametersNegative()
    {
        var instance = new TestInstance();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        Mock.Setup(context => instance.TestMethodReturnWithParameters(
            context.It.Is<int>(x => x == 1), context.It.Is<int[]>(x => x.Length == 0)), () =>
        {
            Assert.Throws<Exception>(() => instance.TestMethodReturnWithParameters(2, new []{ 0 }));
        }).Throws<ArgumentException>();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }

    [Test]
    public void TestThrowsSetupInstanceItIsAnyWithParametersPositive()
    {
        var instance = new TestInstance();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        Mock.Setup(context => instance.TestMethodReturnWithParameters(
            context.It.IsAny<int>(), context.It.IsAny<int[]>()), () =>
        {
            Assert.Throws<ArgumentException>(() => instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
        }).Throws<ArgumentException>();
        Assert.AreEqual(1, instance.TestMethodReturnWithParameters(1, Array.Empty<int>()));
    }
}