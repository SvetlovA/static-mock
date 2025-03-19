using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Hierarchical.CallbackTests;

[TestFixture]
public class SetupMockCallbackTests
{
    [Test]
    public void TestActionCallback()
    {
        static void DoSomething() { }

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException), () =>
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

        Mock.SetupAction(typeof(TestInstance), nameof(TestInstance.TestVoidMethodWithoutParametersThrowsException), new SetupProperties { Instance = instance }, () =>
        {
            instance.TestVoidMethodWithoutParametersThrowsException();
        }).Callback(() =>
        {
            DoSomething();
        });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWithParameters()
    {
        const int parameter1 = 10;

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[] { typeof(int) }
                },
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1))
            .Callback<int>(argument =>
            {
                ClassicAssert.AreEqual(parameter1, argument);
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith2Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string)
                    }
                },
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2))
            .Callback<int, string>((argument1, argument2) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith3Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string),
                        typeof(double)
                    }
                },
                () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2, parameter3))
            .Callback<int, string, double>((argument1, argument2, argument3) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith4Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string),
                        typeof(double),
                        typeof(int[])
                    }
                },
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4))
            .Callback<int, string, double, int[]>((
                argument1,
                argument2,
                argument3,
                argument4) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
                ClassicAssert.AreEqual(parameter4, argument4);
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith5Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5))
            .Callback<int, string, double, int[], string[]>((
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
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith6Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6))
            .Callback<int, string, double, int[], string[], char>((
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
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith7Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7))
            .Callback<int, string, double, int[], string[], char, bool>((
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
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith8Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7,
                        parameter8))
            .Callback<int, string, double, int[], string[], char, bool, TestInstance>((
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
            });
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith9Parameters()
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

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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
                () => TestStaticClass.TestVoidMethodWithParameters(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6,
                        parameter7,
                        parameter8,
                        parameter9))
            .Callback<int, string, double, int[], string[], char, bool, TestInstance, Func<int, int>>((
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
            });
    }


    [Test]
    public void TestSetupVoidWithTestMethodParameterCountMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var setup = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[]
                {
                    typeof(int),
                    typeof(string)
                }
            },
            () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2));

        Assert.Throws<Exception>(() => setup.Callback<int>(argument => { }));
    }

    [Test]
    public void TestSetupVoidWithTestMethodParameterTypeMismatchFunc()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        var setup = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[]
                {
                    typeof(int),
                    typeof(string)
                }
            },
            () => TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2));

        Assert.Throws<Exception>(() => setup.Callback<string, string>((argument1, argument2) => { }));
    }

    [Test]
    public void TestGenericSetupVoidWithoutArgumentsWithTestMethodReturnParameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
        var executed = false;

        Mock.SetupAction(
            typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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
                TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
            }).Callback(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
        });

        ClassicAssert.IsTrue(executed);
    }
    
    [Test]
    public void TestGenericSetupVoidUnsafeStaticMethod()
    {
        TestStaticClass.TestUnsafeStaticVoidMethod();
        var executed = false;

        Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestUnsafeStaticVoidMethod), () =>
        {
            TestStaticClass.TestUnsafeStaticVoidMethod();

            Assert.That(executed, Is.True);
        }).Callback(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
        });
    }
    
    [Test]
    public void TestGenericSetupVoidUnsafeInstanceMethod()
    {
        var instance = new TestInstance();
        instance.TestUnsafeInstanceVoidMethod();
        var executed = false;

        Mock.SetupAction(instance.GetType(), nameof(instance.TestUnsafeInstanceVoidMethod), new SetupProperties { Instance = instance }, () =>
        {
            instance.TestUnsafeInstanceVoidMethod();

            Assert.That(executed, Is.True);
        }).Callback(() =>
        {
            executed = true;
            Assert.Pass("Method executed");
        });
    }
}