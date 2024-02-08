using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;
using StaticMock.Tests.Entities;

namespace StaticMock.Tests.Tests.Sequential.ThrowsTests;

[TestFixture]
public class MockThrowsTests
{
    [Test]
    public void TestThrowsTestMethodReturn1WithoutParameters()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws(typeof(Exception));

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestThrowsTestVoidMethodWithoutParameters()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters()).Throws(typeof(Exception));

        Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithoutParameters());
    }

    [Test]
    public void TestThrowsTestVoidMethodWithParameters()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithParameters(1)).Throws(typeof(Exception));

        Assert.Throws<Exception>(() => TestStaticClass.TestVoidMethodWithParameters(1));
    }

    [Test]
    public void TestThrowsArgumentNullException()
    {
        using var _ = Mock.Setup(() => TestStaticClass.TestVoidMethodWithoutParameters()).Throws(typeof(ArgumentNullException));

        Assert.Throws<ArgumentNullException>(() => TestStaticClass.TestVoidMethodWithoutParameters());
    }

    [Test]
    public void TestInstanceMethodThrows()
    {
        var testInstance = new TestInstance();

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), new SetupProperties { Instance = testInstance }).Throws<Exception>();

        Assert.Throws<Exception>(() => testInstance.TestMethodReturn1WithoutParameters());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTask()
    {
        using var _ = Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTask)).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTask());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskAsync()
    {
        using var _ = Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskAsync)).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsTestVoidMethodReturnTaskAsync()
    {
        using var _ = Mock.SetupAction(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestVoidMethodReturnTaskAsync)).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticAsyncClass.TestVoidMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParametersAsync()
    {
        using var _ = Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync)).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync());
    }

    [Test]
    public void TestGenericThrowsTestMethodReturnTaskWithoutParameters()
    {
        using var _ = Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters)).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTask()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTask), new SetupProperties { Instance = instance }).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTask());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskAsync), new SetupProperties { Instance = instance }).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsInstanceTestVoidMethodReturnTaskAsync()
    {
        var instance = new TestInstance();

        using var _ = Mock.SetupAction(typeof(TestInstance), nameof(TestInstance.TestVoidMethodReturnTaskAsync), new SetupProperties { Instance = instance}).Throws<Exception>();

        Assert.Throws<Exception>(() => instance.TestVoidMethodReturnTaskAsync());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParametersAsync()
    {
        var instance = new TestInstance();

        using var _ =Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), new SetupProperties { Instance = instance }).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParametersAsync());
    }

    [Test]
    public void TestGenericThrowsInstanceTestMethodReturnTaskWithoutParameters()
    {
        var instance = new TestInstance();

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), new SetupProperties { Instance = instance }).Throws<Exception>();

        Assert.ThrowsAsync<Exception>(() => instance.TestMethodReturnTaskWithoutParameters());
    }

    [Test]
    public void TestThrowsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty)).Throws<Exception>();

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

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty)).Throws<Exception>();

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

        using var _ = Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), new SetupProperties { Instance = instance }).Throws<Exception>();

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

        using var _ = Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), new SetupProperties { Instance = instance }).Throws<Exception>();

        Assert.Throws<Exception>(() =>
        {
            var actualResult = instance.ObjectProperty;
        });
    }

    [Test]
    public void TestInstanceThrowsPrivateMethodReturn1WithoutParameters()
    {
        var testInstance = new TestInstance();
        var type = testInstance.GetType();
        var methodInfo = type.GetMethod("TestPrivateMethodReturn1WithoutParameters", BindingFlags.NonPublic | BindingFlags.Instance);

        using var _ = Mock.Setup(typeof(TestInstance), methodInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }).Throws(typeof(Exception));

        try
        {
            var actualResult = methodInfo.Invoke(testInstance, new object[] { });
        }
        catch (Exception e)
        {
            ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
        }
    }

    [Test]
    public void TestThrowsPrivateIntProperty()
    {
        var testInstance = new TestInstance();
        var type = testInstance.GetType();
        var propertyInfo = type.GetProperty("PrivateIntProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        var methodInfo = propertyInfo.GetMethod;

        using var _ = Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }).Throws(typeof(Exception));

        try
        {
            var actualResult = methodInfo.Invoke(testInstance, new object[] { });
        }
        catch (Exception e)
        {
            ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
        }
    }

    [Test]
    public void TestThrowsPrivateObjectProperty()
    {

        var testInstance = new TestInstance();
        var type = testInstance.GetType();
        var propertyInfo = type.GetProperty("PrivateObjectProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        var methodInfo = propertyInfo.GetMethod;

        using var _ = Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }).Throws(typeof(Exception));

        try
        {
            var actualResult = methodInfo.Invoke(testInstance, new object[] { });
        }
        catch (Exception e)
        {
            ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
        }
    }

    [Test]
    public void TestThrowsTestPrivateMethodReturn1WithoutParameters()
    {
        var type = typeof(TestStaticClass);
        var methodInfo = type.GetMethod("TestPrivateStaticMethodReturn1WithoutParameters", BindingFlags.Static | BindingFlags.NonPublic);

        using var _ = Mock.Setup(type, methodInfo.Name, BindingFlags.Static | BindingFlags.NonPublic).Throws(typeof(Exception));

        try
        {
            int result = (int)methodInfo.Invoke(type, new object[] { });
        }
        catch (Exception e)
        {
            ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
        }
    }

    [Test]
    public void TestThrowsPrivateStaticIntProperty()
    {
        var type = typeof(TestStaticClass);
        var propertyInfo = type.GetProperty("PrivateStaticIntProperty", BindingFlags.NonPublic | BindingFlags.Static);
        var methodInfo = propertyInfo.GetMethod;
        var originalValue = methodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(default(int), originalValue);

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static).Throws<Exception>();

        try
        {
            var actualResult = (int)methodInfo.Invoke(type, new object[] { });
        }
        catch (Exception e)
        {
            ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
        }
    }

    [Test]
    public void TestThrowsPrivateStaticObjectProperty()
    {
        var type = typeof(TestStaticClass);
        var propertyInfo = type.GetProperty("PrivateStaticObjectProperty", BindingFlags.NonPublic | BindingFlags.Static);
        var methodInfo = propertyInfo.GetMethod;
        var originalValue = methodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(default, originalValue);

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static).Throws<Exception>();

        try
        {
            var actualResult = (int)methodInfo.Invoke(type, new object[] { });
        }
        catch (Exception e)
        {
            ClassicAssert.AreEqual(typeof(Exception), e.InnerException.GetType());
        }
    }

    [Test]
    public void TestSetupThrowsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);

        using var _ = Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new[] { typeof(int) } }).Throws<Exception>();

        Assert.Throws<Exception>(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>());
    }

    [Test]
    public void TestSetupThrowsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            }).Throws<Exception>();

        Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>());
    }

    [Test]
    public void TestSetupThrowsWithGenericTestMethodReturn1WithoutParametersInstance()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        ClassicAssert.AreEqual(0, originalResult);

        using var _ = Mock.Setup(typeof(TestGenericInstance<int>), nameof(TestGenericInstance<int>.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            }).Throws<Exception>();

        Assert.Throws<Exception>(() => testInstance.GenericTestMethodReturnDefaultWithoutParameters());
    }

    [Test]
    public void TestThrowsParameterLessCustomExceptionTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        using var _ = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws(typeof(CustomExceptionWithoutDefaultConstructor), message);

        Assert.Throws<CustomExceptionWithoutDefaultConstructor>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
    }

    [Test]
    public void TestThrowsExceptionWithMessageTestMethodReturn1WithoutParameters()
    {
        const string message = "testExceptionMessage";
        using var _ = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters()).Throws(typeof(Exception), message);

        Assert.Throws<Exception>(() => TestStaticClass.TestMethodReturn1WithoutParameters(), message);
    }
}