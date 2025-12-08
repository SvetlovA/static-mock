# Advanced Usage Patterns

This guide covers advanced scenarios and patterns for using SMock effectively in complex testing situations.

## Table of Contents
- [Complex Mock Scenarios](#complex-mock-scenarios)
- [State Management Patterns](#state-management-patterns)
- [Conditional Mocking Strategies](#conditional-mocking-strategies)
- [Mock Composition Patterns](#mock-composition-patterns)
- [Error Handling and Edge Cases](#error-handling-and-edge-cases)
- [Performance Optimization Patterns](#performance-optimization-patterns)

## Complex Mock Scenarios

### Mocking Nested Static Calls

When your code under test makes multiple nested static method calls, you'll need to mock each level:

```csharp
[Test]
public void Mock_Nested_Static_Calls()
{
    // Mock the configuration reading
    using var configMock = Mock.Setup(() =>
        ConfigurationManager.AppSettings["DatabaseProvider"])
        .Returns("SqlServer");

    // Mock the connection string building
    using var connectionMock = Mock.Setup(context =>
        ConnectionStringBuilder.Build(context.It.IsAny<string>()))
        .Returns("Server=localhost;Database=test;");

    // Mock the database connection
    using var dbMock = Mock.Setup(context =>
        DatabaseFactory.CreateConnection(context.It.IsAny<string>()))
        .Returns(new MockDbConnection());

    var service = new DataService();
    var result = service.InitializeDatabase();

    Assert.IsTrue(result.IsConnected);
}
```

### Multi-Mock Coordination

When multiple mocks need to work together in a coordinated way:

```csharp
[Test]
public void Coordinated_Multi_Mock_Pattern()
{
    var userToken = "auth_token_123";
    var userData = new User { Id = 1, Name = "Test User" };

    // Mock authentication
    using var authMock = Mock.Setup(() =>
        AuthenticationService.ValidateToken(userToken))
        .Returns(new AuthResult { IsValid = true, UserId = 1 });

    // Mock user retrieval (depends on auth result)
    using var userMock = Mock.Setup(() =>
        UserRepository.GetById(1))
        .Returns(userData);

    // Mock audit logging
    var auditCalls = new List<string>();
    using var auditMock = Mock.Setup(context =>
        AuditLogger.Log(context.It.IsAny<string>()))
        .Callback<string>(message => auditCalls.Add(message));

    var controller = new UserController();
    var result = controller.GetUserProfile(userToken);

    Assert.AreEqual("Test User", result.Name);
    Assert.Contains("User profile accessed for ID: 1", auditCalls);
}
```

### Dynamic Return Values Based on Call History

Create mocks that behave differently based on previous calls:

```csharp
[Test]
public void Dynamic_Behavior_Based_On_History()
{
    var callHistory = new List<string>();
    var attemptCount = 0;

    using var mock = Mock.Setup(context =>
        ExternalApiClient.Call(context.It.IsAny<string>()))
        .Returns<string>(endpoint =>
        {
            callHistory.Add(endpoint);
            attemptCount++;

            // First two calls fail, third succeeds
            if (attemptCount <= 2)
                throw new HttpRequestException("Service temporarily unavailable");

            return new ApiResponse { Success = true, Data = "Retrieved data" };
        });

    var service = new ResilientApiService();
    var result = service.GetDataWithRetry("/api/data");

    Assert.IsTrue(result.Success);
    Assert.AreEqual(3, callHistory.Count);
    Assert.IsTrue(callHistory.All(call => call == "/api/data"));
}
```

## State Management Patterns

### Mock State Persistence Across Calls

Maintain state between mock calls to simulate stateful operations:

```csharp
[Test]
public void Stateful_Mock_Pattern()
{
    var mockState = new Dictionary<string, object>();

    // Mock cache get operations
    using var getMock = Mock.Setup(context =>
        CacheManager.Get(context.It.IsAny<string>()))
        .Returns<string>(key => mockState.GetValueOrDefault(key));

    // Mock cache set operations
    using var setMock = Mock.Setup(context =>
        CacheManager.Set(context.It.IsAny<string>(), context.It.IsAny<object>()))
        .Callback<string, object>((key, value) => mockState[key] = value);

    var service = new CachedDataService();

    // First call should miss cache and set value
    var result1 = service.GetExpensiveData("key1");
    Assert.IsNotNull(result1);

    // Second call should hit cache
    var result2 = service.GetExpensiveData("key1");
    Assert.AreEqual(result1, result2);

    // Verify state was maintained
    Assert.IsTrue(mockState.ContainsKey("key1"));
}
```

### Session-Based Mock Behavior

Create mocks that behave differently within test sessions:

```csharp
[Test]
public void Session_Based_Mock_Behavior()
{
    var currentSession = new TestSession
    {
        UserId = 123,
        Role = "Administrator",
        SessionStart = DateTime.Now
    };

    using var sessionMock = Mock.Setup(() =>
        SessionManager.GetCurrentSession())
        .Returns(currentSession);

    using var permissionMock = Mock.Setup(context =>
        PermissionChecker.HasPermission(context.It.IsAny<int>(), context.It.IsAny<string>()))
        .Returns<int, string>((userId, permission) =>
        {
            // Permission based on current session
            if (userId != currentSession.UserId) return false;

            return currentSession.Role == "Administrator"
                ? true
                : permission == "Read";
        });

    var service = new SecureDataService();

    // Admin can write
    Assert.IsTrue(service.CanWriteData());

    // Change session role
    currentSession.Role = "User";

    // User can only read
    Assert.IsFalse(service.CanWriteData());
    Assert.IsTrue(service.CanReadData());
}
```

## Conditional Mocking Strategies

### Environment-Based Mocking

Different mock behavior based on environment conditions:

```csharp
[Test]
public void Environment_Conditional_Mocking()
{
    using var environmentMock = Mock.Setup(context =>
        Environment.GetEnvironmentVariable(context.It.IsAny<string>()))
        .Returns<string>(varName => varName switch
        {
            "ENVIRONMENT" => "Development",
            "DEBUG_MODE" => "true",
            "LOG_LEVEL" => "Debug",
            _ => null
        });

    using var loggerMock = Mock.Setup(context =>
        Logger.Log(context.It.IsAny<LogLevel>(), context.It.IsAny<string>()))
        .Callback<LogLevel, string>((level, message) =>
        {
            // Only log debug messages in development
            var env = Environment.GetEnvironmentVariable("ENVIRONMENT");
            if (env == "Development" || level >= LogLevel.Info)
            {
                Console.WriteLine($"[{level}] {message}");
            }
        });

    var service = new EnvironmentAwareService();
    service.DoWork(); // Should log debug messages in development
}
```

### Parameter-Driven Mock Selection

Choose mock behavior based on input parameters:

```csharp
[Test]
public void Parameter_Driven_Mock_Selection()
{
    var responseTemplates = new Dictionary<string, object>
    {
        ["users"] = new[] { new { id = 1, name = "User 1" } },
        ["products"] = new[] { new { id = 1, name = "Product 1", price = 99.99 } },
        ["orders"] = new[] { new { id = 1, userId = 1, total = 99.99 } }
    };

    using var mock = Mock.Setup(context =>
        ApiClient.Get(context.It.IsAny<string>()))
        .Returns<string>(endpoint =>
        {
            var resource = endpoint.Split('/').LastOrDefault();
            return responseTemplates.ContainsKey(resource)
                ? new ApiResponse { Data = responseTemplates[resource] }
                : new ApiResponse { Error = "Not Found" };
        });

    var service = new DataAggregationService();
    var dashboard = service.BuildDashboard();

    Assert.IsNotNull(dashboard.Users);
    Assert.IsNotNull(dashboard.Products);
    Assert.IsNotNull(dashboard.Orders);
}
```

## Mock Composition Patterns

### Hierarchical Mock Chains

Create complex mock chains for hierarchical operations:

```csharp
[Test]
public void Hierarchical_Mock_Chain()
{
    // Mock factory pattern
    var mockConnection = new Mock<IDbConnection>();
    var mockCommand = new Mock<IDbCommand>();
    var mockReader = new Mock<IDataReader>();

    using var factoryMock = Mock.Setup(context =>
        DatabaseFactory.CreateConnection(context.It.IsAny<string>()))
        .Returns(mockConnection.Object);

    using var commandMock = Mock.Setup(() =>
        mockConnection.Object.CreateCommand())
        .Returns(mockCommand.Object);

    using var readerMock = Mock.Setup(() =>
        mockCommand.Object.ExecuteReader())
        .Returns(mockReader.Object);

    // Configure reader behavior
    var hasDataCalls = 0;
    mockReader.Setup(r => r.Read()).Returns(() => hasDataCalls++ < 2);
    mockReader.Setup(r => r["Name"]).Returns("Test Item");
    mockReader.Setup(r => r["Id"]).Returns(1);

    var repository = new DatabaseRepository();
    var items = repository.GetItems();

    Assert.AreEqual(2, items.Count);
    Assert.IsTrue(items.All(item => item.Name == "Test Item"));
}
```

### Composite Mock Patterns

Combine multiple mock types for complex scenarios:

```csharp
[Test]
public void Composite_Mock_Pattern()
{
    var fileSystemState = new Dictionary<string, string>();
    var networkResponses = new Queue<HttpResponseMessage>();

    // Queue up network responses
    networkResponses.Enqueue(new HttpResponseMessage
    {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent("remote_data_v1")
    });

    // Mock file system operations
    using var fileExistsMock = Mock.Setup(context =>
        File.Exists(context.It.IsAny<string>()))
        .Returns<string>(path => fileSystemState.ContainsKey(path));

    using var fileReadMock = Mock.Setup(context =>
        File.ReadAllText(context.It.IsAny<string>()))
        .Returns<string>(path => fileSystemState.GetValueOrDefault(path, ""));

    using var fileWriteMock = Mock.Setup(context =>
        File.WriteAllText(context.It.IsAny<string>(), context.It.IsAny<string>()))
        .Callback<string, string>((path, content) =>
            fileSystemState[path] = content);

    // Mock network operations
    using var httpMock = Mock.Setup(context =>
        HttpClient.GetAsync(context.It.IsAny<string>()))
        .Returns(() => Task.FromResult(networkResponses.Dequeue()));

    var service = new CachingRemoteDataService();

    // First call: network fetch + cache write
    var data1 = await service.GetDataAsync("endpoint1");
    Assert.AreEqual("remote_data_v1", data1);
    Assert.IsTrue(fileSystemState.ContainsKey("cache_endpoint1"));

    // Second call: cache hit
    var data2 = await service.GetDataAsync("endpoint1");
    Assert.AreEqual("remote_data_v1", data2);
}
```

## Error Handling and Edge Cases

### Simulating Intermittent Failures

Test resilience by simulating intermittent failures:

```csharp
[Test]
public void Intermittent_Failure_Simulation()
{
    var callCount = 0;
    var failurePattern = new[] { true, false, true, false, false }; // fail, succeed, fail, succeed, succeed

    using var mock = Mock.Setup(context =>
        UnreliableService.ProcessData(context.It.IsAny<string>()))
        .Returns<string>(data =>
        {
            var shouldFail = callCount < failurePattern.Length && failurePattern[callCount];
            callCount++;

            if (shouldFail)
                throw new ServiceUnavailableException("Temporary failure");

            return $"Processed: {data}";
        });

    var resilientService = new ResilientProcessorService();
    var result = resilientService.ProcessWithRetry("test_data", maxRetries: 5);

    Assert.AreEqual("Processed: test_data", result);
    Assert.AreEqual(4, callCount); // Should take 4 attempts (fail, succeed, fail, succeed)
}
```

### Exception Chain Testing

Test complex exception handling scenarios:

```csharp
[Test]
public void Exception_Chain_Testing()
{
    var exceptions = new Queue<Exception>(new[]
    {
        new TimeoutException("Request timeout"),
        new HttpRequestException("Network error"),
        new InvalidOperationException("Invalid state")
    });

    using var mock = Mock.Setup(context =>
        ExternalService.Execute(context.It.IsAny<string>()))
        .Returns<string>(operation =>
        {
            if (exceptions.Count > 0)
                throw exceptions.Dequeue();

            return "Success";
        });

    var service = new FaultTolerantService();
    var result = service.ExecuteWithFallbacks("test_operation");

    // Should eventually succeed after handling all exceptions
    Assert.AreEqual("Success", result.Value);
    Assert.AreEqual(3, result.AttemptCount);
    Assert.AreEqual(0, exceptions.Count);
}
```

## Performance Optimization Patterns

### Lazy Mock Initialization

Optimize performance by initializing mocks only when needed:

```csharp
[Test]
public void Lazy_Mock_Initialization()
{
    var expensiveOperationCalled = false;
    Lazy<IDisposable> expensiveMock = null;

    expensiveMock = new Lazy<IDisposable>(() =>
    {
        expensiveOperationCalled = true;
        return Mock.Setup(context => ExpensiveExternalService.Process(context.It.IsAny<string>()))
            .Returns("mocked_result");
    });

    var service = new ConditionalService();

    // Mock not initialized if condition not met
    var result1 = service.ProcessData("simple_data");
    Assert.IsFalse(expensiveOperationCalled);

    // Mock initialized only when needed
    var result2 = service.ProcessData("complex_data");
    Assert.IsTrue(expensiveOperationCalled);

    expensiveMock.Value.Dispose();
}
```

### Mock Pooling for Repeated Tests

Reuse mock configurations across multiple test methods:

```csharp
public class MockPoolTests
{
    private static readonly ConcurrentDictionary<string, Func<IDisposable>> MockPool =
        new ConcurrentDictionary<string, Func<IDisposable>>();

    static MockPoolTests()
    {
        // Pre-configure common mocks
        MockPool["datetime_fixed"] = () => Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        MockPool["file_exists_true"] = () => Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
            .Returns(true);
    }

    [Test]
    public void Test_With_Pooled_Mocks()
    {
        using var dateMock = MockPool["datetime_fixed"]();
        using var fileMock = MockPool["file_exists_true"]();

        var service = new TimeAwareFileService();
        var result = service.ProcessTodaysFiles();

        Assert.IsNotNull(result);
    }
}
```

## Best Practices Summary

### Do's
- ✅ Use meaningful mock return values that reflect real scenarios
- ✅ Group related mocks together for better test organization
- ✅ Use parameter matching (`It.IsAny<T>()`, `It.Is<T>()`) for flexible mocks
- ✅ Implement proper cleanup with `using` statements (Sequential API)
- ✅ Test edge cases and failure scenarios
- ✅ Use callbacks for side-effect verification

### Don'ts
- ❌ Don't create mocks you don't use in the test
- ❌ Don't use overly specific parameter matching unless necessary
- ❌ Don't ignore mock cleanup, especially in integration tests
- ❌ Don't mock everything - focus on external dependencies
- ❌ Don't create complex mock hierarchies when simple ones suffice

### Performance Tips
- 🚀 Initialize mocks lazily when possible
- 🚀 Reuse mock configurations for similar test scenarios
- 🚀 Prefer Sequential API for automatic cleanup
- 🚀 Use parameter matching efficiently to avoid over-specification
- 🚀 Group related assertions to minimize mock overhead

This advanced patterns guide should help you handle complex testing scenarios effectively with SMock.

## See Also

### Related Guides
- **[Real-World Examples & Case Studies](real-world-examples.md)** - See these patterns applied in enterprise scenarios
- **[Performance Guide](performance-guide.md)** - Optimize the advanced patterns for better performance
- **[Testing Framework Integration](framework-integration.md)** - Integrate these patterns with your test framework

### Getting Help
- **[Troubleshooting & FAQ](troubleshooting.md)** - Solutions for issues with complex patterns
- **[Migration Guide](migration-guide.md)** - Migrate existing complex test setups
- **[Getting Started Guide](getting-started.md)** - Review the basics if needed

### Community Resources
- **[GitHub Issues](https://github.com/SvetlovA/static-mock/issues)** - Report advanced pattern bugs
- **[GitHub Discussions](https://github.com/SvetlovA/static-mock/discussions)** - Share your advanced patterns

## Working Advanced Pattern Examples

The advanced patterns shown in this guide are based on actual working test cases. You can find complete, debugged examples in the SMock test suite:

- **[Complex Mock Scenarios](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/AdvancedPatterns/ComplexMockScenarios.cs)** - `src/StaticMock.Tests/Tests/Examples/AdvancedPatterns/ComplexMockScenarios.cs`

These examples demonstrate:
- **Nested static calls** - Coordinating multiple mocks for complex scenarios
- **State management patterns** - Maintaining state across mock calls
- **Dynamic behavior** - Mocks that change behavior based on call history
- **Conditional mocking** - Different behaviors based on parameters or environment
- **Performance optimization** - Efficient patterns for complex test scenarios

### Running Advanced Pattern Examples

```bash
# Navigate to the src directory
cd src

# Run the advanced pattern examples specifically
dotnet test --filter "ClassName=ComplexMockScenarios"

# Or run all example tests
dotnet test --filter "FullyQualifiedName~Examples"
```