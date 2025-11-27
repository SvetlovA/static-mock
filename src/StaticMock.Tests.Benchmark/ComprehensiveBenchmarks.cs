using BenchmarkDotNet.Attributes;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Benchmark;

[MemoryDiagnoser]
public class ComprehensiveBenchmarks
{
    private TestInstance _testInstance = null!;

    [GlobalSetup]
    public void Setup()
    {
        _testInstance = new TestInstance();
    }

    #region Basic Sequential API Benchmarks

    [Benchmark]
    public void SequentialMock_Setup_StaticMethodWithReturn()
    {
        using var mock = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
            .Returns(42);
    }

    [Benchmark]
    public void SequentialMock_Execution_StaticMethodWithReturn()
    {
        using var mock = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
            .Returns(42);

        TestStaticClass.TestMethodReturn1WithoutParameters();
    }

    #endregion

    #region Parameter Matching Benchmarks

    [Benchmark]
    public void ParameterMatching_ExactMatch()
    {
        using var mock = Mock.Setup(() => TestStaticClass.TestMethodReturnWithParameter(100))
            .Returns(42);

        TestStaticClass.TestMethodReturnWithParameter(100);
    }

    [Benchmark]
    public void ParameterMatching_IsAny()
    {
        using var mock = Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()))
            .Returns(42);

        TestStaticClass.TestMethodReturnWithParameter(100);
    }

    [Benchmark]
    public void ParameterMatching_Conditional()
    {
        using var mock = Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.Is<int>(x => x > 50)))
            .Returns(42);

        TestStaticClass.TestMethodReturnWithParameter(100);
    }

    #endregion

    #region Callback Benchmarks

    [Benchmark]
    public void MockWithSimpleCallback()
    {
        using var mock = Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameter(context.It.IsAny<int>()))
            .Callback<int>(_ => { /* Do nothing */ });

        for (var i = 0; i < 10; i++)
        {
            TestStaticClass.TestVoidMethodWithParameter(i);
        }
    }

    [Benchmark]
    public void MockWithComplexCallback()
    {
        var sum = 0;
        using var mock = Mock.Setup(context => TestStaticClass.TestVoidMethodWithParameter(context.It.IsAny<int>()))
            .Callback<int>(x =>
            {
                sum += x * x;
                if (sum > 1000) sum = 0;
            });

        for (var i = 0; i < 10; i++)
        {
            TestStaticClass.TestVoidMethodWithParameter(i);
        }
    }

    #endregion

    #region Async Method Benchmarks

    [Benchmark]
    public async Task AsyncMock_Setup_TaskMethod()
    {
        using var mock = Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTask())
            .Returns(Task.CompletedTask);

        await TestStaticAsyncClass.TestMethodReturnTask();
    }

    [Benchmark]
    public async Task AsyncMock_Setup_TaskWithReturn()
    {
        using var mock = Mock.Setup(() => TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters())
            .Returns(Task.FromResult(42));

        await TestStaticAsyncClass.TestMethodReturnTaskWithoutParameters();
    }

    [Benchmark]
    public async Task AsyncMock_ParameterMatching()
    {
        using var mock = Mock.Setup(context => TestStaticAsyncClass.TestMethodReturnWithParameterAsync(context.It.IsAny<int>()))
            .Returns(Task.FromResult(42));

        for (var i = 0; i < 5; i++)
        {
            await TestStaticAsyncClass.TestMethodReturnWithParameterAsync(i);
        }
    }

    #endregion

    #region Multiple Mocks Benchmarks

    [Benchmark]
    public void MultipleMocks_Setup_ThreeMocks()
    {
        using var mock1 = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
            .Returns(1);
        using var mock3 = Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()))
            .Returns(42);
        using var mock4 = Mock.Setup(() => TestStaticClass.TestMethodReturnReferenceObject())
            .Returns(new TestInstance());
    }

    [Benchmark]
    public void MultipleMocks_Execution_ThreeMocks()
    {
        using var mock1 = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
            .Returns(1);
        using var mock3 = Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameter(context.It.IsAny<int>()))
            .Returns(42);
        using var mock4 = Mock.Setup(() => TestStaticClass.TestMethodReturnReferenceObject())
            .Returns(new TestInstance());

        TestStaticClass.TestMethodReturn1WithoutParameters();
        TestStaticClass.TestMethodReturnWithParameter(100);
        TestStaticClass.TestMethodReturnReferenceObject();
    }

    #endregion

    #region Instance vs Static Benchmarks

    [Benchmark]
    public void InstanceMock_Setup()
    {
        using var mock = Mock.Setup(() => _testInstance.TestMethodReturn1WithoutParameters())
            .Returns(42);
    }

    [Benchmark]
    public void InstanceMock_Execution()
    {
        using var mock = Mock.Setup(() => _testInstance.TestMethodReturn1WithoutParameters())
            .Returns(42);

        _testInstance.TestMethodReturn1WithoutParameters();
    }

    [Benchmark]
    public void StaticMock_Setup()
    {
        using var mock = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
            .Returns(42);
    }

    [Benchmark]
    public void StaticMock_Execution()
    {
        using var mock = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
            .Returns(42);

        TestStaticClass.TestMethodReturn1WithoutParameters();
    }

    #endregion

    #region Generic Method Benchmarks

    [Benchmark]
    public void GenericMock_Setup()
    {
        using var mock = Mock.Setup(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>())
            .Returns(42);
    }

    [Benchmark]
    public void GenericMock_Execution()
    {
        using var mock = Mock.Setup(() => TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>())
            .Returns(42);

        TestStaticClass.GenericTestMethodReturnDefaultWithoutParameters<int>();
    }

    #endregion

    #region Property Mocking Benchmarks

    [Benchmark]
    public void PropertyMock_Setup()
    {
        using var mock = Mock.Setup(() => TestStaticClass.StaticIntProperty)
            .Returns(42);
    }

    [Benchmark]
    public void PropertyMock_Execution()
    {
        using var mock = Mock.Setup(() => TestStaticClass.StaticIntProperty)
            .Returns(42);

        _ = TestStaticClass.StaticIntProperty;
    }

    #endregion

    #region Complex Parameter Benchmarks

    [Benchmark]
    public void ComplexParameters_MultipleParameters()
    {
        using var mock = Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(
            context.It.IsAny<int>(),
            context.It.IsAny<string>(),
            context.It.IsAny<double>()))
            .Returns(42);

        TestStaticClass.TestMethodReturnWithParameters(1, "test", 3.14);
    }

    [Benchmark]
    public void ComplexParameters_ArrayParameter()
    {
        using var mock = Mock.Setup(context => TestStaticClass.TestMethodReturnWithParameters(
            context.It.IsAny<int>(),
            context.It.IsAny<int[]>()))
            .Returns(42);

        TestStaticClass.TestMethodReturnWithParameters(1, [1, 2, 3]);
    }

    #endregion

    #region Memory Intensive Benchmarks

    [Benchmark]
    public void MemoryIntensive_SetupAndDispose_100Times()
    {
        for (var i = 0; i < 100; i++)
        {
            using var mock = Mock.Setup(() => TestStaticClass.TestMethodReturn1WithoutParameters())
                .Returns(i);

            TestStaticClass.TestMethodReturn1WithoutParameters();
        }
    }

    [Benchmark]
    public void MemoryIntensive_ComplexObjectReturn()
    {
        using var mock = Mock.Setup(() => TestStaticClass.TestMethodReturnReferenceObject())
            .Returns(new TestInstance { IntProperty = 42, ObjectProperty = "test" });

        for (var i = 0; i < 50; i++)
        {
            TestStaticClass.TestMethodReturnReferenceObject();
        }
    }

    #endregion
}