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

SMock is designed with performance in mind, utilizing efficient runtime IL modification techniques to minimize overhead during test execution. The following performance metrics are based on comprehensive benchmarking across multiple .NET framework versions.

### Key Performance Metrics by Framework

#### Modern .NET (.NET 8.0/9.0/10.0)

| Operation | Mean Time | Memory Allocation | Notes |
|-----------|-----------|-------------------|--------|
| Static Mock Setup | 600-730 μs | 7-10 KB | Fast static method interception |
| Static Mock Execution | 580-710 μs | 7-10 KB | Minimal runtime overhead |
| Instance Mock Setup | 1,050 μs | 10 KB | Higher cost for instance methods |
| Instance Mock Execution | 625-670 μs | 10 KB | Efficient execution |
| Parameter Matching (Exact) | 1,330-1,360 μs | 8-11 KB | Literal parameter matching |
| Parameter Matching (IsAny) | 8,100-8,600 μs | 18-22 KB | Dynamic parameter matching |
| Callback Operations | 950-2,670 μs | 16-21 KB | Depends on callback complexity |
| Async Method Mocking | 1,750-2,300 μs | 7-20 KB | Task-based operations |

#### .NET Framework (4.6.2 - 4.8.1)

| Operation | Mean Time | Memory Allocation | Notes |
|-----------|-----------|-------------------|--------|
| Static Mock Setup | 1.05-1.25 ms | 104 KB | Legacy runtime overhead |
| Static Mock Execution | 1.08-1.26 ms | 104 KB | Higher execution cost |
| Instance Mock Setup | 109-118 ms | 120 KB | Significantly higher setup cost |
| Instance Mock Execution | 1.6-1.7 ms | 128 KB | Acceptable execution performance |
| Parameter Matching (Exact) | 2.1-2.5 ms | 112 KB | Good literal matching |
| Parameter Matching (IsAny) | 4.1-4.9 ms | 120 KB | Better than modern .NET |
| Callback Operations | 1.5-4.1 ms | 104 KB | Consistent performance |
| Async Method Mocking | 2.7-10.2 ms | 112-144 KB | Variable async overhead |

### Performance Philosophy

1. **Setup Cost vs Runtime Cost**: SMock optimizes for runtime performance by accepting slightly higher setup costs
2. **Memory Efficiency**: Temporary IL modifications with minimal memory footprint
3. **Cleanup Performance**: Fast hook removal ensures no lingering overhead
4. **Scalability**: Linear performance scaling with number of mocks

## Official Benchmarks

SMock includes a comprehensive benchmarking project located at `src/StaticMock.Tests.Benchmark/` that uses BenchmarkDotNet for performance measurements across multiple .NET framework versions.

### Running Benchmarks

To run all benchmarks across supported frameworks:

```bash
cd src
# Modern .NET versions
dotnet run --project StaticMock.Tests.Benchmark --framework net8.0
dotnet run --project StaticMock.Tests.Benchmark --framework net9.0
dotnet run --project StaticMock.Tests.Benchmark --framework net10.0

# .NET Framework versions
dotnet run --project StaticMock.Tests.Benchmark --framework net462
dotnet run --project StaticMock.Tests.Benchmark --framework net47
dotnet run --project StaticMock.Tests.Benchmark --framework net471
dotnet run --project StaticMock.Tests.Benchmark --framework net472
dotnet run --project StaticMock.Tests.Benchmark --framework net48
dotnet run --project StaticMock.Tests.Benchmark --framework net481
```

### Comprehensive Benchmark Suite

The benchmark project includes 24+ performance tests covering all major SMock functionality:

#### Sequential API Benchmarks
- **Setup and Execution**: Mock creation and method interception performance
- **Parameter Matching**: Exact values, `It.IsAny<T>()`, and conditional matching
- **Callback Operations**: Simple and complex callback execution

#### Async Method Benchmarks
- **Task Methods**: Async void and Task<T> return types
- **Parameter Matching**: Async method parameter validation

#### Multiple Mock Scenarios
- **Concurrent Mocks**: Performance with multiple active mocks
- **Memory Intensive**: 100+ mock creation/disposal cycles

#### Instance vs Static Comparison
- **Static Method Mocking**: Performance characteristics
- **Instance Method Mocking**: Memory and time overhead comparison

### Framework Performance Comparison

Based on comprehensive benchmarking across .NET 8.0/9.0/10.0 and .NET Framework 4.6.2-4.8.1:

