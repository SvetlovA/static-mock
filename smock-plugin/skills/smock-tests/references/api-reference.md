# SMock API Reference

Full reference for the `StaticMock` library. All public API is on the static `Mock` class.

## Namespace

```csharp
using StaticMock;
```

---

## Sequential API

Returns disposable mock objects. Always wrap in `using var`.

### Func mocks (methods that return a value)

```csharp
// No parameters
IFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<TReturn>> expr);

// With parameter matching via SetupContext
IFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<SetupContext, TReturn>> expr);

// Async
IAsyncFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<Task<TReturn>>> expr);
IAsyncFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<SetupContext, Task<TReturn>>> expr);
```

### Action mocks (void methods)

```csharp
IActionMock mock = Mock.Setup(Expression<Action> expr);
IActionMock mock = Mock.Setup(Expression<Action<SetupContext>> expr);
```

### Property mocks

```csharp
IFuncMock mock = Mock.SetupProperty(Type type, string propertyName);
IFuncMock mock = Mock.SetupProperty(Type type, string propertyName, BindingFlags flags);
IFuncMock mock = Mock.SetupProperty(Type type, string propertyName, SetupProperties props);
```

### Type + method name overloads (reflection-based)

```csharp
IFuncMock mock = Mock.Setup(Type type, string methodName);
IFuncMock mock = Mock.Setup(Type type, string methodName, BindingFlags flags);
IFuncMock mock = Mock.Setup(Type type, string methodName, SetupProperties props);
IActionMock mock = Mock.SetupAction(Type type, string methodName);
IActionMock mock = Mock.SetupAction(Type type, string methodName, BindingFlags flags);
void Mock.SetupDefault(Type type, string methodName);
```

---

## Hierarchical API

Accepts a validation `Action` that executes during the mock call. No disposal needed.

```csharp
// Func with validation
IFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<TReturn>> expr, Action validation);
IFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<SetupContext, TReturn>> expr, Action validation);

// Async with validation
IAsyncFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<Task<TReturn>>> expr, Action validation);
IAsyncFuncMock<TReturn> mock = Mock.Setup<TReturn>(Expression<Func<SetupContext, Task<TReturn>>> expr, Action validation);

// Action (void) with validation
IActionMock mock = Mock.Setup(Expression<Action> expr, Action validation);
IActionMock mock = Mock.Setup(Expression<Action<SetupContext>> expr, Action validation);

// SetupDefault — no return value, no mock object
void Mock.SetupDefault(Expression<Action> expr, Action validation);
void Mock.SetupDefault(Type type, string methodName, Action validation);
```

---

## Configuring Mock Behavior

### On `IFuncMock` / `IFuncMock<TReturn>` (methods with return values)

#### `.Returns(value)` — fixed return value

```csharp
using var mock = Mock.Setup(() => DateTime.Now).Returns(new DateTime(2024, 1, 1));
```

#### `.Returns(Func<TReturn> factory)` — dynamic/sequential return values

```csharp
var count = 0;
using var mock = Mock.Setup(() => DateTime.Now)
    .Returns(() => count++ == 0 ? new DateTime(2024, 1, 1) : new DateTime(2024, 1, 2));
```

#### `.Returns<TArg>(Func<TArg, TReturn> selector)` — return based on input (1 arg)

```csharp
using var mock = Mock.Setup(context => Environment.GetEnvironmentVariable(context.It.IsAny<string>()))
    .Returns<string>(key => key == "ENV" ? "test" : null);
```

Multi-arg variants exist up to 9 arguments:
```csharp
.Returns<TArg1, TArg2>(Func<TArg1, TArg2, TReturn> getValue)
// ... up to 9 type parameters
```

#### `.ReturnsAsync<TReturn>(TReturn value)` — shorthand for async return (on `IFuncMock`)

```csharp
// Equivalent to .Returns(Task.FromResult(value)) but cleaner
using var mock = Mock.Setup(() => Service.GetAsync()).ReturnsAsync("result");
```

---

### On `IAsyncFuncMock<TReturn>` (async methods)

#### `.ReturnsAsync(TReturn value)` — returns a completed Task wrapping value

```csharp
using var mock = Mock.Setup(() => HttpService.GetAsync("url")).ReturnsAsync("response body");
var result = await HttpService.GetAsync("url"); // → "response body"
```

#### Also inherits all `.Returns(...)` overloads from `IFuncMock<Task<TReturn>>`

```csharp
// Dynamic async return
using var mock = Mock.Setup(() => Service.GetAsync())
    .Returns(async () =>
    {
        await Task.Delay(1);
        return "value";
    });
```

---

### On `IActionMock` (void methods only)

**`Callback` is ONLY available on `IActionMock` — not on `IFuncMock`.**

#### `.Callback(Action callback)` — zero-arg callback

```csharp
var called = false;
using var mock = Mock.Setup(context => Logger.Log(context.It.IsAny<string>()))
    .Callback(() => called = true);
```

#### `.Callback<TArg>(Action<TArg> callback)` — single-arg callback

