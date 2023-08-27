using System.Linq.Expressions;
using NUnit.Framework;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Hierarchical;

[TestFixture]
public class SetupDefaultTests
{
    [Test]
    public void TestSetupDefaultPositive()
    {
        Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException), () =>
        {
            TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
        });
    }

    [Test]
    public void TestSetupDefaultNegative()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters), () => 
        { 
            TestStaticClass.TestMethodReturn1WithoutParameters();
        }), "Default setup supported only for void methods");
    }

    [Test]
    public void TestGenericSetupDefaultPositive()
    {
        Mock.SetupDefault(() => TestStaticClass.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
        });
    }

    [Test]
    public void TestGenericSetupDefaultNegative()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(() => TestStaticClass.TestMethodReturn1WithoutParameters(), () =>
        {
            TestStaticClass.TestMethodReturn1WithoutParameters();
        }), "Default setup supported only for void methods");
    }

    [Test]
    public void TestGenericSetupDefaultNegativeIncorrectExpression()
    {
        var body = Expression.Block(
            Expression.Add(Expression.Constant(1), Expression.Constant(2)),
            Expression.Constant(3));
        var lambda = Expression.Lambda<Action>(body);

        Assert.Throws<Exception>(() => Mock.SetupDefault(lambda, () =>
        {
            TestStaticClass.TestMethodReturn1WithoutParameters();
        }), "Get expression not contains method to setup");
    }

    [Test]
    public void TestGenericSetupDefaultInstanceMethod()
    {
        var testInstance = new TestInstance();

        Mock.SetupDefault(() => testInstance.TestVoidMethodWithoutParametersThrowsException(), () =>
        {
            testInstance.TestVoidMethodWithoutParametersThrowsException();
        });
    }

    [Test]
    public void TestGenericSetupDefaultTestMethodThrowsExceptionAsyncNegative()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(() => TestStaticAsyncClass.TestMethodThrowsExceptionAsync(), () =>
        {
            TestStaticAsyncClass.TestMethodThrowsExceptionAsync();
        }));
    }

    [Test]
    public void TestGenericSetupDefaultTestMethodThrowsExceptionNegative()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(() => TestStaticAsyncClass.TestMethodThrowsException(), () =>
        {
            TestStaticAsyncClass.TestMethodThrowsException();
        }));
    }

    [Test]
    public void TestSetupDefaultTestMethodThrowsExceptionAsyncNegative()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodThrowsExceptionAsync), () =>
        {
            TestStaticAsyncClass.TestMethodThrowsExceptionAsync();
        }));
    }

    [Test]
    public void TestSetupDefaultTestMethodThrowsExceptionNegative()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodThrowsException), () =>
        {
            TestStaticAsyncClass.TestMethodThrowsException();
        }));
    }

    [Test]
    public void TestSetupDefaultStaticIntPropertyThrowsException()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () =>
        {
            var actualResult = TestStaticClass.StaticIntProperty;
        }), $"Can't find method {nameof(TestStaticClass.StaticIntProperty)} of type {nameof(TestStaticClass)}");
    }

    [Test]
    public void TestSetupDefaultStaticObjectPropertyThrowsException()
    {
        Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty), () =>
        {
            var actualResult = TestStaticClass.StaticObjectProperty;
        }), $"Can't find method {nameof(TestStaticClass.StaticObjectProperty)} of type {nameof(TestStaticClass)}");
    }

    [Test]
    public void TestSetupDefaultInstanceIntPropertyThrowsException()
    {
        var instance = new TestInstance();

        Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestInstance), nameof(TestInstance.IntProperty), () =>
        {
            var actualResult = instance.IntProperty;
        }), $"Can't find method {nameof(TestInstance.IntProperty)} of type {nameof(TestInstance)}");
    }

    [Test]
    public void TestSetupDefaultInstanceObjectPropertyThrowsException()
    {
        var instance = new TestInstance();

        Assert.Throws<Exception>(() => Mock.SetupDefault(typeof(TestInstance), nameof(TestInstance.ObjectProperty), () =>
        {
            var actualResult = instance.ObjectProperty;
        }), $"Can't find method {nameof(TestInstance.ObjectProperty)} of type {nameof(TestInstance)}");
    }
}