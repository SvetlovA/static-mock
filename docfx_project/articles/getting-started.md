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
        Assert.AreEqual(new DateTime(2024, 1, 1), testDate);
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
    // Each mock is disposable
    using var existsMock = Mock.Setup(() => File.Exists("test.txt"))
        .Returns(true);

    using var readMock = Mock.Setup(() => File.ReadAllText("test.txt"))
        .Returns("file content");

    // Test your code
    var processor = new FileProcessor();
    var result = processor.ProcessFile("test.txt");

    Assert.AreEqual("FILE CONTENT", result);
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
    var expectedPath = "important.txt";

    Mock.Setup(() => File.ReadAllText(It.IsAny<string>()), () =>
    {
        // This validation runs DURING the mock call
        var content = File.ReadAllText(expectedPath);
        Assert.IsNotNull(content);
        Assert.IsTrue(content.Length > 0);

        // You can even verify the mock was called with correct parameters
    }).Returns("validated content");

    // Test your code - validation happens automatically
    var service = new DocumentService();
    var document = service.LoadDocument(expectedPath);

    Assert.AreEqual("validated content", document.Content);
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
    var timeService = new TimeService();
    var greeting = timeService.GetGreeting(); // Uses DateTime.Now internally

    Assert.AreEqual("Good morning! Today is 2024-12-25", greeting);
}
```

### Mocking Static Methods with Parameters

```csharp
[Test]
public void Mock_File_Operations()
{
    using var existsMock = Mock.Setup(() => File.Exists("config.json"))
        .Returns(true);

    using var readMock = Mock.Setup(() => File.ReadAllText("config.json"))
        .Returns("{\"database\": \"localhost\", \"port\": 5432}");

    var config = new ConfigurationLoader();
    var settings = config.LoadSettings();

    Assert.AreEqual("localhost", settings.Database);
    Assert.AreEqual(5432, settings.Port);
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
    Assert.AreEqual("Mocked Display Name", result);
}
```

### Mocking Properties

```csharp
[Test]
public void Mock_Static_Property()
{
    using var mock = Mock.SetupProperty(typeof(Environment), nameof(Environment.MachineName))
        .Returns("TEST-MACHINE");

    var machineName = Environment.MachineName;
    Assert.AreEqual("TEST-MACHINE", machineName);
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
    using var anyStringMock = Mock.Setup(() =>
        ValidationHelper.ValidateInput(It.IsAny<string>()))
        .Returns(true);

    // Match specific conditions
    using var conditionalMock = Mock.Setup(() =>
        MathHelper.Calculate(It.Is<int>(x => x > 0)))
        .Returns(100);

    // Match complex objects
    using var objectMock = Mock.Setup(() =>
        UserService.ProcessUser(It.Is<User>(u => u.IsActive && u.Age >= 18)))
        .Returns(new ProcessResult { Success = true });

    // Test your code
    Assert.IsTrue(ValidationHelper.ValidateInput("test"));
    Assert.AreEqual(100, MathHelper.Calculate(5));

    var user = new User { IsActive = true, Age = 25 };
    var result = UserService.ProcessUser(user);
    Assert.IsTrue(result.Success);
}
```

### Advanced Parameter Matching

```csharp
[Test]
public void Advanced_Parameter_Matching()
{
    using var mock = Mock.Setup(() =>
        DataProcessor.Transform(It.Is<DataModel>(data =>
            data.Category == "Important" &&
            data.Priority > 5 &&
            data.CreatedDate >= DateTime.Today)))
        .Returns(new TransformResult { Status = "Processed" });

    var testData = new DataModel
    {
        Category = "Important",
        Priority = 8,
        CreatedDate = DateTime.Now
    };

    var result = DataProcessor.Transform(testData);
    Assert.AreEqual("Processed", result.Status);
}
```

### Parameter Matching with Hierarchical API

```csharp
[Test]
public void Hierarchical_Parameter_Validation()
{
    Mock.Setup(() => DatabaseQuery.Execute(It.IsAny<string>()), () =>
    {
        // Validate the actual parameter that was passed
        var result = DatabaseQuery.Execute("SELECT * FROM Users");
        Assert.IsNotNull(result);

        // You can access the actual parameters and validate them
    }).Returns(new QueryResult { RowCount = 10 });

    var service = new DataService();
    var users = service.GetAllUsers();

    Assert.AreEqual(10, users.Count);
}
```

## Async Support

SMock provides full support for async/await patterns:

### Mocking Async Methods

```csharp
[Test]
public async Task Mock_Async_Methods()
{
    var expectedData = new ApiResponse { Data = "test data" };

    using var mock = Mock.Setup(() =>
        HttpClientHelper.GetDataAsync("https://api.example.com/data"))
        .Returns(Task.FromResult(expectedData));

    var service = new ApiService();
    var result = await service.FetchDataAsync();

    Assert.AreEqual("test data", result.Data);
}
```

### Mocking with Delays

```csharp
[Test]
public async Task Mock_Async_With_Delay()
{
    using var mock = Mock.Setup(() =>
        ExternalService.ProcessAsync(It.IsAny<string>()))
        .Returns(async () =>
        {
            await Task.Delay(100); // Simulate processing time
            return "processed";
        });

    var service = new WorkflowService();
    var result = await service.ExecuteWorkflowAsync("input");

    Assert.AreEqual("processed", result);
}
```

### Exception Handling with Async

```csharp
[Test]
public async Task Mock_Async_Exceptions()
{
    using var mock = Mock.Setup(() =>
        NetworkService.DownloadAsync(It.IsAny<string>()))
        .Throws<HttpRequestException>();

    var service = new DownloadManager();

    var exception = await Assert.ThrowsAsync<HttpRequestException>(
        () => service.DownloadFileAsync("https://example.com/file.zip"));

    Assert.IsNotNull(exception);
}
```

## Advanced Scenarios

### Callback Execution

Execute custom logic when mocks are called:

```csharp
[Test]
public void Mock_With_Callbacks()
{
    var loggedMessages = new List<string>();

    using var mock = Mock.Setup(() => Logger.Log(It.IsAny<string>()))
        .Callback<string>(message =>
        {
            loggedMessages.Add($"Captured: {message}");
            Console.WriteLine($"Mock captured: {message}");
        });

    var service = new BusinessService();
    service.ProcessOrder("ORDER-123");

    Assert.Contains("Captured: Processing order ORDER-123", loggedMessages);
}
```

### Sequential Return Values

Return different values on successive calls:

```csharp
[Test]
public void Sequential_Return_Values()
{
    var callCount = 0;

    using var mock = Mock.Setup(() => RandomNumberGenerator.Next())
        .Returns(() =>
        {
            callCount++;
            return callCount * 10; // Returns 10, 20, 30, ...
        });

    Assert.AreEqual(10, RandomNumberGenerator.Next());
    Assert.AreEqual(20, RandomNumberGenerator.Next());
    Assert.AreEqual(30, RandomNumberGenerator.Next());
}
```

### Conditional Mocking

Different behaviors based on parameters:

```csharp
[Test]
public void Conditional_Mock_Behavior()
{
    using var mock = Mock.Setup(() =>
        SecurityService.ValidateUser(It.IsAny<string>()))
        .Returns<string>(username =>
        {
            if (username.StartsWith("admin_"))
                return new ValidationResult { IsValid = true, Role = "Admin" };
            else if (username.StartsWith("user_"))
                return new ValidationResult { IsValid = true, Role = "User" };
            else
                return new ValidationResult { IsValid = false };
        });

    Assert.AreEqual("Admin", SecurityService.ValidateUser("admin_john").Role);
    Assert.AreEqual("User", SecurityService.ValidateUser("user_jane").Role);
    Assert.IsFalse(SecurityService.ValidateUser("guest").IsValid);
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