using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Sequential.CallbackTests;

[TestFixture]
public class SetupMockCallbackTests
{
    [Test]
    public void TestActionCallback()
    {
        static void DoSomething() { }

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException)).Callback(() =>
        {
            DoSomething();
        });

        TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
    }

    [Test]
    public void TestActionCallbackInstance()
    {
        var instance = new TestInstance();

        static void DoSomething() { }

        using var _ = Mock.SetupAction(typeof(TestInstance), nameof(TestInstance.TestVoidMethodWithoutParametersThrowsException), new SetupProperties { Instance = instance }).Callback(() =>
        {
            DoSomething();
        });

        instance.TestVoidMethodWithoutParametersThrowsException();
    }

    [Test]
    public void TestSetupWithTestVoidMethodWithParameters()
    {
        const int parameter1 = 10;

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[] { typeof(int) }
                })
            .Callback<int>(argument =>
            {
                ClassicAssert.AreEqual(parameter1, argument);
            });

        TestStaticClass.TestVoidMethodWithParameters(parameter1);
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith2Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string)
                    }
                })
            .Callback<int, string>((argument1, argument2) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
            });

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith3Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string),
                        typeof(double)
                    }
                })
            .Callback<int, string, double>((argument1, argument2, argument3) =>
            {
                ClassicAssert.AreEqual(parameter1, argument1);
                ClassicAssert.AreEqual(parameter2, argument2);
                ClassicAssert.AreEqual(parameter3, argument3);
            });

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2, parameter3);
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith4Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
                new SetupProperties
                {
                    MethodParametersTypes = new[]
                    {
                        typeof(int),
                        typeof(string),
                        typeof(double),
                        typeof(int[])
                    }
                })
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

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4);
    }

    [Test]
    public void TestSetupWithTestVoidMethodWith5Parameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5);
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

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6);
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

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7);
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

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8);
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

        using var _ = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
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

        TestStaticClass.TestVoidMethodWithParameters(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6,
            parameter7,
            parameter8,
            parameter9);
    }


    [Test]
    public void TestSetupVoidWithTestMethodParameterCountMismatchFunc()
    {
        var setup = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[]
                {
                    typeof(int),
                    typeof(string)
                }
            });

        Assert.Throws<Exception>(() => setup.Callback<int>(argument => { }));
    }

    [Test]
    public void TestSetupVoidWithTestMethodParameterTypeMismatchFunc()
    {
        var setup = Mock.SetupAction(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[]
                {
                    typeof(int),
                    typeof(string)
                }
            });

        Assert.Throws<Exception>(() => setup.Callback<string, string>((argument1, argument2) => { }));
    }

    [Test]
    public void TestGenericSetupVoidWithoutArgumentsWithTestMethodReturnParameters()
    {
        const int parameter1 = 10;
        const string parameter2 = "parameter2";

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);
        var executed = false;

        using var _ = Mock.SetupAction(
            typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithParameters),
            new SetupProperties
            {
                MethodParametersTypes = new[]
                {
                    typeof(int),
                    typeof(string)
                }
            }).Callback(() =>
        {
            executed = true;
            ClassicAssert.Pass("Method executed");
        });

        TestStaticClass.TestVoidMethodWithParameters(parameter1, parameter2);

        ClassicAssert.IsTrue(executed);
    }
}