#### Performance Highlights
- **Best Overall Performance**: Modern .NET (8.0/9.0/10.0) for most operations
- **Parameter Matching Exception**: .NET Framework performs better with `IsAny<T>()` (4ms vs 8ms)
- **Instance Mocking Cost**: Significantly higher on .NET Framework (109-118ms setup vs 1ms)
- **Memory Efficiency**: Modern .NET uses ~10x less memory (6-31KB vs 104-304KB)
- **Framework Consistency**: All .NET Framework versions (4.6.2-4.8.1) show nearly identical performance

#### Recommended Framework Selection
- **Modern .NET (.NET 8+)**: Best for new projects requiring optimal performance
- **.NET Framework (any version 4.6.2+)**: Acceptable for legacy projects, with parameter matching advantages
- **Version Agnostic**: Performance is consistent across all .NET Framework versions tested

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
        using var mock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
            .Returns(true);

        var _ = File.Exists("test.txt");
    }

    [Benchmark]
    public void ParameterMatching_Conditional()
    {
        using var mock = Mock.Setup(context => File.ReadAllText(context.It.Is<string>(s => s.EndsWith(".txt"))))
            .Returns("content");

        var _ = File.ReadAllText("test.txt");
    }

    // Callback performance
    [Benchmark]
    public void MockWithCallback()
    {
        var counter = 0;
        using var mock = Mock.Setup(context => Console.WriteLine(context.It.IsAny<string>()))
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
        using var mock3 = Mock.Setup(context => File.Exists(context.It.IsAny<string>())).Returns(true);

        var _ = DateTime.Now;
        var _ = Environment.MachineName;
        var _ = File.Exists("test.txt");
    }

    // Async method benchmarks
    [Benchmark]
    public async Task AsyncMock_Setup()
    {
        using var mock = Mock.Setup(context => Task.Delay(context.It.IsAny<int>()))
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

### Detailed Benchmark Results

#### .NET 8.0/9.0/10.0 Performance Matrix

| Benchmark | .NET 8.0 (μs) | .NET 9.0 (μs) | .NET 10.0 (μs) | Memory (KB) |
|-----------|---------------|---------------|----------------|-------------|
| SequentialMock_Setup_StaticMethodWithReturn | 547,000 | 534,000 | 562,000 | 6.1-6.2 |
| SequentialMock_Execution_StaticMethodWithReturn | 707 | 694 | 792 | 7.7-10.4 |
| ParameterMatching_ExactMatch | 1,348 | 1,360 | 1,329 | 8.1-10.8 |
| ParameterMatching_IsAny | 8,592 | 8,131 | 8,270 | 18.6-21.6 |
| ParameterMatching_Conditional | 1,717 | 1,695 | 1,641 | 19.5-22.2 |
| MockWithSimpleCallback | 2,646 | 2,570 | 2,664 | 16.7-20.9 |
| MockWithComplexCallback | 944 | 1,090 | 976 | 16.8-21.1 |
| AsyncMock_Setup_TaskMethod | 2,161 | 2,300 | 2,184 | 7.1-10.7 |
| AsyncMock_Setup_TaskWithReturn | 1,758 | 1,814 | 1,825 | 8.3-11.0 |
| MultipleMocks_Setup_ThreeMocks | 1,430 | 1,518 | 1,441 | 27.4-31.6 |
| MultipleMocks_Execution_ThreeMocks | 1,260 | 1,265 | 1,386 | 28.3-31.6 |
| StaticMock_Setup | 594 | 731 | 630 | 7.7-10.4 |
| StaticMock_Execution | 586 | 713 | 606 | 7.7-10.4 |
| MemoryIntensive_SetupAndDispose_100Times | 9,419 | 9,160 | 9,875 | 565-571 |

#### Complete .NET Framework Performance Matrix (4.6.2 - 4.8.1)

| Benchmark | 4.6.2 (ms) | 4.7 (ms) | 4.7.1 (ms) | 4.7.2 (ms) | 4.8 (ms) | 4.8.1 (ms) | Memory (KB) |
|-----------|------------|----------|------------|------------|----------|------------|-------------|
| SequentialMock_Setup_StaticMethodWithReturn | 520.7 | 503.6 | 503.4 | 503.8 | 510.3 | 504.2 | 104 |
| SequentialMock_Execution_StaticMethodWithReturn | 1.215 | 1.257 | 1.181 | 1.202 | 1.167 | 1.183 | 104 |
| ParameterMatching_ExactMatch | 2.192 | 2.272 | 2.511 | 2.114 | 2.121 | 2.114 | 112 |
| ParameterMatching_IsAny | 4.265 | 4.894 | 4.261 | 4.207 | 4.456 | 4.136 | 120 |
| ParameterMatching_Conditional | 1.750 | 1.831 | 1.792 | 1.647 | 1.689 | 1.716 | 120 |
| MockWithSimpleCallback | 3.665 | 3.891 | 4.082 | 3.814 | 3.552 | 3.777 | 104 |
| MockWithComplexCallback | 1.612 | 1.643 | 1.621 | 1.597 | 1.475 | 1.481 | 104 |
| AsyncMock_Setup_TaskMethod | 4.157 | 3.782 | 3.532 | 3.458 | 3.407 | 3.332 | 112 |
| AsyncMock_Setup_TaskWithReturn | 9.870 | 9.507 | 10.244 | 9.610 | 9.501 | 9.556 | 128 |
| StaticMock_Setup | 1.118 | 1.234 | 1.081 | 1.108 | 1.144 | 1.053 | 104 |
| StaticMock_Execution | 1.132 | 1.215 | 1.067 | 1.152 | 1.197 | 1.083 | 104 |
| InstanceMock_Setup | 116.3 | 117.8 | 113.1 | 109.2 | 110.5 | 111.7 | 120 |

#### Key .NET Framework Observations

**Remarkable Performance Consistency**: All .NET Framework versions from 4.6.2 to 4.8.1 show nearly identical performance characteristics:
- **Setup Times**: Consistently ~503-521ms for initial mock setup
- **Memory Usage**: Uniform 104-304KB allocations across all versions
- **Execution Performance**: Minimal variation in method interception times

**Minor Version Improvements**:
- **Instance Mock Setup**: Slight improvement from 116.3ms (.NET 4.6.2) to 109.2ms (.NET 4.7.2)
- **Async Method Setup**: Gradual improvement in TaskMethod setup from 4.157ms to 3.332ms across versions
- **Parameter Matching**: Consistent 4.1-4.9ms range with minimal variation

## Performance Characteristics

Based on comprehensive benchmarking, SMock exhibits the following performance characteristics:

### Setup vs Execution Performance

SMock follows a **high setup cost, low execution cost** model:

1. **Initial Setup Cost**: The first mock setup incurs significant overhead (500+ ms) due to:
   - MonoMod hook installation
   - IL code generation and JIT compilation
   - Runtime type analysis

2. **Subsequent Operations**: Once hooks are established, operations are efficient:
   - Static method execution: 580-730 μs (modern .NET)
   - Instance method execution: 625-670 μs (modern .NET)
   - Parameter matching: 1.3-8.6 ms depending on complexity

### Memory Usage Patterns

| Framework | Typical Allocation | Peak Usage | GC Pressure |
|-----------|-------------------|------------|-------------|
| .NET 8.0/9.0/10.0 | 6-31 KB | 571 KB (100 mocks) | Low |
| .NET Framework 4.8+ | 104-304 KB | 2+ MB (100 mocks) | Medium |

### Scaling Characteristics

- **Linear Scaling**: Performance scales linearly with the number of active mocks
- **Memory Intensive Operations**: 100 mock creations complete in ~9-10ms on modern .NET
- **Concurrent Mocks**: Multiple active mocks perform consistently without interference

### Framework-Specific Observations

#### Modern .NET Advantages
- **Lower Memory Footprint**: 10x less memory usage than .NET Framework
- **Faster Basic Operations**: Static and instance mocking 50-80% faster
- **Better JIT Optimization**: More efficient runtime compilation

#### .NET Framework Advantages
- **Parameter Matching**: `IsAny<T>()` operations ~50% faster than modern .NET
- **Predictable Performance**: More consistent timing across different operations
- **Lower Variability**: Less variation in benchmark results

## Optimization Strategies

Based on benchmark data analysis, follow these strategies to optimize SMock performance in your tests:

### 1. Minimize Parameter Matching Overhead

**Problem**: `It.IsAny<T>()` adds 6-7x overhead compared to exact value matching

```csharp
// ❌ Slower - Dynamic parameter matching (8.5ms)
Mock.Setup(context => MyClass.Method(context.It.IsAny<string>()))
    .Returns(42);

// ✅ Faster - Exact parameter matching (1.3ms)
Mock.Setup(() => MyClass.Method("specific-value"))
    .Returns(42);
```

**When to use each**:
- Use exact matching for known test values
- Use `IsAny<T>()` only when parameter values vary significantly
- Consider conditional matching `It.Is<T>(predicate)` for complex validation

### 2. Prefer Static Method Mocking

**Modern .NET Performance Comparison**:
- Static method setup: 600-730 μs
- Instance method setup: 1,050 μs (40% slower)

```csharp
// ✅ Preferred - Static method mocking
Mock.Setup(() => DateTime.Now)
    .Returns(new DateTime(2024, 1, 1));

// ❌ Slower - Instance method mocking
Mock.Setup(() => myInstance.GetCurrentTime())
    .Returns(new DateTime(2024, 1, 1));
```

### 3. Framework Selection Strategy

**Choose Modern .NET (.NET 8+) for**:
- New projects requiring optimal memory usage
- High-frequency test execution
- CI/CD pipelines with memory constraints

**Stick with .NET Framework for**:
- Legacy codebases where parameter matching is heavily used
- Projects with existing .NET Framework dependencies
- When `IsAny<T>()` operations dominate your test scenarios

### 4. Efficient Mock Management

#### Group Mock Setup
```csharp
// ✅ Efficient - Setup multiple mocks together
using var mock1 = Mock.Setup(() => DateTime.Now).Returns(fixedDate);
using var mock2 = Mock.Setup(() => Environment.MachineName).Returns("TEST");
using var mock3 = Mock.Setup(context => File.Exists(context.It.IsAny<string>())).Returns(true);

// Execute all operations
```

#### Avoid Excessive Mock Creation
```csharp
// ❌ Inefficient - Creating mocks in loops
for (int i = 0; i < 100; i++)
{
    using var mock = Mock.Setup(() => Method()).Returns(i);
    Method(); // Each iteration pays setup cost
}

// ✅ Efficient - Single mock with callback
var results = new Queue<int>(Enumerable.Range(0, 100));
using var mock = Mock.Setup(() => Method())
    .Returns(() => results.Dequeue());

for (int i = 0; i < 100; i++)
{
    Method(); // Only execution cost
}
```

### 5. Callback Optimization

Simple callbacks perform better than complex ones:

```csharp
// ✅ Fast - Simple callback (950 μs)
Mock.Setup(context => Method(context.It.IsAny<int>()))
    .Callback<int>(_ => { /* minimal work */ });

// ❌ Slower - Complex callback (2.6ms)
Mock.Setup(context => Method(context.It.IsAny<int>()))
    .Callback<int>(x => {
        // Complex computation
        var result = ExpensiveOperation(x);
        ProcessResult(result);
    });
```

## Memory Management

SMock automatically manages memory for mock hooks and IL modifications. However, you can optimize memory usage:

### Disposal Patterns

```csharp
// ✅ Automatic disposal with using
using var mock = Mock.Setup(() => Method()).Returns(42);
// Hook automatically removed at end of scope

// ❌ Manual disposal required
var mock = Mock.Setup(() => Method()).Returns(42);
// ... test code ...
mock.Dispose(); // Must explicitly dispose
```

### Memory Pressure Monitoring

For memory-intensive testing scenarios:

```csharp
// Monitor memory usage during extensive mocking
var initialMemory = GC.GetTotalMemory(false);

// ... extensive mock operations ...

var finalMemory = GC.GetTotalMemory(true); // Force GC
var memoryUsed = finalMemory - initialMemory;

Assert.That(memoryUsed, Is.LessThan(expectedThreshold));
```

## Performance Monitoring

### Benchmark Integration

Add performance assertions to your test suite:

```csharp
[Test]
public void MockSetup_ShouldCompleteWithinTimeLimit()
{
    var stopwatch = Stopwatch.StartNew();

    using var mock = Mock.Setup(() => ExpensiveMethod())
        .Returns(42);

    stopwatch.Stop();

    // Assert reasonable setup time (adjust based on your requirements)
    Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(10));
}
```

### Profiling Integration

For continuous monitoring, integrate with APM tools:

```csharp
// Example with custom timing wrapper
public class TimedMockSetup<T> : IDisposable where T : IDisposable
{
    private readonly T _mock;
    private readonly IMetrics _metrics;
    private readonly string _operationName;
    private readonly Stopwatch _stopwatch;

    public TimedMockSetup(T mock, IMetrics metrics, string operationName)
    {
        _mock = mock;
        _metrics = metrics;
        _operationName = operationName;
        _stopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        _stopwatch.Stop();
        _metrics.RecordValue($"mock_setup_{_operationName}", _stopwatch.ElapsedMilliseconds);
        _mock.Dispose();
    }

    public static implicit operator T(TimedMockSetup<T> setup) => setup._mock;
}
```