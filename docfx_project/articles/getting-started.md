# Getting Started with SMock

Welcome to SMock, the only .NET library that makes static method mocking effortless! This comprehensive guide will walk you through everything you need to know to start using SMock in your test projects.

## Table of Contents
- [What Makes SMock Special](#what-makes-smock-special)
- [Installation & Setup](#installation--setup)
- [Understanding the Two API Styles](#understanding-the-two-api-styles)
- [Your First Mocks](#your-first-mocks)
- [Parameter Matching](#parameter-matching)
- [Async Support](#async-support)
- [Advanced Scenarios](#advanced-scenarios)
- [Best Practices](#best-practices)
- [Common Patterns](#common-patterns)
- [Troubleshooting](#troubleshooting)

## What Makes SMock Special

### The Static Method Problem

Traditional mocking frameworks like Moq, NSubstitute, and FakeItEasy can only mock virtual methods and interfaces. They cannot mock static methods, which leaves developers struggling with:

- **Legacy Code**: Older codebases with heavy static method usage
- **Third-Party Dependencies**: External libraries with static APIs (File.*, DateTime.Now, etc.)
- **System APIs**: .NET Framework/Core static methods
- **Testing Isolation**: Creating predictable test environments

### The SMock Solution

SMock uses **runtime IL modification** via [MonoMod](https://github.com/MonoMod/MonoMod) to intercept method calls at the CLR level:

```csharp
// Traditional approach - can't mock this!
var content = File.ReadAllText("config.json"); // Real file system call

// SMock approach - full control!
using var mock = Mock.Setup(() => File.ReadAllText("config.json"))
    .Returns("{\"test\": \"data\"}");

var content = File.ReadAllText("config.json"); // Returns mocked data!
```

## Installation & Setup

### NuGet Package Installation

Install SMock via your preferred method:

```powershell
# Package Manager Console
Install-Package SMock

# .NET CLI
dotnet add package SMock

# PackageReference
<PackageReference Include="SMock" Version="*" />
```

### Framework Support

SMock supports a wide range of .NET implementations:

| Target Framework | Support | Notes |
|------------------|---------|-------|
| .NET 5.0+ | ✅ Full | Recommended |
| .NET Core 2.0+ | ✅ Full | Excellent performance |
| .NET Framework 4.62-4.81 | ✅ Full | Legacy support |
| .NET Standard 2.0+ | ✅ Full | Library compatibility |

### First Test Setup

No special configuration required! SMock works with any test framework:

```csharp
using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock;

[TestFixture]
public class MyFirstTests
{
    [Test]
    public void MyFirstMockTest()
    {
        // SMock is ready to use immediately!
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var testDate = DateTime.Now;
        ClassicAssert.AreEqual(new DateTime(2024, 1, 1), testDate);
    }
}
```

## Understanding the Two API Styles

SMock provides **two distinct API styles** to match different testing preferences and scenarios.

### Sequential API - Disposable & Clean

**Best for**: Straightforward mocking with automatic cleanup

**Characteristics**:
- Uses `using` statements for automatic cleanup
- Returns `IDisposable` mock objects
- Clean, scoped mocking
- Perfect for most testing scenarios

```csharp
[Test]
public void Sequential_API_Example()
{
    // Each mock is disposable - use context parameter for parameter matching
    using var existsMock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
        .Returns(true);

    using var readMock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
        .Returns("file content");

    // Test the mocked file operations directly
    var exists = File.Exists("test.txt");
    var content = File.ReadAllText("test.txt");

    ClassicAssert.IsTrue(exists);
    ClassicAssert.AreEqual("file content", content);
} // Mocks automatically cleaned up
```

### Hierarchical API - Validation & Control

**Best for**: Complex scenarios requiring inline validation

**Characteristics**:
- Includes validation actions that run during mock execution
- No `using` statements needed
- Perfect for complex assertion scenarios
- Great for behavior verification

```csharp
[Test]
public void Hierarchical_API_Example()
{
    const string expectedPath = "important.txt";
    const string mockContent = "validated content";

    Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()), () =>
    {
        // This validation runs DURING the mock call
        var content = File.ReadAllText(expectedPath);
        ClassicAssert.IsNotNull(content);
        ClassicAssert.AreEqual(mockContent, content);

        // You can even verify the mock was called with correct parameters
    }).Returns(mockContent);

    // Test your code - validation happens automatically
    var actualContent = File.ReadAllText("important.txt");
    ClassicAssert.AreEqual(mockContent, actualContent);
}
```

### When to Use Which Style?

| Scenario | Recommended Style | Reason |
|----------|-------------------|---------|
| Simple return value mocking | Sequential | Cleaner syntax, automatic cleanup |
| Parameter verification | Hierarchical | Built-in validation actions |
| Multiple related mocks | Sequential | Better with `using` statements |
| Complex behavior testing | Hierarchical | Inline validation capabilities |
| One-off mocks | Sequential | Simpler dispose pattern |

## Your First Mocks

### Mocking Static Methods

```csharp
[Test]
public void Mock_DateTime_Now()
{
    var fixedDate = new DateTime(2024, 12, 25, 10, 30, 0);

    using var mock = Mock.Setup(() => DateTime.Now)
        .Returns(fixedDate);

    // Your code that uses DateTime.Now
    var currentDate = DateTime.Now;

    ClassicAssert.AreEqual(fixedDate, currentDate);
    ClassicAssert.AreEqual(2024, currentDate.Year);
    ClassicAssert.AreEqual(12, currentDate.Month);
    ClassicAssert.AreEqual(25, currentDate.Day);
    ClassicAssert.AreEqual(10, currentDate.Hour);
    ClassicAssert.AreEqual(30, currentDate.Minute);
}
```

### Mocking Static Methods with Parameters

```csharp
[Test]
public void Mock_File_Operations()
{
    using var existsMock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
        .Returns(true);

    using var readMock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
        .Returns("{\"database\": \"localhost\", \"port\": 5432}");

    // Test file operations
    var exists = File.Exists("config.json");
    var content = File.ReadAllText("config.json");

    ClassicAssert.IsTrue(exists);
    ClassicAssert.AreEqual("{\"database\": \"localhost\", \"port\": 5432}", content);
    ClassicAssert.IsTrue(content.Contains("localhost"));
    ClassicAssert.IsTrue(content.Contains("5432"));
}
```

### Mocking Instance Methods

Yes! SMock can also mock instance methods:

```csharp
[Test]
public void Mock_Instance_Method()
{
    var testUser = new User { Name = "Test User" };

    using var mock = Mock.Setup(() => testUser.GetDisplayName())
        .Returns("Mocked Display Name");

    var result = testUser.GetDisplayName();
    ClassicAssert.AreEqual("Mocked Display Name", result);
}
```

### Mocking Properties

```csharp
[Test]
public void Mock_Static_Property()
{
    using var mock = Mock.Setup(() => Environment.MachineName)
        .Returns("TEST-MACHINE");

    var machineName = Environment.MachineName;
    ClassicAssert.AreEqual("TEST-MACHINE", machineName);
}
```

## Parameter Matching

SMock provides powerful parameter matching through the `It` class:

### Basic Parameter Matching

```csharp
[Test]
public void Parameter_Matching_Examples()
{
    // Match any string parameter
    using var anyStringMock = Mock.Setup(context => Path.GetFileName(context.It.IsAny<string>()))
        .Returns("mocked-file.txt");

    // Test with different paths
    var result1 = Path.GetFileName(@"C:\temp\test.txt");
    var result2 = Path.GetFileName(@"D:\documents\report.docx");

    ClassicAssert.AreEqual("mocked-file.txt", result1);
    ClassicAssert.AreEqual("mocked-file.txt", result2);
}
```

### Advanced Parameter Matching

```csharp
[Test]
public void Advanced_Parameter_Matching()
{
    // Note: Conditional parameter matching with It.Is has current limitations
    // This example shows the expected syntax once fully implemented
    using var mock = Mock.Setup(context =>
        Convert.ToInt32(context.It.IsAny<string>()))
        .Returns(42);

    var result = Convert.ToInt32("123");
    ClassicAssert.AreEqual(42, result);
}
```

### Parameter Matching with Hierarchical API

```csharp
[Test]
public void Hierarchical_Parameter_Validation()
{
    Mock.Setup(context => Path.Combine(context.It.IsAny<string>(), context.It.IsAny<string>()), () =>
    {
        // Validate the actual parameters that were passed
        var result = Path.Combine("test", "path");
        ClassicAssert.IsNotNull(result);
        ClassicAssert.IsTrue(result.Contains("test"));
        ClassicAssert.IsTrue(result.Contains("path"));
    }).Returns(@"test\path");

    var combinedPath = Path.Combine("test", "path");
    ClassicAssert.AreEqual(@"test\path", combinedPath);
}
```

## Async Support

SMock provides full support for async/await patterns:

### Mocking Async Methods

```csharp
[Test]
public async Task Mock_Async_Methods()
{
    // Mock async Task.FromResult
    using var mock = Mock.Setup(() => Task.FromResult(42))
        .Returns(Task.FromResult(100));

    var result = await Task.FromResult(42);
    ClassicAssert.AreEqual(100, result);
}
```

### Mocking with Delays

```csharp
[Test]
public async Task Mock_Async_With_Delay()
{
    // Mock Task.Delay to complete immediately
    using var delayMock = Mock.Setup(context => Task.Delay(context.It.IsAny<int>()))
        .Returns(Task.CompletedTask);

    // Simulate an async operation that would normally take time
    await Task.Delay(5000); // This should complete immediately

    ClassicAssert.Pass("Async mock executed successfully");
}
```

### Exception Handling with Async

```csharp
[Test]
public async Task Mock_Async_Exceptions()
{
    // Mock Task.Delay to throw an exception for negative values
    using var mock = Mock.Setup(context => Task.Delay(context.It.Is<int>(ms => ms < 0)))
        .Throws<ArgumentOutOfRangeException>();

    // Test exception handling in async context
    try
    {
        await Task.Delay(-1);
        Assert.Fail("Expected ArgumentOutOfRangeException to be thrown");
    }
    catch (ArgumentOutOfRangeException exception)
    {
        ClassicAssert.IsNotNull(exception);
    }
}
```

## Advanced Scenarios

### Callback Execution

Execute custom logic when mocks are called:

```csharp
[Test]
public void Mock_With_Callbacks()
{
    var callCount = 0;

    using var mock = Mock.Setup(context => File.WriteAllText(context.It.IsAny<string>(), context.It.IsAny<string>()))
        .Callback<string, string>((path, content) => callCount++);

    File.WriteAllText("test.txt", "content");
    File.WriteAllText("test2.txt", "content2");

    ClassicAssert.AreEqual(2, callCount);
}
```

### Sequential Return Values

Return different values on successive calls:

```csharp
[Test]
public void Sequential_Return_Values()
{
    var callCount = 0;

    using var mock = Mock.Setup(() => DateTime.Now)
        .Returns(() =>
        {
            callCount++;
            return callCount switch
            {
                1 => new DateTime(2024, 1, 1),
                2 => new DateTime(2024, 1, 2),
                _ => new DateTime(2024, 1, 3)
            };
        });

    var date1 = DateTime.Now;
    var date2 = DateTime.Now;
    var date3 = DateTime.Now;

    ClassicAssert.AreEqual(new DateTime(2024, 1, 1), date1);
    ClassicAssert.AreEqual(new DateTime(2024, 1, 2), date2);
    ClassicAssert.AreEqual(new DateTime(2024, 1, 3), date3);
}
```

### Conditional Mocking

Different behaviors based on parameters:

```csharp
[Test]
public void Conditional_Mock_Behavior()
{
    using var mock = Mock.Setup(context => Environment.GetEnvironmentVariable(context.It.IsAny<string>()))
        .Returns<string>(varName => varName switch
        {
            "ENVIRONMENT" => "Development",
            "DEBUG_MODE" => "true",
            "LOG_LEVEL" => "Debug",
            _ => null
        });

    var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
    var debugMode = Environment.GetEnvironmentVariable("DEBUG_MODE");
    var logLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");
    var unknown = Environment.GetEnvironmentVariable("UNKNOWN");

    ClassicAssert.AreEqual("Development", environment);
    ClassicAssert.AreEqual("true", debugMode);
    ClassicAssert.AreEqual("Debug", logLevel);
    ClassicAssert.IsNull(unknown);
}
```

## Best Practices

### 1. Scope Mocks Appropriately

```csharp
[Test]
public void Good_Mock_Scoping()
{
    // ✅ Good: Scope mocks to specific test needs
    using var timeMock = Mock.Setup(() => DateTime.Now)
        .Returns(new DateTime(2024, 1, 1));

    using var fileMock = Mock.Setup(() => File.Exists("config.json"))
        .Returns(true);

    // Test logic here
}

[Test]
public void Bad_Mock_Scoping()
{
    // ❌ Bad: Don't create mocks you don't use in the test
    using var unnecessaryMock = Mock.Setup(() => Console.WriteLine(It.IsAny<string>()));

    // Test that doesn't use Console.WriteLine
}
```

### 2. Use Meaningful Return Values

```csharp
[Test]
public void Meaningful_Return_Values()
{
    // ✅ Good: Return values that make sense for the test
    using var mock = Mock.Setup(() => UserRepository.GetUserById(123))
        .Returns(new User
        {
            Id = 123,
            Name = "Test User",
            Email = "test@example.com",
            IsActive = true
        });

    // ❌ Bad: Return meaningless default values
    using var badMock = Mock.Setup(() => UserRepository.GetUserById(456))
        .Returns(new User()); // Empty object with no meaningful data
}
```

### 3. Group Related Mocks

```csharp
[Test]
public void Group_Related_Mocks()
{
    // ✅ Good: Group mocks that work together
    using var existsMock = Mock.Setup(() => File.Exists("database.config"))
        .Returns(true);
    using var readMock = Mock.Setup(() => File.ReadAllText("database.config"))
        .Returns("connection_string=test_db");
    using var writeMock = Mock.Setup(() => File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

    // Test configuration management
    var configManager = new ConfigurationManager();
    configManager.UpdateConfiguration("new_setting", "value");
}
```

### 4. Verify Mock Usage When Needed

```csharp
[Test]
public void Verify_Mock_Usage()
{
    var callCount = 0;

    using var mock = Mock.Setup(() => AuditLogger.LogAction(It.IsAny<string>()))
        .Callback<string>(action => callCount++);

    var service = new CriticalService();
    service.PerformCriticalOperation();

    // Verify the audit log was called
    Assert.AreEqual(1, callCount, "Audit logging should be called exactly once");
}
```

## Common Patterns

### Configuration Testing

```csharp
[Test]
public void Configuration_Pattern()
{
    var testConfig = new Dictionary<string, string>
    {
        ["DatabaseConnection"] = "test_connection",
        ["ApiKey"] = "test_key_12345",
        ["EnableFeatureX"] = "true"
    };

    using var mock = Mock.Setup(() =>
        ConfigurationManager.AppSettings[It.IsAny<string>()])
        .Returns<string>(key => testConfig.GetValueOrDefault(key));

    var service = new ConfigurableService();
    service.Initialize();

    Assert.IsTrue(service.IsFeatureXEnabled);
    Assert.AreEqual("test_connection", service.DatabaseConnection);
}
```

### Time-Dependent Testing

```csharp
[Test]
public void Time_Dependent_Pattern()
{
    var testDate = new DateTime(2024, 6, 15, 14, 30, 0); // Saturday afternoon

    using var mock = Mock.Setup(() => DateTime.Now)
        .Returns(testDate);

    var scheduler = new TaskScheduler();
    var nextRun = scheduler.CalculateNextBusinessDay();

    // Should be Monday since Saturday -> next business day is Monday
    Assert.AreEqual(DayOfWeek.Monday, nextRun.DayOfWeek);
    Assert.AreEqual(new DateTime(2024, 6, 17), nextRun.Date);
}
```

### External Dependency Testing

```csharp
[Test]
public void External_Dependency_Pattern()
{
    // Mock external web service
    using var webMock = Mock.Setup(() =>
        WebClient.DownloadString("https://api.weather.com/current"))
        .Returns("{\"temperature\": 22, \"condition\": \"sunny\"}");

    // Mock file system for caching
    using var fileMock = Mock.Setup(() =>
        File.WriteAllText(It.IsAny<string>(), It.IsAny<string>()));

    var weatherService = new WeatherService();
    var weather = weatherService.GetCurrentWeather();

    Assert.AreEqual(22, weather.Temperature);
    Assert.AreEqual("sunny", weather.Condition);
}
```

## Troubleshooting

### Common Issues and Solutions

#### Issue: Mock Not Triggering

**Problem**: Your mock setup looks correct, but the original method is still being called.

```csharp
// ❌ This might not work as expected
Mock.Setup(() => SomeClass.Method()).Returns("mocked");
var result = SomeClass.Method(); // Still calls original!
```

**Solution**: Ensure you're calling the exact same method signature.

```csharp
// ✅ Make sure parameter types match exactly
Mock.Setup(() => SomeClass.Method(It.IsAny<string>())).Returns("mocked");
var result = SomeClass.Method("any_parameter"); // Now mocked!
```

#### Issue: Parameter Matching Not Working

**Problem**: Your parameter matcher seems too restrictive.

```csharp
// ❌ Too specific
Mock.Setup(() => Validator.Validate("exact_string")).Returns(true);
var result = Validator.Validate("different_string"); // Not mocked!
```

**Solution**: Use appropriate parameter matchers.

```csharp
// ✅ Use IsAny for flexible matching
Mock.Setup(() => Validator.Validate(It.IsAny<string>())).Returns(true);

// ✅ Or use Is with conditions
Mock.Setup(() => Validator.Validate(It.Is<string>(s => s.Length > 0))).Returns(true);
```

#### Issue: Async Mocks Not Working

**Problem**: Async methods aren't being mocked properly.

```csharp
// ❌ Wrong return type
Mock.Setup(() => Service.GetDataAsync()).Returns("data"); // Won't compile!
```

**Solution**: Return the correct Task type.

```csharp
// ✅ Return Task with proper value
Mock.Setup(() => Service.GetDataAsync()).Returns(Task.FromResult("data"));

// ✅ Or use async lambda
Mock.Setup(() => Service.GetDataAsync()).Returns(async () =>
{
    await Task.Delay(10);
    return "data";
});
```

#### Issue: Mocks Interfering Between Tests

**Problem**: Mocks from one test affecting another.

**Solution**: Always use `using` statements with Sequential API to ensure proper cleanup.

```csharp
[Test]
public void Test_With_Proper_Cleanup()
{
    using var mock = Mock.Setup(() => DateTime.Now)
        .Returns(new DateTime(2024, 1, 1));

    // Test logic here
} // Mock automatically disposed and cleaned up
```

### Getting Help

When you encounter issues:

1. **Check the Documentation**: Review this guide and the [API reference](../api/index.md)
2. **Search Issues**: Check [GitHub Issues](https://github.com/SvetlovA/static-mock/issues) for similar problems
3. **Create Minimal Repro**: Prepare a minimal code example that demonstrates the issue
4. **Ask for Help**: Create a new issue with details about your environment and problem

### Performance Considerations

- **Mock Setup Cost**: Creating mocks has a small one-time cost (~1-2ms per mock)
- **Runtime Overhead**: Method interception is very fast (<0.1ms per call)
- **Memory Usage**: Minimal impact, temporary IL modifications only
- **Cleanup**: Always dispose Sequential mocks to free resources promptly

## Next Steps

Now that you understand the basics of SMock, continue your journey with these comprehensive guides:

### 🚀 **Level Up Your Skills**
- **[Advanced Usage Patterns](advanced-patterns.md)** - Complex mock scenarios, state management, and composition patterns
- **[Testing Framework Integration](framework-integration.md)** - Deep integration with NUnit, xUnit, MSTest, and CI/CD pipelines
- **[Real-World Examples & Case Studies](real-world-examples.md)** - Enterprise scenarios, legacy modernization, and practical applications

### 🛠️ **Optimization & Troubleshooting**
- **[Performance Guide & Benchmarks](performance-guide.md)** - Optimization strategies, benchmarking, and scaling considerations
- **[Troubleshooting & FAQ](troubleshooting.md)** - Solutions to common issues, diagnostic tools, and community support
- **[Migration Guide](migration-guide.md)** - Upgrading between versions and switching from other mocking frameworks

### 📚 **Reference Materials**
- **[API Reference](../api/index.md)** - Complete API documentation with detailed method signatures
- **[GitHub Repository](https://github.com/SvetlovA/static-mock)** - Source code, issue tracking, and community discussions
- **[NuGet Package](https://www.nuget.org/packages/SMock)** - Latest releases and version history

### 💡 **Quick Navigation by Use Case**
- **New to mocking?** Start with [Advanced Usage Patterns](advanced-patterns.md) for more examples
- **Enterprise developer?** Check out [Real-World Examples](real-world-examples.md) for case studies
- **Performance concerns?** Visit [Performance Guide](performance-guide.md) for optimization strategies
- **Having issues?** Go to [Troubleshooting & FAQ](troubleshooting.md) for solutions
- **Migrating from another framework?** See [Migration Guide](migration-guide.md) for guidance

Happy testing with SMock! 🚀

## Working Examples in the Test Suite

All examples in this documentation are based on actual working test cases. You can find complete, debugged examples in the SMock test suite:

### 📁 **Basic Examples**
- **[Basic Sequential Examples](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/GettingStarted/BasicSequentialExamples.cs)** - `src/StaticMock.Tests/Tests/Examples/GettingStarted/BasicSequentialExamples.cs`
- **[Basic Hierarchical Examples](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/GettingStarted/BasicHierarchicalExamples.cs)** - `src/StaticMock.Tests/Tests/Examples/GettingStarted/BasicHierarchicalExamples.cs`
- **[Async Examples](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/GettingStarted/AsyncExamples.cs)** - `src/StaticMock.Tests/Tests/Examples/GettingStarted/AsyncExamples.cs`

### 📁 **Advanced Examples**
- **[Complex Mock Scenarios](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/AdvancedPatterns/ComplexMockScenarios.cs)** - `src/StaticMock.Tests/Tests/Examples/AdvancedPatterns/ComplexMockScenarios.cs`
- **[Performance Tests](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/PerformanceGuide/PerformanceTests.cs)** - `src/StaticMock.Tests/Tests/Examples/PerformanceGuide/PerformanceTests.cs`
- **[Real-World Enterprise Scenarios](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/RealWorldExamples/EnterpriseScenarios.cs)** - `src/StaticMock.Tests/Tests/Examples/RealWorldExamples/EnterpriseScenarios.cs`

### 📁 **Migration & Integration**
- **[Migration Examples](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/MigrationGuide/MigrationExamples.cs)** - `src/StaticMock.Tests/Tests/Examples/MigrationGuide/MigrationExamples.cs`
- **[Framework Integration Tests](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/FrameworkIntegration/NUnitIntegrationTests.cs)** - `src/StaticMock.Tests/Tests/Examples/FrameworkIntegration/NUnitIntegrationTests.cs`

### 💡 **Why Reference the Test Examples?**

The test examples provide:
- **Verified working code** - All examples compile and pass tests
- **Complete context** - Full test methods with setup and teardown
- **Current limitations** - Some examples include `[Ignore]` attributes with notes about current implementation constraints
- **Best practices** - Real-world usage patterns and error handling
- **Latest syntax** - Up-to-date API usage that matches the current implementation

### 🔧 **Running the Examples Locally**

To run these examples on your machine:

```bash
# Clone the repository
git clone https://github.com/SvetlovA/static-mock.git
cd static-mock/src

# Run the specific example tests
dotnet test --filter "FullyQualifiedName~Examples"

# Or run a specific example class
dotnet test --filter "ClassName=BasicSequentialExamples"
```