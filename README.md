# 🎯 SMock - Static & Instance Method Mocking for .NET

<div align="center">

[![NuGet Version](https://img.shields.io/nuget/v/SMock.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/SMock)
[![NuGet Downloads](https://img.shields.io/nuget/dt/SMock.svg?style=for-the-badge&logo=nuget)](https://www.nuget.org/packages/SMock)
[![GitHub Stars](https://img.shields.io/github/stars/SvetlovA/static-mock?style=for-the-badge&logo=github)](https://github.com/SvetlovA/static-mock/stargazers)
[![License](https://img.shields.io/badge/license-MIT-blue.svg?style=for-the-badge)](LICENSE)
[![.NET Version](https://img.shields.io/badge/.NET-Standard%202.0%2B-purple.svg?style=for-the-badge)](https://dotnet.microsoft.com/)

**A mocking library that makes testing static methods easier!**

</div>

---

## ✨ Why SMock?

SMock breaks down the barriers of testing legacy code, third-party dependencies, and static APIs. Built on [MonoMod](https://github.com/MonoMod/MonoMod) runtime modification technology, SMock gives you the power to mock what others can't.

- **🎯 Mock Static Methods**: The only .NET library that handles static methods seamlessly
- **🎨 Two API Styles**: Choose Hierarchical (with validation) or Sequential (disposable) patterns
- **⚡ Zero Configuration**: Works with your existing test frameworks (NUnit, xUnit, MSTest)
- **🌊 Complete Feature Set**: Async/await, parameter matching, callbacks, exceptions, unsafe code

---

## 📦 Installation

### Package Manager
```powershell
Install-Package SMock
```

### .NET CLI
```bash
dotnet add package SMock
```

> 💡 **Pro Tip**: SMock works great with any testing framework - NUnit, xUnit, MSTest, you name it!

---

## 🚀 Quick Start

```csharp
// Mock a static method in just one line - Sequential API
using var mock = Mock.Setup(() => File.ReadAllText("config.json"))
    .Returns("{ \"setting\": \"test\" }");

// Your code now uses the mocked value!
var content = File.ReadAllText("config.json"); // Returns test JSON

// Or use the Hierarchical API with inline validation
Mock.Setup(() => DateTime.Now, () =>
{
    var result = DateTime.Now;
    Assert.AreEqual(new DateTime(2024, 1, 1), result);
}).Returns(new DateTime(2024, 1, 1));
```

---

## 🧠 Core Concepts

### 🔄 Hook-Based Runtime Modification

SMock uses [MonoMod](https://github.com/MonoMod/MonoMod) to create **runtime hooks** that intercept method calls:

- **🎯 Non-Invasive**: No source code changes required
- **🔒 Isolated**: Each test runs in isolation
- **⚡ Fast**: Minimal performance overhead
- **🧹 Auto-Cleanup**: Hooks automatically removed after test completion

### 🎭 Mock Lifecycle

```csharp
// 1. Setup: Create a mock for the target method
var mock = Mock.Setup(() => DateTime.Now);

// 2. Configure: Define return values or behaviors
mock.Returns(new DateTime(2024, 1, 1));

// 3. Execute: Run your code - calls are intercepted
var now = DateTime.Now; // Returns mocked value

// 4. Cleanup: Dispose mock (Sequential) or automatic (Hierarchical)
mock.Dispose(); // Or automatic with 'using'
```

---

## 🎨 API Styles

SMock provides **two distinct API patterns** to fit different testing preferences:

### 🔄 Sequential API

Perfect for **clean, scoped mocking** with automatic cleanup:

```csharp
[Test]
public void TestFileOperations()
{
    // Mock file existence check
    using var existsMock = Mock.Setup(() => File.Exists("test.txt"))
        .Returns(true);

    // Mock file content reading
    using var readMock = Mock.Setup(() => File.ReadAllText("test.txt"))
        .Returns("Hello World");

    // Your code under test
    var processor = new FileProcessor();
    var result = processor.ProcessFile("test.txt");

    Assert.AreEqual("HELLO WORLD", result);
} // Mocks automatically cleaned up here
```

### 🏗️ Hierarchical API

Perfect for **inline validation** during mock execution:

```csharp
[Test]
public void TestDatabaseConnection()
{
    var expectedConnectionString = "Server=localhost;Database=test;";

    Mock.Setup(() => DatabaseConnection.Connect(It.IsAny<string>()), () =>
    {
        // This validation runs DURING the mock execution
        var actualCall = DatabaseConnection.Connect(expectedConnectionString);
        Assert.IsNotNull(actualCall);
        Assert.IsTrue(actualCall.IsConnected);
    }).Returns(new MockConnection { IsConnected = true });

    // Test your service
    var service = new DatabaseService();
    service.InitializeConnection(expectedConnectionString);
}
```

---

## 🔧 Core Features

### 🌊 Async/Await & Instance Methods

**Sequential API:**
```csharp
[Test]
public async Task TestAsync_Sequential()
{
    using var asyncMock = Mock.Setup(() => HttpClient.GetAsync(It.IsAny<string>()))
        .Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));

    var result = await HttpClient.GetAsync("https://example.com");
    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
}
```

**Hierarchical API:**
```csharp
[Test]
public async Task TestAsync_Hierarchical()
{
    Mock.Setup(() => HttpClient.GetAsync(It.IsAny<string>()), async () =>
    {
        var result = await HttpClient.GetAsync("https://example.com");
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        Assert.IsNotNull(result.Content);
    }).Returns(Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)));
}
```

### 🎛️ Properties & Callbacks

**Sequential API:**
```csharp
[Test]
public void TestPropertiesAndCallbacks_Sequential()
{
    var callbackExecuted = false;

    using var propMock = Mock.Setup(() => Environment.MachineName)
        .Returns("TEST-MACHINE");

    using var callbackMock = Mock.Setup(() => Logger.Log(It.IsAny<string>()))
        .Callback<string>(message => callbackExecuted = true);

    Assert.AreEqual("TEST-MACHINE", Environment.MachineName);
    Logger.Log("Test message");
    Assert.IsTrue(callbackExecuted);
}
```

**Hierarchical API:**
```csharp
[Test]
public void TestPropertiesAndCallbacks_Hierarchical()
{
    var callbackExecuted = false;

    Mock.Setup(() => Environment.MachineName, () =>
    {
        var machineName = Environment.MachineName;
        Assert.AreEqual("TEST-MACHINE", machineName);
    }).Returns("TEST-MACHINE");

    Mock.Setup(() => Logger.Log(It.IsAny<string>()), () =>
    {
        callbackExecuted = true;
        Logger.Log("Test message");
        Assert.IsTrue(callbackExecuted);
    }).Callback<string>(message => Console.WriteLine($"Logged: {message}"));
}
```

### 🚨 Exception Handling & Parameter Matching

```csharp
[Test]
public void TestExceptionsAndMatching()
{
    // Exception throwing
    using var exceptionMock = Mock.Setup(() => DatabaseConnection.Connect(It.IsAny<string>()))
        .Throws<ConnectionException>();

    // Parameter matching with It.IsAny<T>() and It.Is<T>(predicate)
    using var matchingMock = Mock.Setup(() => MathUtils.Calculate(It.Is<int>(x => x > 0)))
        .Returns(100);

    Assert.Throws<ConnectionException>(() => DatabaseConnection.Connect("invalid"));
    Assert.AreEqual(100, MathUtils.Calculate(5)); // Matches predicate
}
```

### 🔧 Advanced Scenarios

```csharp
[Test]
public void TestAdvancedScenarios()
{
    // Mock private/internal methods with SetupProperties
    var setupProps = new SetupProperties
    {
        BindingFlags = BindingFlags.NonPublic | BindingFlags.Static,
        GenericTypes = new[] { typeof(string), typeof(int) }
    };

    using var privateMock = Mock.Setup(typeof(InternalUtility), "ProcessGeneric", setupProps)
        .Returns("processed_result");

    // Mock by type name for dynamic scenarios
    using var reflectiveMock = Mock.Setup(typeof(StaticUtilities), "GetTimestamp")
        .Returns(12345L);

    // Mock specific instance
    var calculator = new Calculator();
    var instanceProps = new SetupProperties { Instance = calculator };
    using var instanceMock = Mock.Setup(typeof(Calculator), "Calculate", instanceProps)
        .Returns(42);

    Assert.AreEqual(12345L, StaticUtilities.GetTimestamp());
    Assert.AreEqual(42, calculator.Calculate());
}
```


---

## ⚡ Performance

SMock is designed for **minimal performance impact**:

- **🚀 Runtime Hooks**: Only active during tests
- **⚡ Zero Production Overhead**: No dependencies in production builds
- **🎯 Efficient Interception**: Built on MonoMod's optimized IL modification
- **📊 Benchmarked**: Comprehensive performance testing with BenchmarkDotNet

### Performance Characteristics

| Operation | Overhead | Notes |
|-----------|----------|--------|
| **Mock Setup** | ~1-2ms | One-time cost per mock |
| **Method Interception** | <0.1ms | Minimal runtime impact |
| **Cleanup** | <1ms | Automatic hook removal |
| **Memory Usage** | Minimal | Temporary IL modifications only |

---

## 📚 Additional Resources

- **📖 [API Documentation](https://svetlova.github.io/static-mock/api/index.html)**
- **📝 [More Examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests)**
- **🎯 [Hierarchical API Examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests/Hierarchical)**
- **🔄 [Sequential API Examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests/Sequential)**

---

## 📄 License

This library is available under the [MIT license](https://github.com/SvetlovA/static-mock/blob/master/LICENSE).

---

<div align="center">

## 🚀 Ready to revolutionize your .NET testing?

**[⚡ Get Started Now](#-installation)** | **[📚 View Examples](https://github.com/SvetlovA/static-mock/tree/master/src/StaticMock.Tests/Tests)** | **[💬 Join Discussion](https://github.com/SvetlovA/static-mock/discussions)**

---

*Made with ❤️ by [@SvetlovA](https://github.com/SvetlovA) and the SMock community*

</div>