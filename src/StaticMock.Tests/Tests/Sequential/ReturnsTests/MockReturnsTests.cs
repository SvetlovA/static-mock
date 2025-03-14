using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Sequential.ReturnsTests;

[TestFixture]
public class MockReturnsTests
{
    [Test]
    public void TestReturnsWithTestMethodReturn1WithoutParameters()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters))
            .Returns(expectedResult);

        var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsWithTestMethodReturnParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturnWithParameter(10);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameter))
            .Returns(expectedResult);

        var actualResult = TestStaticClass.TestMethodReturnWithParameter(10);

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsWithTestMethodReturnInstanceObject()
    {
        var originalResult = TestStaticClass.TestMethodReturnReferenceObject();
        var expectedResult = new TestInstance
        {
            IntProperty = 1,
            ObjectProperty = new object()
        };

        using var _ = Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnReferenceObject))
            .Returns(expectedResult);

        var actualResult = TestStaticClass.TestMethodReturnReferenceObject();

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsWithTestMethodOutParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithOutParameter(out var _);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithOutParameter))
            .Returns(expectedResult);

        var actualResult = TestStaticClass.TestMethodReturn1WithOutParameter(out var _);

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsWithTestMethodRefParameter()
    {
        var x = 1;
        var originalResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref x);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithRefParameter))
            .Returns(expectedResult);

        var y = 1;
        var actualResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref y);

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestInstanceMethodReturns()
    {
        var testInstance = new TestInstance();
        ClassicAssert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), new SetupProperties { Instance = testInstance }).Returns(expectedResult);

        var actualResult = testInstance.TestMethodReturn1WithoutParameters();
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public async Task TestSetupReturnsMethodsReturnTask()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters)).Returns(Task.FromResult(expectedResult));

        var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public async Task TestSetupReturnsMethodsReturnTaskAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        ClassicAssert.AreEqual(1, originalResult);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync)).Returns(Task.FromResult(expectedResult));

        var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public async Task TestSetupInstanceReturnsMethodsReturnTask()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), new SetupProperties { Instance = instance }).Returns(Task.FromResult(expectedResult));

        var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public async Task TestSetupInstanceReturnsMethodsReturnTaskAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        ClassicAssert.AreEqual(1, originalResult);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), new SetupProperties { Instance = instance }).Returns(Task.FromResult(expectedResult));

        var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);
        const int expectedResult = 2;

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty)).Returns(expectedResult);

        var actualResult = TestStaticClass.StaticIntProperty;
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty)).Returns(expectedResult);

        var actualResult = TestStaticClass.StaticObjectProperty;
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        ClassicAssert.AreEqual(default(int), originalValue);

        using var _ = Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), new SetupProperties { Instance = instance }).Returns(2);

        var actualResult = instance.IntProperty;
        ClassicAssert.AreEqual(2, actualResult);
    }

    [Test]
    public void TestReturnsStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        ClassicAssert.AreEqual(default, originalValue);

        using var _ = Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), new SetupProperties { Instance = instance }).Returns(typeof(int));

        var actualResult = instance.ObjectProperty;
        ClassicAssert.AreEqual(typeof(int), actualResult);
    }

    [Test]
    public void TestInstancePrivateMethodReturn1WithoutParameters()
    {
        var testInstance = new TestInstance();
        var type = testInstance.GetType();
        var methodInfo = type.GetMethod("TestPrivateMethodReturn1WithoutParameters", BindingFlags.NonPublic | BindingFlags.Instance);
        ClassicAssert.AreEqual(1, methodInfo.Invoke(testInstance, new object[] { }));
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestInstance), methodInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }).Returns(expectedResult);

        var actualResult = methodInfo.Invoke(testInstance, new object[] { });
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsPrivateIntProperty()
    {
        var testInstance = new TestInstance();
        var type = testInstance.GetType();
        var propertyInfo = type.GetProperty("PrivateIntProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        var methodInfo = propertyInfo.GetMethod;
        var originalValue = methodInfo.Invoke(testInstance, new object[] { });
        ClassicAssert.AreEqual(default(int), originalValue);
        const int expectedResult = 2;

        using var _ = Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }).Returns(expectedResult);

        var actualResult = methodInfo.Invoke(testInstance, new object[] { });
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsPrivateObjectProperty()
    {
        var testInstance = new TestInstance();
        var type = testInstance.GetType();
        var propertyInfo = type.GetProperty("PrivateObjectProperty", BindingFlags.NonPublic | BindingFlags.Instance);
        var methodInfo = propertyInfo.GetMethod;
        var originalValue = methodInfo.Invoke(testInstance, new object[] { });
        ClassicAssert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        using var _ = Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }).Returns(expectedResult);

        var actualResult = methodInfo.Invoke(testInstance, new object[] { });
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }


    [Test]
    public void TestReturnsWithTestPrivateStaticMethodReturn1WithoutParameters()
    {
        var type = typeof(TestStaticClass);
        var methodInfo = type.GetMethod("TestPrivateStaticMethodReturn1WithoutParameters", BindingFlags.Static | BindingFlags.NonPublic);

        var originalResult = methodInfo.Invoke(type, new object[] { });
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticClass), methodInfo.Name, BindingFlags.Static | BindingFlags.NonPublic)
            .Returns(expectedResult);

        var actualResult = methodInfo.Invoke(type, new object[] { });

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsPrivateStaticIntProperty()
    {
        var type = typeof(TestStaticClass);
        var propertyInfo = type.GetProperty("PrivateStaticIntProperty", BindingFlags.NonPublic | BindingFlags.Static);
        var methodInfo = propertyInfo.GetMethod;
        var originalValue = methodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(default(int), originalValue);
        const int expectedResult = 2;

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static).Returns(expectedResult);

        var actualResult = methodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestReturnsPrivateStaticObjectProperty()
    {
        var type = typeof(TestStaticClass);
        var propertyInfo = type.GetProperty("PrivateStaticObjectProperty", BindingFlags.NonPublic | BindingFlags.Static);
        var methodInfo = propertyInfo.GetMethod;
        var originalValue = methodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        using var _ = Mock.SetupProperty(typeof(TestStaticClass), propertyInfo.Name, BindingFlags.NonPublic | BindingFlags.Static).Returns(expectedResult);

        var actualResult = methodInfo.Invoke(type, new object[] { });
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new[] { typeof(int) } }).Returns(expectedResult);

        var actualResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        ClassicAssert.AreEqual(0, originalResult);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestInstance), nameof(TestInstance.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            }).Returns(expectedResult);

        var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestInstanceReturnDefaultWithoutParameters()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        ClassicAssert.AreEqual(0, originalResult);
        const int expectedResult = 2;

        using var _ = Mock.Setup(typeof(TestGenericInstance<int>), nameof(TestGenericInstance<int>.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            }).Returns(expectedResult);

        var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();

        ClassicAssert.AreNotEqual(originalResult, actualResult);
        ClassicAssert.AreEqual(expectedResult, actualResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnWithoutParameterFunc()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        const int expectedResult = 2;

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters))
                   .Returns(() => 2))
        {
            var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameterFunc()
    {
        const int parameter1 = 10;

        var originalResult = TestStaticClass.TestMethodReturnWithParameter(parameter1);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameter))
                   .Returns<int, int>(argument =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument);
                       return argument / 2;
                   }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameter(parameter1);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters2Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string)
                    }
                })
            .Returns<int, string, int>((argument1, argument2) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                return argument1 / 2;
            }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters3Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2, parameter3);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string),
                               typeof(double)
                           }
                       })
                   .Returns<int, string, double, int>((argument1, argument2, argument3) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       ClassicAssert.AreEqual(parameter3, argument3);
                       return argument1 / 2;
                   }))
        {
            var actualResult =
                TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2, parameter3);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters4Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string),
                               typeof(double),
                               typeof(int[]),
                           }
                       })
                   .Returns<int, string, double, int[], int>((
                       argument1,
                       argument2,
                       argument3,
                       argument4) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       ClassicAssert.AreEqual(parameter3, argument3);
                       ClassicAssert.AreEqual(parameter4, argument4);
                       return argument1 / 2;
                   }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                parameter1,
                parameter2,
                parameter3,
                parameter4);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters5Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string),
                               typeof(double),
                               typeof(int[]),
                               typeof(string[])
                           }
                       })
                   .Returns<int, string, double, int[], string[], int>((
                       argument1,
                       argument2,
                       argument3,
                       argument4,
                       argument5) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       ClassicAssert.AreEqual(parameter3, argument3);
                       ClassicAssert.AreEqual(parameter4, argument4);
                       ClassicAssert.AreEqual(parameter5, argument5);
                       return argument1 / 2;
                   }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters6Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string),
                               typeof(double),
                               typeof(int[]),
                               typeof(string[]),
                               typeof(char)
                           }
                       })
                   .Returns<int, string, double, int[], string[], char, int>((
                       argument1,
                       argument2,
                       argument3,
                       argument4,
                       argument5,
                       argument6) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       ClassicAssert.AreEqual(parameter3, argument3);
                       ClassicAssert.AreEqual(parameter4, argument4);
                       ClassicAssert.AreEqual(parameter5, argument5);
                       ClassicAssert.AreEqual(parameter6, argument6);
                       return argument1 / 2;
                   }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5,
                parameter6);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters7Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string),
                               typeof(double),
                               typeof(int[]),
                               typeof(string[]),
                               typeof(char),
                               typeof(bool)
                           }
                       })
                   .Returns<int, string, double, int[], string[], char, bool, int>((
                       argument1,
                       argument2,
                       argument3,
                       argument4,
                       argument5,
                       argument6,
                       argument7) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       ClassicAssert.AreEqual(parameter3, argument3);
                       ClassicAssert.AreEqual(parameter4, argument4);
                       ClassicAssert.AreEqual(parameter5, argument5);
                       ClassicAssert.AreEqual(parameter6, argument6);
                       ClassicAssert.AreEqual(parameter7, argument7);
                       return argument1 / 2;
                   }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5,
                parameter6,
                parameter7);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters8Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(
                       typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string),
                               typeof(double),
                               typeof(int[]),
                               typeof(string[]),
                               typeof(char),
                               typeof(bool),
                               typeof(TestInstance)
                           }
                       })
                   .Returns<int, string, double, int[], string[], char, bool, TestInstance, int>((
                       argument1,
                       argument2,
                       argument3,
                       argument4,
                       argument5,
                       argument6,
                       argument7,
                       argument8) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       ClassicAssert.AreEqual(parameter3, argument3);
                       ClassicAssert.AreEqual(parameter4, argument4);
                       ClassicAssert.AreEqual(parameter5, argument5);
                       ClassicAssert.AreEqual(parameter6, argument6);
                       ClassicAssert.AreEqual(parameter7, argument7);
                       ClassicAssert.AreEqual(parameter8, argument8);
                       return argument1 / 2;
                   }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5,
                parameter6,
                parameter7,
                parameter8);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters9Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();
        Func<int, int> parameter9 = x => x;

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9);

        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(
                       typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string),
                               typeof(double),
                               typeof(int[]),
                               typeof(string[]),
                               typeof(char),
                               typeof(bool),
                               typeof(TestInstance),
                               typeof(Func<int, int>)
                           }
                       })
                   .Returns<int, string, double, int[], string[], char, bool, TestInstance, Func<int, int>, int>((
                       argument1,
                       argument2,
                       argument3,
                       argument4,
                       argument5,
                       argument6,
                       argument7,
                       argument8,
                       argument9) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       ClassicAssert.AreEqual(parameter3, argument3);
                       ClassicAssert.AreEqual(parameter4, argument4);
                       ClassicAssert.AreEqual(parameter5, argument5);
                       ClassicAssert.AreEqual(parameter6, argument6);
                       ClassicAssert.AreEqual(parameter7, argument7);
                       ClassicAssert.AreEqual(parameter8, argument8);
                       ClassicAssert.AreEqual(parameter9, argument9);
                       return argument1 / 2;
                   }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                parameter1,
                parameter2,
                parameter3,
                parameter4,
                parameter5,
                parameter6,
                parameter7,
                parameter8,
                parameter9);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }


    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameterCountMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

        ClassicAssert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[] { typeof(int), typeof(string) }
            });

        Assert.Throws<Exception>(() => setup.Returns<int, int>(argument => argument / 2));
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameterTypeMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

        ClassicAssert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[] { typeof(int), typeof(string) }
            });

        Assert.Throws<Exception>(() => setup.Returns<string, string, int>((_, _) => 2));
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters2FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
                       new SetupProperties
                       {
                           MethodParametersTypes = new[]
                           {
                               typeof(int),
                               typeof(string)
                           }
                       })
                   .Returns<int, string, Task<int>>((argument1, argument2) =>
                   {
                       ClassicAssert.AreEqual(parameter1, argument1);
                       ClassicAssert.AreEqual(parameter2, argument2);
                       return Task.FromResult(argument1 / 2);
                   }))
        {
            var actualResult =
                await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnTypeMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

        ClassicAssert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[] { typeof(int), typeof(string) }
            });

        Assert.Throws<Exception>(() => setup.Returns<int, string, string>((_, _) => string.Empty));
    }

    [Test]
    public void TestGenericSetupReturnsWithoutArgumentsWithTestMethodReturnParameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;
        var executed = false;

        ClassicAssert.AreEqual(parameter1, originalResult);

        using (Mock.Setup(
                   typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                   new SetupProperties
                   {
                       MethodParametersTypes = new[] { typeof(int), typeof(string) }
                   }).Returns(() =>
               {
                   executed = true;
                   ClassicAssert.Pass("Method executed");
                   return originalResult / 2;
               }))
        {
            var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

            ClassicAssert.AreNotEqual(originalResult, actualResult);
            ClassicAssert.AreEqual(expectedResult, actualResult);
        }

        ClassicAssert.IsTrue(executed);
        ClassicAssert.AreEqual(parameter1, originalResult);
    }
    
    [Test]
    public void TestGenericSetupReturnsDateTimeUtcNow()
    {
        var expectedDate = new DateTime(2020, 4, 5);
        
        using var _ = Mock.SetupProperty(typeof(DateTime), nameof(DateTime.UtcNow))
            .Returns(expectedDate);

        Assert.That(DateTime.UtcNow, Is.EqualTo(expectedDate));
    }
    
    [Test]
    public void TestGenericSetupReturnsDateTimeNow()
    {
        var expectedDate = new DateTime(2020, 4, 5);
        
        using var _ = Mock.SetupProperty(typeof(DateTime), nameof(DateTime.Now))
            .Returns(expectedDate);

        Assert.That(DateTime.Now, Is.EqualTo(expectedDate));
    }
}