# SMock - Static & Instance Method Mocking for .NET

[![NuGet Version](https://img.shields.io/nuget/v/SMock.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/SMock)
[![NuGet Downloads](https://img.shields.io/nuget/dt/SMock.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/SMock)
[![GitHub Stars](https://img.shields.io/github/stars/SvetlovA/static-mock?style=for-the-badge&logo=github)](https://github.com/SvetlovA/static-mock/stargazers)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge)](LICENSE)
[![.NET Version](https://img.shields.io/badge/.NET-Standard%202.0%2B-purple.svg?style=for-the-badge)](https://dotnet.microsoft.com/)

**The only .NET library that makes testing static methods effortless!**

---

## Why SMock?

SMock revolutionizes .NET testing by breaking down the barriers that make legacy code, third-party dependencies, and static APIs difficult to test. Built on the powerful [MonoMod](https://github.com/MonoMod/MonoMod) runtime modification technology, SMock provides capabilities that other mocking frameworks simply cannot offer.

### Key Features

- **🎯 Mock Static Methods**: The only .NET library that seamlessly handles static method mocking
- **🎨 Dual API Design**: Choose between Hierarchical (validation-focused) or Sequential (disposable) patterns
- **⚡ Zero Configuration**: Works instantly with any test framework (NUnit, xUnit, MSTest)
- **🌊 Complete Feature Set**: Full support for async/await, parameter matching, callbacks, exceptions
- **🔒 Safe & Isolated**: Each test runs in complete isolation with automatic cleanup
- **⚡ High Performance**: Minimal runtime overhead with optimized IL modification

---

## Installation

### Package Manager Console
```powershell
Install-Package SMock
```

### .NET CLI
```bash
dotnet add package SMock
```

### PackageReference
```xml
<PackageReference Include="SMock" Version="*" />
```

> **Compatibility**: SMock supports .NET Standard 2.0+ and .NET Framework 4.62-4.81, ensuring broad compatibility across the .NET ecosystem.

---

## Quick Start Examples

### Sequential API - Clean & Scoped
Perfect for straightforward mocking with automatic cleanup:

```csharp
[Test]
public void TestFileOperations()
{
    // Mock file existence check
    using var existsMock = Mock.Setup(() => File.Exists("config.json"))
        .Returns(true);

    // Mock file content reading
    using var readMock = Mock.Setup(() => File.ReadAllText("config.json"))
        .Returns("{\"setting\": \"test\"}");

    // Your code under test
    var configService = new ConfigurationService();
    var setting = configService.GetSetting("setting");

    Assert.AreEqual("test", setting);
} // Mocks automatically cleaned up here
```

### Hierarchical API - Validation During Execution
Perfect for inline validation and complex testing scenarios:

```csharp
[Test]
public void TestDatabaseOperations()
{
    var expectedQuery = "SELECT * FROM Users WHERE Active = 1";

    Mock.Setup(context => DatabaseHelper.ExecuteQuery(context.It.IsAny<string>()), () =>
    {
        // This validation runs DURING the mock execution
        var result = DatabaseHelper.ExecuteQuery(expectedQuery);
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Count > 0);
    }).Returns(new List<User> { new User { Name = "Test User" } });

    // Test your service
    var userService = new UserService();
    var activeUsers = userService.GetActiveUsers();

    Assert.AreEqual(1, activeUsers.Count);
    Assert.AreEqual("Test User", activeUsers.First().Name);
}
```

---

## Core Concepts

### Runtime Hook Technology

SMock uses advanced runtime modification techniques to intercept method calls:

```csharp
// 1. Setup Phase - Create hook for target method
var mock = Mock.Setup(() => DateTime.Now);

// 2. Configuration - Define behavior
mock.Returns(new DateTime(2024, 1, 1));

// 3. Execution - Your code calls the original method, but gets the mock
var testTime = DateTime.Now; // Returns mocked value: 2024-01-01

// 4. Cleanup - Hook automatically removed (Sequential) or managed (Hierarchical)
```

### Key Benefits

- **🎯 Non-Invasive**: No source code changes required
- **🔒 Isolated**: Each test runs independently
- **⚡ Fast**: Minimal performance impact
- **🧹 Auto-Cleanup**: Hooks automatically removed after tests

---

## Advanced Features

### Parameter Matching with `It`

SMock provides powerful parameter matching capabilities:

```csharp
// Match any argument
Mock.Setup(context => MyService.Process(context.It.IsAny<string>()))
    .Returns("mocked");

// Match with conditions
Mock.Setup(context => MyService.Process(context.It.Is<string>(s => s.StartsWith("test_"))))
    .Returns("conditional_mock");

// Match specific values with complex conditions
Mock.Setup(context => DataProcessor.Transform(context.It.Is<DataModel>(d =>
    d.IsValid && d.Priority > 5)))
    .Returns(new ProcessedData { Success = true });
```

### Async Method Support

Full support for asynchronous operations:

```csharp
// Mock async methods (Sequential)
using var mock = Mock.Setup(() => HttpClient.GetStringAsync("https://api.example.com"))
    .Returns(Task.FromResult("{\"data\": \"test\"}"));

// Mock async methods (Hierarchical)
Mock.Setup(context => DatabaseService.GetUserAsync(context.It.IsAny<int>()), async () =>
{
    var user = await DatabaseService.GetUserAsync(123);
    Assert.IsNotNull(user);
}).Returns(Task.FromResult(new User { Id = 123, Name = "Test" }));
```

### Exception Handling

Easy exception testing:

```csharp
// Sequential approach
using var mock = Mock.Setup(() => FileHelper.ReadConfig("invalid.json"))
    .Throws<FileNotFoundException>();

Assert.Throws<FileNotFoundException>(() =>
    FileHelper.ReadConfig("invalid.json"));

// Hierarchical approach
Mock.Setup(() => ApiClient.CallEndpoint("/api/test"), () =>
{
    Assert.Throws<HttpRequestException>(() =>
        ApiClient.CallEndpoint("/api/test"));
}).Throws<HttpRequestException>();
```

### Callback Execution

Execute custom logic during mock calls:

```csharp
var callCount = 0;

Mock.Setup(context => Logger.LogMessage(context.It.IsAny<string>()), () =>
{
    var result = Logger.LogMessage("test");
    Assert.IsTrue(callCount > 0);
}).Callback<string>(message =>
{
    callCount++;
    Console.WriteLine($"Logged: {message}");
});
```

---

## Best Practices

### Test Organization

```csharp
[TestFixture]
public class ServiceTests
{
    [SetUp]
    public void Setup()
    {
        // SMock works with any setup/teardown approach
        // No special initialization required
    }

    [Test]
    public void Should_Handle_FileSystem_Operations()
    {
        // Group related mocks together
        using var existsMock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
            .Returns(true);
        using var readMock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
            .Returns("test content");

        // Test your logic
        var processor = new FileProcessor();
        var result = processor.ProcessFiles();

        Assert.IsNotNull(result);
    }
}
```

### Performance Considerations

- **Mock Reuse**: Create mocks once per test method
- **Cleanup**: Always use `using` statements with Sequential API
- **Scope**: Keep mock scope as narrow as possible
- **Validation**: Use Hierarchical API when you need immediate validation

---

## Framework Support

SMock integrates seamlessly with all major .NET testing frameworks:

| Framework | Support | Notes |
|-----------|---------|-------|
| **NUnit** | ✅ Full | Recommended for attribute-based testing |
| **xUnit** | ✅ Full | Excellent for fact/theory patterns |
| **MSTest** | ✅ Full | Perfect for Visual Studio integration |
| **Custom** | ✅ Full | Works with any testing approach |

---

## Troubleshooting

### Common Issues

**Mock Not Triggering**
```csharp
// ❌ Incorrect - Mock won't trigger
Mock.Setup(() => MyClass.Method()).Returns("test");
MyClass.Method(); // Different instance

// ✅ Correct - Mock triggers properly
Mock.Setup(() => MyClass.Method()).Returns("test");
var result = MyClass.Method(); // Same static call
```

**Parameter Matching Issues**
```csharp
// ❌ Incorrect - Too specific
Mock.Setup(() => MyClass.Process("exact_string")).Returns("result");

// ✅ Better - Use parameter matching
Mock.Setup(context => MyClass.Process(context.It.IsAny<string>())).Returns("result");
```

### Getting Help

- 📖 [Full API Documentation](api/index.md)
- 📝 [Comprehensive Examples](articles/getting-started.md)
- 🐛 [Report Issues](https://github.com/SvetlovA/static-mock/issues)
- 💬 [Join Discussions](https://github.com/SvetlovA/static-mock/discussions)

---

## What's Next?

Ready to dive deeper? Explore our comprehensive documentation:

### 🚀 **Getting Started**
- **[Getting Started Guide](articles/getting-started.md)** - Detailed walkthrough with examples
- **[Testing Framework Integration](articles/framework-integration.md)** - NUnit, xUnit, MSTest, and more

### 📚 **Advanced Topics**
- **[Advanced Usage Patterns](articles/advanced-patterns.md)** - Complex scenarios and best practices
- **[Real-World Examples](articles/real-world-examples.md)** - Enterprise case studies and practical examples
- **[Performance Guide](articles/performance-guide.md)** - Optimization strategies and benchmarks

### 🛠️ **Reference & Support**
- **[API Reference](api/index.md)** - Complete API documentation
- **[Migration Guide](articles/migration-guide.md)** - Upgrading and switching from other frameworks
- **[Troubleshooting & FAQ](articles/troubleshooting.md)** - Solutions to common issues

---

## Support the Project

SMock is developed and maintained as an open-source project. If you find it useful and would like to support its continued development, consider sponsoring the project:

- ⭐ **[GitHub Sponsors](https://github.com/sponsors/SvetlovA)** - Direct support through GitHub
- 🎯 **[Patreon](https://patreon.com/svtlv)** - Monthly support with exclusive updates
- 💝 **[Boosty](https://boosty.to/svtlv)** - Alternative sponsorship platform

Your support helps maintain the project, add new features, and provide community support. Every contribution, no matter the size, is greatly appreciated!

## License

SMock is available under the [MIT License](https://github.com/SvetlovA/static-mock/blob/master/LICENSE).

---

*Made with ❤️ by [@SvetlovA](https://github.com/SvetlovA) and the SMock community*