# Performance Guide

This guide provides performance information, optimization strategies, and information about the official benchmarking project for SMock.

## Table of Contents
- [Performance Overview](#performance-overview)
- [Official Benchmarks](#official-benchmarks)
- [Performance Characteristics](#performance-characteristics)
- [Optimization Strategies](#optimization-strategies)
- [Memory Management](#memory-management)
- [Performance Monitoring](#performance-monitoring)

## Performance Overview

SMock is designed with performance in mind, utilizing efficient runtime IL modification techniques to minimize overhead during test execution.

### Key Performance Metrics

| Operation | Typical Time | Acceptable Range | Notes |
|-----------|--------------|------------------|--------|
| Mock Setup | 1-2ms | < 5ms | One-time cost per mock |
| Method Interception | <0.1ms | < 0.5ms | Per method call |
| Mock Disposal | <1ms | < 2ms | Cleanup overhead |
| Memory Overhead | ~1-2KB | < 10KB | Per active mock |

### Performance Philosophy

1. **Setup Cost vs Runtime Cost**: SMock optimizes for runtime performance by accepting slightly higher setup costs
2. **Memory Efficiency**: Temporary IL modifications with minimal memory footprint
3. **Cleanup Performance**: Fast hook removal ensures no lingering overhead
4. **Scalability**: Linear performance scaling with number of mocks

## Official Benchmarks

SMock includes an official benchmarking project located at `src/StaticMock.Tests.Benchmark/` that uses BenchmarkDotNet for performance measurements.

### Running Benchmarks

To run the official benchmarks:

```bash
cd src
dotnet run --project StaticMock.Tests.Benchmark --configuration Release
```

### Current Benchmarks

The benchmark project currently includes the following performance tests:

#### Setup Default Benchmark

```csharp
[DisassemblyDiagnoser(printSource: true)]
public class TestBenchmarks
{
    [Benchmark]
    public void TestBenchmarkSetupDefault()
    {
        Mock.SetupDefault(typeof(TestStaticClass), nameof(TestStaticClass.TestVoidMethodWithoutParametersThrowsException), () =>
        {
            TestStaticClass.TestVoidMethodWithoutParametersThrowsException();
        });
    }
}
```

This benchmark measures the performance of the `Mock.SetupDefault` method, which is used for setting up default mock behavior.

### Extending Benchmarks

The benchmark project can be extended with additional performance tests. Here are some suggested benchmarks that would be valuable:

```csharp
[MemoryDiagnoser]
public class ExtendedSMockBenchmarks
{
    // Basic Sequential API benchmarks
    [Benchmark]
    public void SequentialMock_Setup()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));
    }

    [Benchmark]
    public void SequentialMock_Execution()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var _ = DateTime.Now;
    }

    // Parameter matching benchmarks
    [Benchmark]
    public void ParameterMatching_IsAny()
    {
        using var mock = Mock.Setup(() => File.Exists(It.IsAny<string>()))
            .Returns(true);

        var _ = File.Exists("test.txt");
    }

    [Benchmark]
    public void ParameterMatching_Conditional()
    {
        using var mock = Mock.Setup(() => File.ReadAllText(It.Is<string>(s => s.EndsWith(".txt"))))
            .Returns("content");

        var _ = File.ReadAllText("test.txt");
    }

    // Callback performance
    [Benchmark]
    public void MockWithCallback()
    {
        var counter = 0;
        using var mock = Mock.Setup(() => Console.WriteLine(It.IsAny<string>()))
            .Callback<string>(s => counter++);

        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"test{i}");
        }
    }

    // Multiple mocks
    [Benchmark]
    public void MultipleMocksSetup()
    {
        using var mock1 = Mock.Setup(() => DateTime.Now).Returns(new DateTime(2024, 1, 1));
        using var mock2 = Mock.Setup(() => Environment.MachineName).Returns("TEST");
        using var mock3 = Mock.Setup(() => File.Exists(It.IsAny<string>())).Returns(true);

        var _ = DateTime.Now;
        var _ = Environment.MachineName;
        var _ = File.Exists("test.txt");
    }

    // Async method benchmarks
    [Benchmark]
    public async Task AsyncMock_Setup()
    {
        using var mock = Mock.Setup(() => Task.Delay(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        await Task.Delay(100);
    }
}
```

### Performance Analysis Tools

Use these tools with the benchmark project for detailed performance analysis:

1. **BenchmarkDotNet**: Included in the project for micro-benchmarking
2. **Memory Profiler**: Add `[MemoryDiagnoser]` attribute to track allocations
3. **Disassembly Analysis**: Current `[DisassemblyDiagnoser]` shows generated IL code
4. **Hardware Counters**: Add hardware profiling for cache performance

### Benchmark Configuration

The benchmark project can be configured with different options:

```csharp
// Add to Program.cs for custom configurations
var config = DefaultConfig.Instance
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddDiagnoser(DisassemblyDiagnoser.Create(DisassemblyDiagnoserConfig.Asm))
    .AddJob(Job.Default.WithRuntime(CoreRuntime.Core80))
    .AddJob(Job.Default.WithRuntime(CoreRuntime.Core90));

BenchmarkRunner.Run<TestBenchmarks>(config);
```