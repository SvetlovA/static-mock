using System.Reflection;
using NUnit.Framework;
using StaticMock.Entities;
using StaticMock.Tests.TestEntities;

namespace StaticMock.Tests.CallbackTests;

[TestFixture]
public class SetupMockCallbackTests
{
    [Test]
    public void TestFuncCallback()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        var expectedResult = 2;
        Assert.AreNotEqual(expectedResult, originalResult);

        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters), () =>
        {
            var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(() =>
        {
            var x = expectedResult;
            return x;
        });
    }

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
    public async Task TestCallbackAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        var expectedResult = 2;

        Assert.AreNotEqual(expectedResult, originalResult);

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(async () =>
        {
            return await Task.FromResult(2);
        });
    }

    [Test]
    public void TestFuncCallbackInstance()
    {
        var instance = new TestInstance();
        var originalResult = instance.TestMethodReturn1WithoutParameters();
        Assert.AreNotEqual(2, originalResult);

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), () =>
        {
            var actualResult = instance.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(2, actualResult);
        }).Callback(() =>
        {
            return 2; // if not static scope you can't use closure
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

    [Test]
    public async Task TestCallbackInstanceAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
        var expectedResult = 2;

        Assert.AreNotEqual(expectedResult, originalResult);

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(async () =>
        {
            return await Task.FromResult(2);
        });
    }

    [Test]
    public void TestCallbackStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        Assert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () =>
        {
            var actualResult = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(() => expectedResult);
    }

    [Test]
    public void TestCallbackStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        Assert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty), () =>
        {
            var actualResult = TestStaticClass.StaticObjectProperty;
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(() => expectedResult);
    }

    [Test]
    public void TestCallbackStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        Assert.AreEqual(default(int), originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), () =>
        {
            var actualResult = instance.IntProperty;
            Assert.AreEqual(2, actualResult);
        }).Callback(() => 2);
    }

    [Test]
    public void TestCallbackStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        Assert.AreEqual(default, originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), () =>
        {
            var actualResult = instance.ObjectProperty;
            Assert.AreEqual(typeof(int), actualResult);
        }).Callback(() => typeof(int));
    }

    [Test]
    public void TestInstancePrivateMethodReturn1WithoutParameters()
    {
        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        MethodInfo methodInfo = type.GetMethod("TestPrivateMethodReturn1WithoutParameters", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.AreEqual(1, methodInfo.Invoke(testInstance, new object[] { }));
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), methodInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
        {
            var actualResult = methodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult); 
        }).Callback(()=> { return 2; });
    }

    [Test]
    public void TestReturnsPrivateIntProperty()
    {
        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        PropertyInfo propertyInfo = type.GetProperty("PrivateIntProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(testInstance, new object[] { });
        Assert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
        {
            var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(()=> { return 2; });
    }

    [Test]
    public void TestReturnsPrivateObjectProperty()
    {

        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        PropertyInfo propertyInfo = type.GetProperty("PrivateObjectProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(testInstance, new object[] { });
        Assert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Instance, () =>
        {
            var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(()=> { return new TestInstance(); });
    }


    [Test]
    public void TestReturnsWithTestPrivateStaticMethodReturn1WithoutParameters()
    {
        Type type = typeof(TestStaticClass);
        MethodInfo methodInfo = type.GetMethod("TestPrivateStaticMethodReturn1WithoutParameters", BindingFlags.Static | BindingFlags.NonPublic);

        var originalResult = methodInfo.Invoke(type, new object[] { });
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                methodInfo.Name,
                BindingFlags.Static | BindingFlags.NonPublic,
                () =>
                {
                    var actualResult = methodInfo.Invoke(type, new object[] { });

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Callback(()=> { return 2; });
    }

    [Test]
    public void TestReturnsPrivateStaticIntProperty()
    {
        Type type = typeof(TestStaticClass);
        PropertyInfo propertyInfo = type.GetProperty("PrivateStaticIntProperty", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(type, new object[] { });
        Assert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
        {
            var actualResult = mothodInfo.Invoke(type, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(()=> { return 2; });
    }

    [Test]
    public void TestReturnsPrivateStaticObjectProperty()
    {
        Type type = typeof(TestStaticClass);
        PropertyInfo propertyInfo = type.GetProperty("PrivateStaticObjectProperty", BindingFlags.NonPublic | BindingFlags.Static);
        MethodInfo mothodInfo = propertyInfo.GetMethod;
        var originalValue = mothodInfo.Invoke(type, new object[] { });
        Assert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static, () =>
        {
            var actualResult = mothodInfo.Invoke(type, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Callback(()=> { return new TestInstance(); });
    }

    [Test]
    public void TestSetupCallbackWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);

        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new []{ typeof(int) } },
                () =>
                {
                    var actualResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(2, actualResult);
                })
            .Callback(() => 2);
    }

    [Test]
    public void TestSetupCallbackWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new []{ typeof(int) } },
                () =>
                {
                    var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(2, actualResult);
                })
            .Callback(() => 2);
    }

    [Test]
    public void TestGenericSetupCallbackWithGenericTestInstanceReturnDefaultWithoutParameters()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        Assert.AreEqual(0, originalResult);

        Mock.Setup(typeof(TestGenericInstance<int>), nameof(TestGenericInstance<int>.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new []{ typeof(int) } },
                () =>
                {
                    var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(2, actualResult);
                })
            .Callback(() => 2);
    }
}