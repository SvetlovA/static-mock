using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.PerformanceGuide;

[TestFixture]
public class PerformanceTests
{
    [Test]
    public void Measure_Mock_Setup_Performance()
    {
        var stopwatch = Stopwatch.StartNew();

        // Measure simple mock setup time
        var simpleSetupStart = stopwatch.ElapsedTicks;
        using var simpleMock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));
        var simpleSetupTime = stopwatch.ElapsedTicks - simpleSetupStart;

        // Measure parameter matching setup time
        var parameterSetupStart = stopwatch.ElapsedTicks;
        using var parameterMock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
            .Returns("content");
        var parameterSetupTime = stopwatch.ElapsedTicks - parameterSetupStart;

        stopwatch.Stop();

        var simpleSetupMs = simpleSetupTime * 1000.0 / Stopwatch.Frequency;
        var parameterSetupMs = parameterSetupTime * 1000.0 / Stopwatch.Frequency;

        // Performance assertions - setup should be reasonably fast
        ClassicAssert.Less(simpleSetupMs, 10.0, "Simple mock setup should take less than 10ms");
        ClassicAssert.Less(parameterSetupMs, 20.0, "Parameter mock setup should take less than 20ms");

        // Log performance for debugging
        TestContext.WriteLine($"Simple setup: {simpleSetupMs:F2}ms");
        TestContext.WriteLine($"Parameter setup: {parameterSetupMs:F2}ms");
    }

    [Test]
    public void Measure_Runtime_Overhead()
    {
        const int iterations = 1000;

        // Baseline: Original method performance (using unmocked method)
        var baselineStart = Stopwatch.GetTimestamp();
        for (var i = 0; i < iterations; i++)
        {
            _ = DateTime.UtcNow;
        }
        var baselineTime = Stopwatch.GetTimestamp() - baselineStart;

        // Mocked method performance
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var mockedStart = Stopwatch.GetTimestamp();
        for (int i = 0; i < iterations; i++)
        {
            var _ = DateTime.Now; // Mocked method
        }
        var mockedTime = Stopwatch.GetTimestamp() - mockedStart;

        var baselineMs = baselineTime * 1000.0 / Stopwatch.Frequency;
        var mockedMs = mockedTime * 1000.0 / Stopwatch.Frequency;
        var overheadMs = mockedMs - baselineMs;
        var overheadPerCall = overheadMs / iterations;

        TestContext.WriteLine($"Baseline ({iterations:N0} calls): {baselineMs:F2}ms");
        TestContext.WriteLine($"Mocked ({iterations:N0} calls): {mockedMs:F2}ms");
        TestContext.WriteLine($"Total overhead: {overheadMs:F2}ms");
        TestContext.WriteLine($"Overhead per call: {overheadPerCall:F6}ms");

        // Overhead should be minimal
        ClassicAssert.Less(overheadPerCall, 0.01, "Per-call overhead should be under 0.01ms");
    }

    [Test]
    public void Test_Multiple_Mock_Performance()
    {
        var stopwatch = Stopwatch.StartNew();

        // Create multiple mocks and measure performance
        using var mock1 = Mock.Setup(() => DateTime.Now).Returns(new DateTime(2024, 1, 1));
        using var mock2 = Mock.Setup(() => Environment.MachineName).Returns("TEST");
        using var mock3 = Mock.Setup(context => File.Exists(context.It.IsAny<string>())).Returns(true);

        var setupTime = stopwatch.ElapsedMilliseconds;

        // Execute mocked methods
        var executionStart = stopwatch.ElapsedMilliseconds;
        File.Exists("test.txt");
        var executionTime = stopwatch.ElapsedMilliseconds - executionStart;

        stopwatch.Stop();

        TestContext.WriteLine($"Multiple mock setup: {setupTime}ms");
        TestContext.WriteLine($"Multiple mock execution: {executionTime}ms");

        // Performance should be reasonable
        ClassicAssert.Less(setupTime, 100, "Multiple mock setup should take less than 100ms");
        ClassicAssert.Less(executionTime, 10, "Multiple mock execution should take less than 10ms");
    }

    [Test]
    public void Test_Memory_Efficiency()
    {
        var initialMemory = GC.GetTotalMemory(true);

        // Create and dispose multiple mocks
        for (int i = 0; i < 100; i++)
        {
            using var mock = Mock.Setup(() => DateTime.Now)
                .Returns(new DateTime(2024, 1, 1));

            var _ = DateTime.Now; // Execute mock
        }

        var finalMemory = GC.GetTotalMemory(true);
        var memoryUsed = finalMemory - initialMemory;

        TestContext.WriteLine($"Initial memory: {initialMemory:N0} bytes");
        TestContext.WriteLine($"Final memory: {finalMemory:N0} bytes");
        TestContext.WriteLine($"Memory used: {memoryUsed:N0} bytes");

        // Memory usage should be reasonable
        ClassicAssert.Less(memoryUsed, 1_000_000, "Memory usage should be under 1MB for 100 mocks");
    }

    [Test]
    public void Test_Efficient_Parameter_Matching()
    {
        const int iterations = 100;
        var stopwatch = Stopwatch.StartNew();

        // Test exact parameter matching (fastest)
        var exactStart = stopwatch.ElapsedTicks;
        using (Mock.Setup(() => Path.GetFileName("exact_value")).Returns("result"))
        {
            for (var i = 0; i < iterations; i++)
            {
                Path.GetFileName("exact_value");
            }
        }
        var exactTime = stopwatch.ElapsedTicks - exactStart;

        // Test IsAny matching
        var isAnyStart = stopwatch.ElapsedTicks;
        using (Mock.Setup(context => Path.GetFileName(context.It.IsAny<string>())).Returns("result"))
        {
            for (var i = 0; i < iterations; i++)
            {
                Path.GetFileName("any_value");
            }
        }
        var isAnyTime = stopwatch.ElapsedTicks - isAnyStart;

        stopwatch.Stop();

        var exactMs = exactTime * 1000.0 / Stopwatch.Frequency;
        var isAnyMs = isAnyTime * 1000.0 / Stopwatch.Frequency;

        TestContext.WriteLine($"Exact matching: {exactMs:F3}ms");
        TestContext.WriteLine($"IsAny matching: {isAnyMs:F3}ms");

        // Both should be reasonably fast
        ClassicAssert.Less(exactMs, 50, "Exact parameter matching should be fast");
        ClassicAssert.Less(isAnyMs, 100, "IsAny parameter matching should be reasonably fast");
    }
}