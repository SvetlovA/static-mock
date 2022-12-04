using NUnit.Framework;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests.CallbackTests;

[TestFixture]
public class SetupMockCallbackTests
{
    [Test]
    public void TestActionCallback()
    {
        static void DoSomething() { }

        Mock.SetupVoid(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException), () =>
        {
            TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
        }).Callback(() =>
        {
            DoSomething();
        });
    }

    [Test]
    public void TestActionCallbackInstance()
    {
        var instance = new TestInstance();

        static void DoSomething() { }

        Mock.SetupVoid(typeof(TestInstance), nameof(TestInstance.TestVoidMethodWithoutParametersThrowsException), () =>
        {
            instance.TestVoidMethodWithoutParametersThrowsException();
        }).Callback(() =>
        {
            DoSomething();
        });
    }
}