```csharp
var messages = new List<string>();
using var mock = Mock.Setup(context => Logger.Log(context.It.IsAny<string>()))
    .Callback<string>(msg => messages.Add(msg));
```

Multi-arg variants exist up to 9 arguments:
```csharp
.Callback<TArg1, TArg2>(Action<TArg1, TArg2> callback)
// ... up to 9 type parameters
```

---

### On all mock types (`IMock`) — Throws

**There is no `Throws(Exception instance)` overload.** Use one of these:

#### `.Throws<TException>()` — throw by generic type (requires parameterless constructor)

```csharp
using var mock = Mock.Setup(() => SomeClass.Method()).Throws<InvalidOperationException>();
```

#### `.Throws<TException>(object[] constructorArgs)` — throw with constructor arguments

```csharp
using var mock = Mock.Setup(() => SomeClass.Method())
    .Throws<ArgumentException>(new object[] { "Value was invalid", "paramName" });
```

#### `.Throws(Type exceptionType)` — throw by reflection type

```csharp
using var mock = Mock.Setup(() => SomeClass.Method())
    .Throws(typeof(FileNotFoundException));
```

#### `.Throws(Type exceptionType, params object[] constructorArgs)` — reflection type with args

```csharp
using var mock = Mock.Setup(() => SomeClass.Method())
    .Throws(typeof(ArgumentException), "Value was invalid", "paramName");
```

---

## Parameter Matching

Access `It` only through the `SetupContext` lambda parameter — never standalone.

### `context.It.IsAny<T>()` — match any value of type T

```csharp
Mock.Setup(context => File.Exists(context.It.IsAny<string>())).Returns(true);
```

### `context.It.Is<T>(Predicate<T> predicate)` — match with condition

```csharp
Mock.Setup(context => Convert.ToInt32(context.It.Is<string>(s => s.Length > 0)))
    .Returns(42);
```

### Multiple parameters

```csharp
Mock.Setup(context => Path.Combine(context.It.IsAny<string>(), context.It.IsAny<string>()))
    .Returns("combined/path");
```

---

## Mock Interface Summary

| Interface | Source | Key Methods |
|-----------|--------|-------------|
| `IFuncMock` | `Mock.Setup(Type, string)` | `.Returns<TRet>(value)`, `.ReturnsAsync<TRet>(value)`, `.Throws<T>()` |
| `IFuncMock<T>` | `Mock.Setup(() => method())` | `.Returns(value)`, `.Returns<TArg>(selector)`, `.Throws<T>()` |
| `IAsyncFuncMock<T>` | `Mock.Setup(() => asyncMethod())` | `.ReturnsAsync(value)`, inherits `IFuncMock<Task<T>>` |
| `IActionMock` | `Mock.Setup(() => voidMethod())` | `.Callback()`, `.Callback<T>(action)`, `.Throws<T>()` |

All interfaces implement `IDisposable` for Sequential mocks.

---

## SetupProperties

Controls method resolution in reflection-based overloads:

```csharp
var props = new SetupProperties
{
    BindingFlags = BindingFlags.Public | BindingFlags.Static
};
using var mock = Mock.Setup(typeof(MyClass), "MyMethod", props);
```

---

## Complete Examples

### Static method, no parameters (Sequential)

```csharp
using var mock = Mock.Setup(() => DateTime.Now).Returns(new DateTime(2024, 1, 1));
var now = DateTime.Now; // → 2024-01-01
```

### Static method with parameter matching

```csharp
using var mock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
    .Returns("mocked content");
var content = File.ReadAllText("any.txt"); // → "mocked content"
```

### Instance method

```csharp
var instance = new MyService();
using var mock = Mock.Setup(() => instance.GetData()).Returns("mocked");
```

### Async method (preferred)

```csharp
using var mock = Mock.Setup(() => HttpService.GetAsync("https://api.example.com"))
    .ReturnsAsync("{\"status\":\"ok\"}");
var result = await HttpService.GetAsync("https://api.example.com");
```

### Void method with callback

```csharp
var logged = new List<string>();
using var mock = Mock.Setup(context => AuditLog.Write(context.It.IsAny<string>()))
    .Callback<string>(msg => logged.Add(msg));
AuditLog.Write("event1");
ClassicAssert.AreEqual(1, logged.Count);
```

### Exception with constructor args

```csharp
using var mock = Mock.Setup(() => Parser.Parse("bad"))
    .Throws<FormatException>(new object[] { "Invalid format" });
```

### Hierarchical with inline validation

```csharp
Mock.Setup(context => UserRepo.FindById(context.It.IsAny<int>()), () =>
{
    var user = UserRepo.FindById(42);
    ClassicAssert.IsNotNull(user);
    ClassicAssert.AreEqual("Alice", user.Name);
}).Returns(new User { Id = 42, Name = "Alice" });
```

### Conditional return based on input

```csharp
using var mock = Mock.Setup(context =>
    Environment.GetEnvironmentVariable(context.It.IsAny<string>()))
    .Returns<string>(key => key switch
    {
        "ENV" => "staging",
        "TIMEOUT" => "30",
        _ => null
    });
```
