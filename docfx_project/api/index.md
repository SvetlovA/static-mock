# SMock API Reference

Welcome to the comprehensive API documentation for SMock, the premier .NET library for static and instance method mocking. This documentation covers all public APIs, interfaces, and extension points available in the SMock library.

## Overview

SMock is designed around a clean, intuitive API that provides two distinct interaction patterns:
- **Sequential API**: Disposable mocking with automatic cleanup
- **Hierarchical API**: Validation-focused mocking with inline assertions

All functionality is accessible through the main `Mock` class and its supporting types.

---

## Core API Classes

### Mock Class
The central entry point for all mocking operations. Contains static methods for both Sequential and Hierarchical API styles.

**Namespace**: `StaticMock`

**Key Features**:
- Static method mocking
- Instance method mocking
- Property mocking
- Expression-based setup
- Type-based setup
- Global configuration

#### Sequential API Methods

| Method | Description | Returns |
|--------|-------------|---------|
| `Setup<T>(Expression<Func<T>>)` | Sets up a function mock with expression | `IFuncMock<T>` |
| `Setup<T>(Expression<Func<Task<T>>>)` | Sets up an async function mock | `IAsyncFuncMock<T>` |
| `Setup(Expression<Action>)` | Sets up an action (void) mock | `IActionMock` |
| `Setup(Type, string)` | Sets up a method mock by type and name | `IFuncMock` |
| `SetupProperty(Type, string)` | Sets up a property mock | `IFuncMock` |
| `SetupAction(Type, string)` | Sets up an action mock by type and name | `IActionMock` |

#### Hierarchical API Methods

| Method | Description | Returns |
|--------|-------------|---------|
| `Setup<T>(Expression<Func<T>>, Action)` | Sets up function mock with validation | `IFuncMock<T>` |
| `Setup<T>(Expression<Func<Task<T>>>, Action)` | Sets up async function mock with validation | `IAsyncFuncMock<T>` |
| `Setup(Expression<Action>, Action)` | Sets up action mock with validation | `IActionMock` |
| `Setup(Type, string, Action)` | Sets up method mock with validation | `IFuncMock` |

#### Usage Examples

```csharp
// Sequential API
using var mock = Mock.Setup(() => DateTime.Now)
    .Returns(new DateTime(2024, 1, 1));

// Hierarchical API
Mock.Setup(() => File.ReadAllText(It.IsAny<string>()), () =>
{
    var content = File.ReadAllText("test.txt");
    Assert.IsNotNull(content);
}).Returns("mocked content");
```

---

## Mock Interface Hierarchy

### IMock
**Base interface for all mock objects**

**Properties**:
- `bool IsDisposed`: Gets whether the mock has been disposed
- `MethodInfo TargetMethod`: Gets the method being mocked

**Methods**:
- `void Dispose()`: Releases the mock and removes hooks

### IFuncMock
**Interface for function (return value) mocks**

**Inherits**: `IMock`

**Methods**:
- `IMock Returns(object value)`: Sets a constant return value
- `IMock Returns(Func<object> valueFactory)`: Sets a dynamic return value
- `IMock Throws<TException>()`: Configures mock to throw exception
- `IMock Throws(Exception exception)`: Configures mock to throw specific exception
- `IMock Callback(Action callback)`: Adds callback execution

### IFuncMock\<T\>
**Generic interface for strongly-typed function mocks**

**Inherits**: `IFuncMock`

**Methods**:
- `IFuncMock<T> Returns(T value)`: Sets typed return value
- `IFuncMock<T> Returns(Func<T> valueFactory)`: Sets dynamic typed return value
- `IFuncMock<T> Returns<TArg>(Func<TArg, T> valueFactory)`: Sets parameter-based return value
- `IFuncMock<T> Callback<TArg>(Action<TArg> callback)`: Adds typed callback

### IAsyncFuncMock\<T\>
**Interface for asynchronous function mocks**

