using NUnit.Framework;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests.CallbackTests;

[TestFixture]
public class GenericSetupMockCallbackTests
{
    [Test]
    public void TestActionCallback()
    {
        static void DoSomething() { }

        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
        }).Callback(() =>
        {
            DoSomething();
        });
    }

    [Test]
    public void TestActionCallbackThrows()
    {
        Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException());
        }).Callback(() => throw new Exception());
    }

    [Test]
    public void TestActionCallbackInstance()
    {
        var instance = new TestInstance();

        static void DoSomething() { }

        Mock.Setup(() => instance.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            instance.TestVoidMethodWithoutParametersThrowsException();
        }).Callback(() =>
        {
            DoSomething();
        });
    }
}