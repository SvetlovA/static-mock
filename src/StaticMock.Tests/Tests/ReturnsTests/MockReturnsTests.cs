﻿using System.Reflection;
using NUnit.Framework;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.ReturnsTests;

[TestFixture]
public class MockReturnsTests
{
    [Test]
    public void TestReturnsWithTestMethodReturn1WithoutParameters()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturn1WithoutParameters),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsWithTestMethodReturnParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturnWithParameter(10);
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturnWithParameter),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameter(10);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
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

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturnReferenceObject),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnReferenceObject();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsWithTestMethodOutParameter()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturn1WithOutParameter),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithOutParameter(out _);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestReturnsWithTestMethodRefParameter()
    {
        var x = 1;
        var originalResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref x);
        var expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass),
                nameof(TestStaticClass.TestMethodReturn1WithRefParameter),
                () =>
                {
                    var y = 1;
                    var actualResult = TestStaticClass.TestMethodReturn1WithRefParameter(ref y);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(expectedResult);
    }

    [Test]
    public void TestInstanceMethodReturns()
    {
        var testInstance = new TestInstance();
        Assert.AreEqual(1, testInstance.TestMethodReturn1WithoutParameters());
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturn1WithoutParameters), new SetupProperties { Instance = testInstance }, () =>
        {
            var actualResult = testInstance.TestMethodReturn1WithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public async Task TestSetupReturnsMethodsReturnTask()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestSetupReturnsMethodsReturnTaskAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestSetupInstanceReturnsMethodsReturnTask()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), new SetupProperties { Instance = instance }, async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public async Task TestSetupInstanceReturnsMethodsReturnTaskAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), new SetupProperties { Instance = instance }, async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(Task.FromResult(expectedResult));
    }

    [Test]
    public void TestReturnsStaticIntProperty()
    {
        var originalValue = TestStaticClass.StaticIntProperty;
        Assert.AreEqual(default(int), originalValue);
        var expectedResult = 2;

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticIntProperty), () =>
        {
            var actualResult = TestStaticClass.StaticIntProperty;
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsStaticObjectProperty()
    {
        var originalValue = TestStaticClass.StaticObjectProperty;
        Assert.AreEqual(default, originalValue);
        var expectedResult = new TestInstance();

        Mock.SetupProperty(typeof(TestStaticClass), nameof(TestStaticClass.StaticObjectProperty), () =>
        {
            var actualResult = TestStaticClass.StaticObjectProperty;
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
    }

    [Test]
    public void TestReturnsStaticIntPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.IntProperty;
        Assert.AreEqual(default(int), originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.IntProperty), new SetupProperties { Instance = instance }, () =>
        {
            var actualResult = instance.IntProperty;
            Assert.AreEqual(2, actualResult);
        }).Returns(2);
    }

    [Test]
    public void TestReturnsStaticObjectPropertyInstance()
    {
        var instance = new TestInstance();
        var originalValue = instance.ObjectProperty;
        Assert.AreEqual(default, originalValue);

        Mock.SetupProperty(typeof(TestInstance), nameof(TestInstance.ObjectProperty), new SetupProperties { Instance = instance }, () =>
        {
            var actualResult = instance.ObjectProperty;
            Assert.AreEqual(typeof(int), actualResult);
        }).Returns(typeof(int));
    }

    [Test]
    public void TestInstancePrivateMethodReturn1WithoutParameters()
    {
        var testInstance = new TestInstance();
        Type type = testInstance.GetType();
        MethodInfo methodInfo = type.GetMethod("TestPrivateMethodReturn1WithoutParameters", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.AreEqual(1, methodInfo.Invoke(testInstance, new object[] { }));
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), methodInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }, () =>
        {
            var actualResult = methodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
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

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }, () =>
        {
            var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
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

        Mock.SetupProperty(typeof(TestInstance), propertyInfo.Name, new SetupProperties
        {
            Instance = testInstance,
            BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance
        }, () =>
        {
            var actualResult = mothodInfo.Invoke(testInstance, new object[] { });
            Assert.AreEqual(expectedResult, actualResult);
        }).Returns(expectedResult);
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
            .Returns(expectedResult);
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
        }).Returns(expectedResult);
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
        }).Returns(expectedResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestMethodReturnDefaultWithoutParameters()
    {
        var originalResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters), new SetupProperties { GenericTypes = new[] { typeof(int) } },
            () =>
            {
                var actualResult = TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestMethodReturnDefaultWithoutParametersInstance()
    {
        var testInstance = new TestInstance();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();
        Assert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            },
            () =>
            {
                var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters<int>();

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
    }

    [Test]
    public void TestSetupReturnsWithGenericTestInstanceReturnDefaultWithoutParameters()
    {
        var testInstance = new TestGenericInstance<int>();
        var originalResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();
        Assert.AreEqual(0, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestGenericInstance<int>), nameof(TestGenericInstance<int>.GenericTestMethodReturnDefaultWithoutParameters),
            new SetupProperties
            {
                Instance = testInstance,
                GenericTypes = new[] { typeof(int) }
            },
            () =>
            {
                var actualResult = testInstance.GenericTestMethodReturnDefaultWithoutParameters();

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(expectedResult);
    }

    [Test]
    public void TestGenericSetupReturnsWithTestMethodReturnWithoutParameterFunc()
    {
        var originalResult = TestStaticClass.TestMethodReturn1WithoutParameters();
        const int expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturn1WithoutParameters),
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturn1WithoutParameters();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(() => 2);

        Assert.AreEqual(1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameterFunc()
    {
        const int parameter1 = 10;

        var originalResult = TestStaticClass.TestMethodReturnWithParameter(parameter1);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameter),
            () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameter(parameter1);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, int>(argument =>
            {
                Assert.AreEqual(parameter1, argument);
                return argument / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters2Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string)
                    }
                },
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, int>((argument1, argument2) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameters3Func()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2, parameter3);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(double)
                }
            },
            () =>
            {
                var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2, parameter3);

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            })
            .Returns<int, string, double, int>((argument1, argument2, argument3) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
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

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string),
                        typeof(double),
                        typeof(int[]),
                    }
                },
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], int>((
                argument1,
                argument2,
                argument3,
                argument4) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
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

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
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
                },
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], int>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
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

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
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
                        typeof(char)
                    }
                },
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], char, int>((
                argument1,
                argument2,
                argument3,
                argument4,
                argument5,
                argument6) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
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

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
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
                        typeof(bool)
                    }
                },
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
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
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
                Assert.AreEqual(parameter7, argument7);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
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

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
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
                },
                () =>
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

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
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
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
                Assert.AreEqual(parameter7, argument7);
                Assert.AreEqual(parameter8, argument8);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
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

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
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
                },
                () =>
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

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
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
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                Assert.AreEqual(parameter5, argument5);
                Assert.AreEqual(parameter6, argument6);
                Assert.AreEqual(parameter7, argument7);
                Assert.AreEqual(parameter8, argument8);
                Assert.AreEqual(parameter9, argument9);
                return argument1 / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
    }


    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameterCountMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[] { typeof(int), typeof(string) }
            },
            () =>
            {
                var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            });

        Assert.Throws<Exception>(() => setup.Returns<int, int>(argument => argument / 2));
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnParameterTypeMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[] { typeof(int), typeof(string) }
            },
                () =>
                {
                    var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                });

        Assert.Throws<Exception>(() => setup.Returns<string, string, int>((argument1, argument2) => 2));
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters2FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string)
                    }
                },
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, Task<int>>((argument1, argument2) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                return Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public void TestSetupReturnsWithTestMethodReturnTypeMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        var setup = Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[] { typeof(int), typeof(string) }
            },
            () =>
            {
                var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            });

        Assert.Throws<Exception>(() => setup.Returns<int, string, string>((argument1, argument2) => string.Empty));
    }

    [Test]
    public void TestGenericSetupReturnsWithoutArgumentsWithTestMethodReturnParameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var originalResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);
        var expectedResult = originalResult / 2;
        var executed = false;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
            typeof(TestStaticClass), nameof(TestStaticClass.TestMethodReturnWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[] { typeof(int), typeof(string) }
            },
            () =>
            {
                var actualResult = TestStaticClass.TestMethodReturnWithParameters(parameter1, parameter2);

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            }).Returns(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
            return originalResult / 2;
        });

        Assert.IsTrue(executed);
        Assert.AreEqual(parameter1, originalResult);
    }
}