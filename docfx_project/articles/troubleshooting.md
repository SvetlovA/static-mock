# Troubleshooting Guide & FAQ

This comprehensive guide covers common issues, solutions, and frequently asked questions about SMock.

## Table of Contents
- [Quick Diagnostics](#quick-diagnostics)
- [Common Issues](#common-issues)
- [Mock Setup Problems](#mock-setup-problems)
- [Runtime Issues](#runtime-issues)
- [Performance Problems](#performance-problems)
- [Integration Issues](#integration-issues)
- [Frequently Asked Questions](#frequently-asked-questions)
- [Getting Help](#getting-help)

## Quick Diagnostics

### SMock Health Check

Run this quick test to verify SMock is working correctly:

```csharp
[Test]
public void SMock_Health_Check()
{
    try
    {
        // Test basic static method mocking
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var result = DateTime.Now;
        Assert.AreEqual(new DateTime(2024, 1, 1), result);

        Console.WriteLine("✅ SMock is working correctly!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ SMock issue detected: {ex.Message}");
        throw;
    }
}
```

### Environment Verification

```csharp
[Test]
public void Verify_Environment()
{
    Console.WriteLine($"Runtime: {RuntimeInformation.FrameworkDescription}");
    Console.WriteLine($"OS: {RuntimeInformation.OSDescription}");
    Console.WriteLine($"Architecture: {RuntimeInformation.ProcessArchitecture}");

    var smockAssembly = Assembly.GetAssembly(typeof(Mock));
    Console.WriteLine($"SMock Version: {smockAssembly.GetName().Version}");

    // Check for MonoMod assemblies
    try
    {
        var monoModAssembly = Assembly.LoadFrom("MonoMod.Core.dll");
        Console.WriteLine($"MonoMod.Core: {monoModAssembly.GetName().Version}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ MonoMod.Core not found: {ex.Message}");
    }
}
```

## Common Issues

### Issue 1: Mock Not Triggering

**Symptoms**: Mock setup appears correct, but original method is still called.

**Diagnostic Steps**:

```csharp
[Test]
public void Debug_Mock_Not_Triggering()
{
    // Step 1: Verify exact method signature
    using var mock = Mock.Setup(() => File.ReadAllText("test.txt"))
        .Returns("mocked content");

    // Step 2: Test with exact parameters
    var result1 = File.ReadAllText("test.txt");
    Console.WriteLine($"Exact match result: {result1}");

    // Step 3: Test with different parameters (should call original)
    try
    {
        var result2 = File.ReadAllText("different.txt");
        Console.WriteLine($"Different parameter result: {result2}");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("Different parameter called original method (expected)");
    }
}
```

**Common Causes & Solutions**:

1. **Parameter Mismatch**:
   ```csharp
   // ❌ Problem: Too specific
   Mock.Setup(() => MyClass.Method("exact_value")).Returns("result");

   // ✅ Solution: Use parameter matching
   Mock.Setup(() => MyClass.Method(It.IsAny<string>())).Returns("result");
   ```

2. **Method Overload Confusion**:
   ```csharp
   // ❌ Problem: Wrong overload
   Mock.Setup(() => Convert.ToString(It.IsAny<object>())).Returns("mocked");

   // ✅ Solution: Specify exact overload
   Mock.Setup(() => Convert.ToString(It.IsAny<int>())).Returns("mocked");
   ```

3. **Generic Method Issues**:
   ```csharp
   // ❌ Problem: Generic type not resolved
   Mock.Setup(() => JsonSerializer.Deserialize<object>(It.IsAny<string>()))
       .Returns(new { test = "value" });

   // ✅ Solution: Specify concrete type
   Mock.Setup(() => JsonSerializer.Deserialize<MyClass>(It.IsAny<string>()))
       .Returns(new MyClass { Test = "value" });
   ```

### Issue 2: Assembly Loading Failures

**Symptoms**: `FileNotFoundException`, `BadImageFormatException`, or similar assembly errors.

**Diagnostic Code**:
```csharp
[Test]
public void Debug_Assembly_Loading()
{
    var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name.Contains("MonoMod") ||
                    a.GetName().Name.Contains("SMock"))
        .ToList();

    foreach (var assembly in loadedAssemblies)
    {
        Console.WriteLine($"Loaded: {assembly.GetName().Name} v{assembly.GetName().Version}");
        Console.WriteLine($"Location: {assembly.Location}");
    }

    if (!loadedAssemblies.Any())
    {
        Console.WriteLine("⚠️ No SMock/MonoMod assemblies found!");
    }
}
```

**Solutions**:

1. **Clean and Restore**:
   ```bash
   dotnet clean
   rm -rf bin obj  # or del bin obj /s /q on Windows
   dotnet restore
   dotnet build
   ```

2. **Check Package References**:
   ```xml
   <!-- Ensure these are in your .csproj -->
   <PackageReference Include="SMock" Version="[latest-version]" />

   <!-- If issues persist, add explicit MonoMod references -->
   <PackageReference Include="MonoMod.Core" Version="[compatible-version]" />
   ```

3. **Runtime Configuration**:
   ```xml
   <!-- Add to your test project's app.config or test.runsettings -->
   <runtime>
     <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
       <probing privatePath="bin;lib" />
     </assemblyBinding>
   </runtime>
   ```

### Issue 3: Performance Degradation

**Symptoms**: Tests run significantly slower after adding SMock.

**Performance Profiling**:
```csharp
[Test]
public void Profile_Mock_Performance()
{
    var stopwatch = Stopwatch.StartNew();

    // Measure mock setup time
    var setupStart = stopwatch.ElapsedMilliseconds;
    using var mock = Mock.Setup(() => DateTime.Now).Returns(new DateTime(2024, 1, 1));
    var setupTime = stopwatch.ElapsedMilliseconds - setupStart;

    // Measure mock execution time
    var executionStart = stopwatch.ElapsedMilliseconds;
    for (int i = 0; i < 1000; i++)
    {
        var _ = DateTime.Now;
    }
    var executionTime = stopwatch.ElapsedMilliseconds - executionStart;

    Console.WriteLine($"Setup time: {setupTime}ms");
    Console.WriteLine($"Execution time (1000 calls): {executionTime}ms");
    Console.WriteLine($"Per-call overhead: {(double)executionTime / 1000:F3}ms");

    // Acceptable thresholds
    Assert.Less(setupTime, 10, "Setup should be under 10ms");
    Assert.Less((double)executionTime / 1000, 0.1, "Per-call overhead should be under 0.1ms");
}
```

**Optimization Strategies**:

1. **Reduce Mock Scope**:
   ```csharp
   // ❌ Avoid: Creating unnecessary mocks
   [Test]
   public void Wasteful_Mocking()
   {
       using var mock1 = Mock.Setup(() => Method1()).Returns("value1");
       using var mock2 = Mock.Setup(() => Method2()).Returns("value2"); // Not used!
       using var mock3 = Mock.Setup(() => Method3()).Returns("value3");

       // Only Method1 is actually called in test
       Assert.AreEqual("value1", Method1());
   }

   // ✅ Better: Only mock what you need
   [Test]
   public void Efficient_Mocking()
   {
       using var mock = Mock.Setup(() => Method1()).Returns("value1");
       Assert.AreEqual("value1", Method1());
   }
   ```

2. **Use Lazy Initialization**:
   ```csharp
   [Test]
   public void Lazy_Mock_Initialization()
   {
       Lazy<IDisposable> expensiveMock = new(() =>
           Mock.Setup(() => ExpensiveExternalService.Call())
               .Returns("cached_result"));

       var service = new TestService();

       // Mock only created if needed
       if (service.NeedsExternalCall())
       {
           using var mock = expensiveMock.Value;
           service.DoWork();
       }
   }
   ```

## Mock Setup Problems

### Parameter Matching Issues

**Problem**: Parameter matchers not working as expected.

```csharp
[Test]
public void Debug_Parameter_Matching()
{
    // Test different parameter matching strategies
    var callLog = new List<string>();

    using var mock = Mock.Setup(() => TestClass.ProcessData(It.IsAny<string>()))
        .Callback<string>(data => callLog.Add($"Called with: {data}"))
        .Returns("mocked");

    // These should all trigger the mock
    TestClass.ProcessData("test1");
    TestClass.ProcessData("test2");
    TestClass.ProcessData(null);

    Console.WriteLine("Calls captured:");
    callLog.ForEach(Console.WriteLine);

    Assert.AreEqual(3, callLog.Count, "All calls should be captured");
}
```

**Advanced Parameter Matching**:
```csharp
[Test]
public void Advanced_Parameter_Matching()
{
    // Complex object matching
    using var mock = Mock.Setup(() =>
        DataProcessor.Process(It.Is<ProcessRequest>(req =>
            req.Priority > 5 &&
            req.Type == "Important" &&
            req.Data.Contains("test"))))
        .Returns(new ProcessResult { Success = true });

    var request = new ProcessRequest
    {
        Priority = 10,
        Type = "Important",
        Data = "test_data_here"
    };

    var result = DataProcessor.Process(request);
    Assert.IsTrue(result.Success);
}
```

### Async Method Mocking Issues

**Problem**: Async methods not mocking correctly.

```csharp
[Test]
public async Task Debug_Async_Mocking()
{
    // ❌ Common mistake: Wrong return type
    // Mock.Setup(() => AsyncService.GetDataAsync()).Returns("data"); // Won't compile

    // ✅ Correct approaches:

    // Option 1: Task.FromResult
    using var mock1 = Mock.Setup(() => AsyncService.GetDataAsync())
        .Returns(Task.FromResult("mocked_data"));

    var result1 = await AsyncService.GetDataAsync();
    Assert.AreEqual("mocked_data", result1);

    // Option 2: Async lambda
    using var mock2 = Mock.Setup(() => AsyncService.ProcessAsync(It.IsAny<string>()))
        .Returns(async (string input) =>
        {
            await Task.Delay(1); // Simulate async work
            return $"processed_{input}";
        });

    var result2 = await AsyncService.ProcessAsync("test");
    Assert.AreEqual("processed_test", result2);
}
```

## Runtime Issues

### Hook Conflicts

**Problem**: Multiple mocks interfering with each other.

```csharp
[Test]
public void Debug_Hook_Conflicts()
{
    var calls = new List<string>();

    // Create multiple mocks for the same method
    using var mock1 = Mock.Setup(() => Logger.Log(It.IsAny<string>()))
        .Callback<string>(msg => calls.Add($"Mock1: {msg}"))
        .Returns();

    // This might conflict with mock1
    using var mock2 = Mock.Setup(() => Logger.Log(It.IsAny<string>()))
        .Callback<string>(msg => calls.Add($"Mock2: {msg}"))
        .Returns();

    Logger.Log("test_message");

    Console.WriteLine("Captured calls:");
    calls.ForEach(Console.WriteLine);

    // Only the last mock should be active
    Assert.AreEqual(1, calls.Count);
    Assert.IsTrue(calls[0].Contains("Mock2"));
}
```

**Solution**: Use single mock with conditional logic:
```csharp
[Test]
public void Resolved_Conditional_Mocking()
{
    var calls = new List<string>();

    using var mock = Mock.Setup(() => Logger.Log(It.IsAny<string>()))
        .Callback<string>(msg =>
        {
            if (msg.StartsWith("error"))
                calls.Add($"Error logged: {msg}");
            else
                calls.Add($"Info logged: {msg}");
        })
        .Returns();

    Logger.Log("error: Something went wrong");
    Logger.Log("info: Everything is fine");

    Assert.AreEqual(2, calls.Count);
    Assert.IsTrue(calls[0].Contains("Error logged"));
    Assert.IsTrue(calls[1].Contains("Info logged"));
}
```

### Memory Leaks

**Problem**: Memory usage grows over time during test execution.

**Diagnostic Tool**:
```csharp
[Test]
public void Monitor_Memory_Usage()
{
    var initialMemory = GC.GetTotalMemory(true);

    for (int i = 0; i < 100; i++)
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var _ = DateTime.Now;
    }

    var finalMemory = GC.GetTotalMemory(true);
    var memoryIncrease = finalMemory - initialMemory;

    Console.WriteLine($"Initial memory: {initialMemory:N0} bytes");
    Console.WriteLine($"Final memory: {finalMemory:N0} bytes");
    Console.WriteLine($"Memory increase: {memoryIncrease:N0} bytes");

    // Memory increase should be minimal
    Assert.Less(memoryIncrease, 1_000_000, "Memory increase should be under 1MB");
}
```

**Prevention**: Always dispose mocks properly:
```csharp
[Test]
public void Proper_Mock_Disposal()
{
    // ✅ Good: Using statement ensures disposal
    using var mock = Mock.Setup(() => File.Exists(It.IsAny<string>()))
        .Returns(true);

    // Test logic here

    // ✅ Good: Explicit disposal if using statement not possible
    var mock2 = Mock.Setup(() => File.ReadAllText(It.IsAny<string>()))
        .Returns("content");
    try
    {
        // Test logic
    }
    finally
    {
        mock2?.Dispose();
    }
}
```

## Performance Problems

### Slow Test Execution

**Benchmarking Framework**:
```csharp
public class SMockPerformanceBenchmark
{
    [Benchmark]
    public void Mock_Setup_Performance()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));
    }

    [Benchmark]
    public void Mock_Execution_Performance()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        for (int i = 0; i < 1000; i++)
        {
            var _ = DateTime.Now;
        }
    }

    [Benchmark]
    public void Complex_Mock_Performance()
    {
        using var mock = Mock.Setup(() =>
            DataProcessor.Transform(It.Is<DataModel>(d => d.IsValid)))
            .Callback<DataModel>(d => Console.WriteLine($"Processing {d.Id}"))
            .Returns(new TransformResult { Success = true });

        var data = new DataModel { Id = 1, IsValid = true };
        var _ = DataProcessor.Transform(data);
    }
}
```

## Integration Issues

### Test Framework Compatibility

**NUnit Integration Issues**:
```csharp
[TestFixture]
public class NUnitIntegrationTests
{
    [Test]
    public void SMock_Works_With_NUnit()
    {
        using var mock = Mock.Setup(() => Environment.MachineName)
            .Returns("TEST_MACHINE");

        Assert.AreEqual("TEST_MACHINE", Environment.MachineName);
    }
}
```

**xUnit Integration**:
```csharp
public class XUnitIntegrationTests : IDisposable
{
    private readonly List<IDisposable> _mocks = new List<IDisposable>();

    [Fact]
    public void SMock_Works_With_xUnit()
    {
        var mock = Mock.Setup(() => DateTime.UtcNow)
            .Returns(new DateTime(2024, 1, 1));
        _mocks.Add(mock);

        Assert.Equal(new DateTime(2024, 1, 1), DateTime.UtcNow);
    }

    public void Dispose()
    {
        _mocks.ForEach(m => m?.Dispose());
        _mocks.Clear();
    }
}
```

### CI/CD Pipeline Issues

**Problem**: Tests pass locally but fail in CI/CD.

**Diagnostic Script** (for CI):
```csharp
[Test]
public void CI_Environment_Check()
{
    Console.WriteLine("=== CI/CD Environment Diagnostics ===");
    Console.WriteLine($"OS: {Environment.OSVersion}");
    Console.WriteLine($"Runtime: {RuntimeInformation.FrameworkDescription}");
    Console.WriteLine($"Architecture: {RuntimeInformation.ProcessArchitecture}");
    Console.WriteLine($"Is64BitProcess: {Environment.Is64BitProcess}");
    Console.WriteLine($"WorkingDirectory: {Directory.GetCurrentDirectory()}");

    // Check for restricted environments
    try
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));
        Console.WriteLine("✅ Basic mocking works");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Basic mocking failed: {ex.Message}");
        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        throw;
    }
}
```

## Frequently Asked Questions

### Q: Can SMock mock sealed classes?
**A:** SMock mocks methods, not classes. It can mock static methods on sealed classes, but cannot mock instance methods on sealed classes that aren't virtual.

```csharp
// ✅ This works - static method on sealed class
using var mock = Mock.Setup(() => File.ReadAllText(It.IsAny<string>()))
    .Returns("mocked content");

// ❌ This won't work - instance method on sealed class
// var sealedInstance = new SealedClass();
// Mock.Setup(() => sealedInstance.NonVirtualMethod()).Returns("value");
```

### Q: How does SMock compare performance-wise to other mocking frameworks?
**A:** SMock has minimal runtime overhead for method interception (<0.1ms per call), but higher setup cost (~1-2ms) due to IL modification. For most testing scenarios, this is negligible.

### Q: Can I use SMock in production code?
**A:** No, SMock is designed exclusively for testing. It modifies runtime behavior and should never be used in production builds.

### Q: Does SMock work with .NET Native/AOT?
**A:** SMock requires runtime IL modification capabilities that may not be available in AOT-compiled applications. It's designed for traditional .NET runtimes.

### Q: Can I mock methods from third-party libraries?
**A:** Yes, SMock can mock any static method from any assembly, including third-party libraries.

```csharp
// Works with third-party libraries
using var mock = Mock.Setup(() => JsonConvert.SerializeObject(It.IsAny<object>()))
    .Returns("{\"mocked\": true}");
```

### Q: How do I verify that a mocked method was called?
**A:** Use callbacks to track method calls:

```csharp
[Test]
public void Verify_Method_Called()
{
    var wasCalled = false;

    using var mock = Mock.Setup(() => Logger.Log(It.IsAny<string>()))
        .Callback<string>(msg => wasCalled = true);

    // Your code that should call Logger.Log
    MyService.DoSomething();

    Assert.IsTrue(wasCalled, "Logger.Log should have been called");
}
```

### Q: Can I mock generic methods?
**A:** Yes, but you need to specify the generic type parameters:

```csharp
// ✅ Specify the generic type
using var mock = Mock.Setup(() => JsonSerializer.Deserialize<MyClass>(It.IsAny<string>()))
    .Returns(new MyClass());

// ❌ Don't use open generic types
// Mock.Setup(() => JsonSerializer.Deserialize<T>(It.IsAny<string>()))
```

## Getting Help

### Self-Diagnosis Checklist

Before seeking help, run through this checklist:

- [ ] SMock package is properly installed and up-to-date
- [ ] Mock setup syntax is correct (parameter matching, return types)
- [ ] Using statements or proper disposal for Sequential API
- [ ] No conflicting mocks for the same method
- [ ] Test framework compatibility verified
- [ ] Environment supports runtime IL modification

### Reporting Issues

When reporting issues, include:

1. **SMock Version**: `dotnet list package SMock`
2. **Environment**: .NET version, OS, architecture
3. **Minimal Reproduction**: Simplest code that demonstrates the issue
4. **Expected vs Actual**: What you expected vs what happened
5. **Error Messages**: Full exception messages and stack traces
6. **Test Framework**: NUnit, xUnit, MSTest version

### Community Resources

- **GitHub Issues**: [Report bugs and feature requests](https://github.com/SvetlovA/static-mock/issues)
- **GitHub Discussions**: [Ask questions and share solutions](https://github.com/SvetlovA/static-mock/discussions)
- **Documentation**: [Complete API reference](../api/index.md)
- **Examples**: [Real-world usage patterns](real-world-examples.md)

### Professional Support

For enterprise users requiring professional support:
- Priority issue resolution
- Custom integration assistance
- Performance optimization consulting
- Training and onboarding

Contact: [GitHub Sponsors](https://github.com/sponsors/SvetlovA) for enterprise support options.

This troubleshooting guide should help you resolve most SMock-related issues. If you encounter problems not covered here, please contribute to the community by sharing your solution!

## See Also

### Quick Navigation by Issue Type
- **Getting Started Issues?** → [Getting Started Guide](getting-started.md) - Review basics and common patterns
- **Advanced Pattern Problems?** → [Advanced Usage Patterns](advanced-patterns.md) - Complex scenarios and solutions
- **Performance Issues?** → [Performance Guide](performance-guide.md) - Optimization and benchmarking strategies
- **Framework Integration Problems?** → [Testing Framework Integration](framework-integration.md) - NUnit, xUnit, MSTest specific guidance
- **Migration Challenges?** → [Migration Guide](migration-guide.md) - Version upgrades and framework switching

### Example-Driven Solutions
- **[Real-World Examples](real-world-examples.md)** - See working solutions in practical scenarios
- **[API Reference](../api/index.md)** - Detailed method signatures and usage examples

### Community Support
- **[GitHub Issues](https://github.com/SvetlovA/static-mock/issues)** - Search existing issues or report new bugs
- **[GitHub Discussions](https://github.com/SvetlovA/static-mock/discussions)** - Ask questions and get community help
- **[Stack Overflow](https://stackoverflow.com/questions/tagged/smock)** - General programming questions with SMock tag

### Preventive Resources
- **[Best Practices](getting-started.md#best-practices)** - Follow established patterns to avoid common issues
- **[Performance Monitoring](performance-guide.md#performance-monitoring)** - Set up monitoring to catch issues early