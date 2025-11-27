using BenchmarkDotNet.Attributes;
using StaticMock.Tests.Common.TestEntities;

namespace StaticMock.Tests.Benchmark;

[DisassemblyDiagnoser(printSource: true)]
public class TestBenchmarks
{
    [Benchmark]
    public void TestBenchmarkSetupDefault()
    {
        Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException), () =>
        {
            // Fixed: Don't call the original method to avoid infinite recursion
            // Just perform a simple operation for benchmarking
        });
    }
}