**Inherits**: `IFuncMock<T>`

**Methods**:
- `IAsyncFuncMock<T> Returns(Task<T> task)`: Sets async return value
- `IAsyncFuncMock<T> Returns(Func<Task<T>> taskFactory)`: Sets dynamic async return value

### IActionMock
**Interface for action (void method) mocks**

**Inherits**: `IMock`

**Methods**:
- `IActionMock Throws<TException>()`: Configures action to throw exception
- `IActionMock Throws(Exception exception)`: Configures action to throw specific exception
- `IActionMock Callback(Action callback)`: Adds callback execution
- `IActionMock Callback<TArg>(Action<TArg> callback)`: Adds typed callback

---

## Parameter Matching

### It Class
**Provides parameter matching capabilities for method arguments**

**Namespace**: `StaticMock.Entities.Context`

**Methods**:

#### IsAny\<T\>()
Matches any argument of the specified type.

```csharp
Mock.Setup(() => Service.Process(It.IsAny<string>()))
    .Returns("result");
```

#### Is\<T\>(Expression\<Func\<T, bool\>\>)
Matches arguments that satisfy the specified predicate condition.

```csharp
Mock.Setup(() => Math.Abs(It.Is<int>(x => x < 0)))
    .Returns(42);
```

**Parameters**:
- `predicate`: The condition that arguments must satisfy

**Exception Behavior**: Throws exception during mock execution if predicate fails

---

## Context and Configuration

### SetupContext
**Provides context for parameter matching in mock expressions**

**Namespace**: `StaticMock.Entities.Context`

**Properties**:
- `It It`: Gets the parameter matching helper

**Usage**:
```csharp
Mock.Setup(context => Service.Method(context.It.IsAny<string>()))
    .Returns("result");
```

### GlobalSettings
**Global configuration options for SMock behavior**

**Namespace**: `StaticMock.Entities`

**Accessible via**: `Mock.GlobalSettings`

**Properties**:
- `HookManagerType HookManagerType`: Configures the hook implementation strategy

### SetupProperties
**Configuration options for individual mock setups**

**Namespace**: `StaticMock.Entities`

**Properties**:
- `BindingFlags BindingFlags`: Method/property binding flags for reflection
- `Type[] ParameterTypes`: Explicit parameter type specification (for overload resolution)

**Usage**:
```csharp
Mock.Setup(typeof(MyClass), "OverloadedMethod",
    new SetupProperties
    {
        BindingFlags = BindingFlags.Public | BindingFlags.Static,
        ParameterTypes = new[] { typeof(string), typeof(int) }
    });
```

---

## Advanced Interfaces

### ICallbackMock
**Interface for mocks that support callback execution**

**Methods**:
- `ICallbackMock Callback(Action action)`: Adds parameterless callback
- `ICallbackMock Callback<T>(Action<T> callback)`: Adds single-parameter callback
- `ICallbackMock Callback<T1, T2>(Action<T1, T2> callback)`: Adds two-parameter callback

### IReturnsMock
**Interface for mocks that support return value configuration**

**Methods**:
- `IReturnsMock Returns(object value)`: Sets return value
- `IReturnsMock Returns(Func<object> valueFactory)`: Sets dynamic return value

### IThrowsMock
**Interface for mocks that support exception throwing**

**Methods**:
- `IThrowsMock Throws<TException>()` where TException : Exception, new(): Throws exception type
- `IThrowsMock Throws(Exception exception)`: Throws specific exception instance

---

## Hook Management (Advanced)

### IHookManager
**Internal interface for managing method hooks**

**Note**: This is an advanced interface typically not used directly by consumers.

**Methods**:
- `void Dispose()`: Removes hooks and cleans up
- `bool IsDisposed`: Gets disposal status

### HookManagerType Enumeration
**Specifies the hook implementation strategy**

**Values**:
- `MonoMod`: Use MonoMod-based hooks (default)

