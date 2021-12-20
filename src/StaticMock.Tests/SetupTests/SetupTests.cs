using NUnit.Framework;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests.SetupTests;

[TestFixture]
public class SetupTests
{
    [Test]
    public void TestSetup()
    {
        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters), () => { });
    }

    [Test]
    public void TestSetupVoid()
    {
        Mock.SetupVoid(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters), () => { });
    }

    [Test]
    public void TestSetupProperty()
    {
        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () => { });
    }

    [Test]
    public void TestSetupNegativeNotExistingMethodName()
    {
        Assert.Throws<Exception>(() => Mock.Setup(typeof(TestStaticClass), "abc123", () => { }));
    }

    [Test]
    public void TestSetupNegativeVoid()
    {
        Assert.Throws<Exception>(() => Mock.Setup(typeof(TestStaticClass),  nameof(TestStaticClass.TestVoidMethodWithParameters), () => { }));
    }

    [Test]
    public void TestSetupMethodNegativeProperty()
    {
        Assert.Throws<Exception>(() => Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () => { }));
    }

    [Test]
    public void TestSetupPropertyNegativeMethod()
    {
        Assert.Throws<Exception>(() => Mock.SetupProperty(typeof(TestStaticClass),  nameof(TestStaticClass.TestMethodReturn1WithoutParameters), () => { }));
    }
}