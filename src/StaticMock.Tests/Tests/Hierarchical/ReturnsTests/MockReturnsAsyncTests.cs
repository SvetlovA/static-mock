using NUnit.Framework;
using StaticMock.Entities;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Tests.Hierarchical.ReturnsTests;

[TestFixture]
public class MockReturnsAsyncTests
{
    [Test]
    public async Task TestSetupReturnsAsyncMethodsReturnTask()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestSetupReturnsAsyncMethodsReturnTaskAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync), async () =>
        {
            var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestSetupInstanceReturnsAsyncMethodsReturnTask()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParameters();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParameters), new SetupProperties { Instance = instance }, async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParameters();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestSetupInstanceReturnsAsyncMethodsReturnTaskAsync()
    {
        var instance = new TestInstance();
        var originalResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
        Assert.AreEqual(1, originalResult);
        var expectedResult = 2;

        Mock.Setup(typeof(TestInstance), nameof(TestInstance.TestMethodReturnTaskWithoutParametersAsync), new SetupProperties { Instance = instance }, async () =>
        {
            var actualResult = await instance.TestMethodReturnTaskWithoutParametersAsync();
            Assert.AreEqual(expectedResult, actualResult);
        }).ReturnsAsync(expectedResult);
    }

    [Test]
    public async Task TestGenericSetupReturnsWithTestMethodReturnWithoutParameterFuncAsync()
    {
        var originalResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();
        const int expectedResult = 2;

        Mock.Setup(
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync),
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnTaskWithoutParametersAsync();

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns(() => Task.FromResult(2));

        Assert.AreEqual(1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameterFuncAsync()
    {
        const int parameter1 = 10;

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParameterAsync(parameter1);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParameterAsync),
            async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParameterAsync(parameter1);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, Task<int>>(async argument =>
            {
                await Task.CompletedTask;
                Assert.AreEqual(parameter1, argument);
                return argument / 2;
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters2FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
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
            .Returns<int, string, Task<int>>(async (argument1, argument2) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                return await Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters3FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2, parameter3);
        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
            typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
            new SetupProperties
            {
                MethodParametersTypes = new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(double)
                }
            },
            async () =>
            {
                var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(parameter1, parameter2, parameter3);

                Assert.AreNotEqual(originalResult, actualResult);
                Assert.AreEqual(expectedResult, actualResult);
            })
            .Returns<int, string, double, Task<int>>((argument1, argument2, argument3) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                return Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters4FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4);

        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
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
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], Task<int>>(async (
                argument1,
                argument2,
                argument3,
                argument4) =>
            {
                Assert.AreEqual(parameter1, argument1);
                Assert.AreEqual(parameter2, argument2);
                Assert.AreEqual(parameter3, argument3);
                Assert.AreEqual(parameter4, argument4);
                return await Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters5FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5);

        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
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
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], Task<int>>((
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
                return Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters6FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
            parameter1,
            parameter2,
            parameter3,
            parameter4,
            parameter5,
            parameter6);

        var expectedResult = originalResult / 2;

        Assert.AreEqual(parameter1, originalResult);

        Mock.Setup(
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
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
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
                        parameter1,
                        parameter2,
                        parameter3,
                        parameter4,
                        parameter5,
                        parameter6);

                    Assert.AreNotEqual(originalResult, actualResult);
                    Assert.AreEqual(expectedResult, actualResult);
                })
            .Returns<int, string, double, int[], string[], char, Task<int>>(async (
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
                return await Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters7FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
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
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
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
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
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
            .Returns<int, string, double, int[], string[], char, bool, Task<int>>((
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

                return Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters8FuncAsync()
    {
        const int parameter1 = 10;
        const string parameter2 = "testString";
        const double parameter3 = 10;
        var parameter4 = new[] { 10, 20 };
        var parameter5 = new[] { parameter2, parameter1.ToString() };
        const char parameter6 = 'a';
        const bool parameter7 = true;
        var parameter8 = new TestInstance();

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
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
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
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
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
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
            .Returns<int, string, double, int[], string[], char, bool, TestInstance, Task<int>>(async (
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

                return await Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }

    [Test]
    public async Task TestSetupReturnsWithTestMethodReturnParameters9FuncAsync()
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

        var originalResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
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
                typeof(TestStaticAsyncClass), nameof(TestStaticAsyncClass.TestMethodReturnWithParametersAsync),
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
                async () =>
                {
                    var actualResult = await TestStaticAsyncClass.TestMethodReturnWithParametersAsync(
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
            .Returns<int, string, double, int[], string[], char, bool, TestInstance, Func<int, int>, Task<int>>((
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
                return Task.FromResult(argument1 / 2);
            });

        Assert.AreEqual(parameter1, originalResult);
    }
}