---

## Extension Methods and Utilities

### Validation Helpers
SMock includes internal validation to ensure proper usage:

- **Expression Validation**: Ensures mock expressions are valid
- **Parameter Type Checking**: Validates parameter types match method signatures
- **Hook Compatibility**: Verifies methods can be safely hooked

### Error Handling
Common exceptions thrown by SMock:

| Exception | Condition |
|-----------|-----------|
| `ArgumentException` | Invalid expression or parameter setup |
| `InvalidOperationException` | Mock already disposed or invalid state |
| `MethodAccessException` | Method cannot be hooked (e.g., generic constraints) |
| `NotSupportedException` | Unsupported method type or signature |

---

## Usage Patterns

### Basic Function Mock
```csharp
// Sequential
using var mock = Mock.Setup(() => Math.Abs(-5))
    .Returns(10);

// Hierarchical
Mock.Setup(() => Math.Abs(-5), () =>
{
    Assert.AreEqual(10, Math.Abs(-5));
}).Returns(10);
```

### Property Mock
```csharp
using var mock = Mock.SetupProperty(typeof(DateTime), nameof(DateTime.Now))
    .Returns(new DateTime(2024, 1, 1));
```

### Parameter-Based Returns
```csharp
using var mock = Mock.Setup(() => Math.Max(It.IsAny<int>(), It.IsAny<int>()))
    .Returns<int, int>((a, b) => a > b ? a : b);
```

### Callback Execution
```csharp
var calls = new List<string>();

using var mock = Mock.Setup(() => Console.WriteLine(It.IsAny<string>()))
    .Callback<string>(message => calls.Add(message));
```

### Exception Testing
```csharp
using var mock = Mock.Setup(() => File.ReadAllText("missing.txt"))
    .Throws<FileNotFoundException>();

Assert.Throws<FileNotFoundException>(() => File.ReadAllText("missing.txt"));
```

---

## Performance Characteristics

### Mock Setup
- **Cost**: ~1-2ms per mock setup
- **Memory**: Minimal allocation for hook metadata
- **Threading**: Thread-safe setup operations

### Method Interception
- **Overhead**: <0.1ms per intercepted call
- **Memory**: No additional allocation per call
- **Threading**: Thread-safe interception

### Disposal and Cleanup
- **Sequential**: Immediate cleanup on dispose
- **Hierarchical**: Cleanup on test completion
- **Memory**: Hooks fully removed, no leaks

---

## Platform Compatibility

### Supported Runtimes
- .NET 5.0+
- .NET Core 2.0+
- .NET Framework 4.62-4.81
- .NET Standard 2.0+

### Known Limitations
- **Generic Methods**: Limited support for open generic methods
- **Unsafe Code**: Some unsafe method signatures not supported
- **Native Interop**: P/Invoke methods cannot be mocked
- **Compiler-Generated**: Some compiler-generated methods may not be hookable

---

## Migration and Compatibility

### From Other Mocking Frameworks

**Moq Migration**:
```csharp
// Moq
mock.Setup(x => x.Method()).Returns("result");

// SMock
using var smock = Mock.Setup(() => StaticClass.Method())
    .Returns("result");
```

**NSubstitute Migration**:
```csharp
// NSubstitute
substitute.Method().Returns("result");

// SMock
using var mock = Mock.Setup(() => StaticClass.Method())
    .Returns("result");
```

### Backward Compatibility
SMock maintains backward compatibility within major versions. Breaking changes are only introduced in major version updates with migration guides provided.

---

## See Also

- [Getting Started Guide](../articles/getting-started.md) - Complete walkthrough with examples
- [GitHub Repository](https://github.com/SvetlovA/static-mock) - Source code and issues
- [NuGet Package](https://www.nuget.org/packages/SMock) - Package downloads and versions
- [Release Notes](https://github.com/SvetlovA/static-mock/releases) - Version history and changes