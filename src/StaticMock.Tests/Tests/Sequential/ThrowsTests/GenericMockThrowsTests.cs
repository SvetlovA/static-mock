using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Tests.Common.TestEntities;
using StaticMock.Tests.Entities;

namespace StaticMock.Tests.Tests.Sequential.ThrowsTests;

[TestFixture]
public class GenericMockThrowsTests
{
    [Test]
    public void TestThrowsTestMethodReturn1WithoutParameters()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestThrowsTestVoidMethodWithoutParameters()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters()).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParameters());
    }

    [Test]
    public void TestThrowsTestVoidMethodWithParameters()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(1)).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithParameters(1));
    }

    [Test]
    public void TestThrowsArgumentNullException()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters()).Throws<ArgumentNullException>();

        Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestVoidMethodWithoutParameters());
    }

    [Test]
    public void TestInstanceMethodThrows()
    {
        var testInstance = new TestInstance();

        using var _ = Mock.Setup(() => testInstance.TestMethodReturn1WithoutParameters()).Throws<Exception>();

        Assert.Throws<Exception>(() => testInstance.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTask()
    {
        using var _ = Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTask()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTask());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskAsync()
    {
        using var _ = Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskAsync()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsTestVoidMethodReturnTaskAsync()
    {
        using var _ = Mock.Setup(() => TestStaticAsyncClass.TestVoidMethodReturnTaskAsync()).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticAsyncClass.TestVoidMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParametersAsync()
    {
       using var _ = Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParameters()
    {
        using var _ = Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTask()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(() => instance.TestMethodReturnTask()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTask());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(() => instance.TestMethodReturnTaskAsync()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsInstanceTestVoidMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(() => instance.TestVoidMethodReturnTaskAsync() ).Throws<Exception>();

        Assert.Throws<Exception>(() => instance.TestVoidMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParametersAsync()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(() => instance.TestMethodReturnTaskWithoutParametersAsync()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParametersAsync());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParameters()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(() => instance.TestMethodReturnTaskWithoutParameters()).Throws<Exception>();

        ClassicAssert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParameters());
    }

    [Test]
    public void TestThrowsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);

        using var _ = Mock.Setup(() => TestStaticClass.StaticIntProperty).Throws<Exception>();

        Assert.Throws<Exception>(() =>
        {
            var actualResult = TestStaticClass.StaticIntProperty;
        });
    }

    [Test]
    public void TestThrowsStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);

        using var _ = Mock.Setup(() => TestStaticClass.StaticObjectProperty).Throws<Exception>();

        Assert.Throws<Exception>(() =>
        {
            var actualResult = TestStaticClass.StaticObjectProperty;
        });
    }

    [Test]
    public void TestThrowsStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);

        using var _ = Mock.Setup(() => instance.IntProperty).Throws<Exception>();

        Assert.Throws<Exception>(() =>
        {
            var actualResult = instance.IntProperty;
        });
    }

    [Test]
    public void TestThrowsStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);

        using var _ = Mock.Setup(() => instance.ObjectProperty).Throws<Exception>();

        Assert.Throws<Exception>(() =>
        {
            var actualResult = instance.ObjectProperty;
        });
    }

    [Test]
    public void TestGenericSetupThrowsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);

        using var _ = Mock.Setup(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>()).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>());
    }

    [Test]
    public void TestGenericSetupThrowsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);

        using var _ = Mock.Setup(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>()).Throws<Exception>();

        Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>());
    }

    [Test]
    public void TestGenericSetupThrowsWithGenericTestInstanceReturnDefaultWithoutParameters()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        ClassicAssert.AreEqual(0, originalResult);

        using var _ = Mock.Setup(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters()).Throws<Exception>();

        Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters());
    }

    [Test]
    public void TestThrowsParameterLessCustomExceptionTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        using var _ = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws<CustomExceptionWithoutDefaultConstructor>(new object[] { message });

        Assert.Throws<CustomExceptionWithoutDefaultConstructor>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
    }

    [Test]
    public void TestThrowsExceptionWithMessageTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        using var _ = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws<Exception>(new object[] { message });

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
    }
    
    [Test]
    public void TestGenericSetupReturnsDateTimeUtcNow()
    {
        using var _ = Mock.Setup(context => DateTime.UtcNow)
            .Throws<Exception>();

        Assert.Throws<Exception>(() =>
        {
            var x = DateTime.UtcNow;
        });
    }
    
    [Test]
    public void TestGenericSetupReturnsDateTimeNow()
    {
        using var _ = Mock.Setup(context => DateTime.Now)
            .Throws<Exception>();

        Assert.Throws<Exception>(() =>
        {
            var x = DateTime.Now;
        });
    }
    
    [Test]
    public void TestGenericSetupReturnsStaticUnsafeProperty()
    {
        using var _ = Mock.Setup(context => TestStaticClass.UnsafeProperty)
            .Throws<Exception>();
        Assert.Throws<Exception>(() =>
        {
            var x = TestStaticClass.UnsafeProperty;
        });
    }
    
    [Test]
    public void TestGenericSetupReturnsStaticUnsafeMethod()
    {
        using var _ = Mock.Setup(context => TestStaticClass.TestUnsafeStaticMethod())
            .Throws<Exception>();
        Assert.Throws<Exception>(() => TestStaticClass.TestUnsafeStaticMethod());
    }
    
    [Test]
    public void TestGenericSetupReturnsInstanceUnsafeProperty()
    {
        var instance = new TestInstance();
        using var _ = Mock.Setup(context => instance.UnsafeProperty)
            .Throws<Exception>();
        Assert.Throws<Exception>(() =>
        {
            var x = instance.UnsafeProperty;
        });
    }
    
    [Test]
    public void TestGenericSetupReturnsInstanceUnsafeMethod()
    {
        var instance = new TestInstance();
        using var _ = Mock.Setup(context => instance.TestUnsafeInstanceMethod())
            .Throws<Exception>();
        Assert.Throws<Exception>(() => instance.TestUnsafeInstanceMethod());
    }
}