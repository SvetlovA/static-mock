# Testing Framework Integration Guide

This guide provides detailed integration instructions and best practices for using SMock with popular .NET testing frameworks.

## Table of Contents
- [Framework Compatibility](#framework-compatibility)
- [NUnit Integration](#nunit-integration)
- [xUnit Integration](#xunit-integration)
- [MSTest Integration](#mstest-integration)
- [SpecFlow Integration](#specflow-integration)
- [Custom Test Frameworks](#custom-test-frameworks)
- [CI/CD Integration](#cicd-integration)

## Framework Compatibility

SMock works seamlessly with all major .NET testing frameworks. Here's the compatibility matrix:

| Framework | Version | Support Level | Special Features |
|-----------|---------|---------------|------------------|
| **NUnit** | 3.0+ | ✅ Full | Parallel execution, custom attributes |
| **xUnit** | 2.0+ | ✅ Full | Fact/Theory patterns, collection fixtures |
| **MSTest** | 2.0+ | ✅ Full | DataRow testing, deployment items |
| **SpecFlow** | 3.0+ | ✅ Full | BDD scenarios, step definitions |
| **Fixie** | 2.0+ | ✅ Full | Convention-based testing |
| **Expecto** | 9.0+ | ✅ Full | F# functional testing |

## NUnit Integration

### Basic Setup

```csharp
using NUnit.Framework;
using StaticMock;

[TestFixture]
public class NUnitSMockTests
{~~~~~~~~
    [Test]
    public void Basic_SMock_Test()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var result = DateTime.Now;
        Assert.AreEqual(new DateTime(2024, 1, 1), result);
    }
}
```

### NUnit Parameterized Tests

```csharp
[TestFixture]
public class ParameterizedNUnitTests
{
    [Test]
    [TestCase("file1.txt", "content1")]
    [TestCase("file2.txt", "content2")]
    [TestCase("file3.txt", "content3")]
    public void Parameterized_File_Mock_Test(string fileName, string expectedContent)
    {
        using var mock = Mock.Setup(() => File.ReadAllText(fileName))
            .Returns(expectedContent);

        var processor = new FileProcessor();
        var result = processor.ProcessFile(fileName);

        Assert.AreEqual(expectedContent.ToUpper(), result);
    }

    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void TestCaseSource_Example(TestData data)
    {
        using var mock = Mock.Setup(() => TestService.GetValue(data.Input))
            .Returns(data.ExpectedOutput);

        var result = TestService.GetValue(data.Input);
        Assert.AreEqual(data.ExpectedOutput, result);
    }

    private static IEnumerable<TestData> GetTestData()
    {
        yield return new TestData { Input = "test1", ExpectedOutput = "result1" };
        yield return new TestData { Input = "test2", ExpectedOutput = "result2" };
        yield return new TestData { Input = "test3", ExpectedOutput = "result3" };
    }

    public class TestData
    {
        public string Input { get; set; }
        public string ExpectedOutput { get; set; }
    }
}
```

### NUnit Parallel Execution

```csharp
[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ParallelNUnitTests
{
    [Test]
    [Parallelizable]
    public void Parallel_Test_1()
    {
        using var mock = Mock.Setup(() => TestClass.Method("parallel_1"))
            .Returns("result_1");

        var result = TestClass.Method("parallel_1");
        Assert.AreEqual("result_1", result);
    }

    [Test]
    [Parallelizable]
    public void Parallel_Test_2()
    {
        using var mock = Mock.Setup(() => TestClass.Method("parallel_2"))
            .Returns("result_2");

        var result = TestClass.Method("parallel_2");
        Assert.AreEqual("result_2", result);
    }
}
```

### NUnit One-Time Setup with SMock

```csharp
[TestFixture]
public class OneTimeSetupTests
{
    private static IDisposable _globalDateMock;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        // Setup mocks that persist across all tests in this fixture
        _globalDateMock = Mock.Setup(() => Environment.MachineName)
            .Returns("TEST_MACHINE");
    }

    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        // Clean up global mocks
        _globalDateMock?.Dispose();
    }

    [Test]
    public void Test_With_Global_Mock_1()
    {
        // This test uses the globally set up mock
        var machineName = Environment.MachineName;
        Assert.AreEqual("TEST_MACHINE", machineName);

        // Add test-specific mocks
        using var dateMock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var service = new MachineAwareService();
        var result = service.GetMachineTimeStamp();

        Assert.IsNotNull(result);
    }

    [Test]
    public void Test_With_Global_Mock_2()
    {
        // This test also benefits from the global mock
        var machineName = Environment.MachineName;
        Assert.AreEqual("TEST_MACHINE", machineName);
    }
}
```

## xUnit Integration

### Basic xUnit Setup

```csharp
using Xunit;
using StaticMock;

public class XUnitSMockTests
{
    [Fact]
    public void Basic_SMock_Fact()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var result = DateTime.Now;
        Assert.Equal(new DateTime(2024, 1, 1), result);
    }

    [Theory]
    [InlineData("input1", "output1")]
    [InlineData("input2", "output2")]
    [InlineData("input3", "output3")]
    public void Theory_With_SMock(string input, string expectedOutput)
    {
        using var mock = Mock.Setup(() => TestService.Transform(input))
            .Returns(expectedOutput);

        var result = TestService.Transform(input);
        Assert.Equal(expectedOutput, result);
    }
}
```

### xUnit Collection Fixtures

```csharp
// Collection fixture for shared mocks across test classes
public class SharedMockFixture : IDisposable
{
    public IDisposable DateTimeMock { get; private set; }
    public IDisposable EnvironmentMock { get; private set; }

    public SharedMockFixture()
    {
        DateTimeMock = Mock.Setup(() => DateTime.UtcNow)
            .Returns(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));

        EnvironmentMock = Mock.Setup(() => Environment.UserName)
            .Returns("TEST_USER");
    }

    public void Dispose()
    {
        DateTimeMock?.Dispose();
        EnvironmentMock?.Dispose();
    }
}

[CollectionDefinition("Shared Mock Collection")]
public class SharedMockCollection : ICollectionFixture<SharedMockFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("Shared Mock Collection")]
public class FirstTestClass
{
    private readonly SharedMockFixture _fixture;

    public FirstTestClass(SharedMockFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Test_Using_Shared_Mocks()
    {
        // Shared mocks are available through the fixture
        var currentTime = DateTime.UtcNow;
        var userName = Environment.UserName;

        Assert.Equal(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), currentTime);
        Assert.Equal("TEST_USER", userName);
    }
}

[Collection("Shared Mock Collection")]
public class SecondTestClass
{
    private readonly SharedMockFixture _fixture;

    public SecondTestClass(SharedMockFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Another_Test_Using_Shared_Mocks()
    {
        var userName = Environment.UserName;
        Assert.Equal("TEST_USER", userName);
    }
}
```

### xUnit Async Testing

```csharp
public class AsyncXUnitTests
{
    [Fact]
    public async Task Async_SMock_Test()
    {
        using var mock = Mock.Setup(context => HttpClient.GetStringAsync(context.It.IsAny<string>()))
            .Returns(Task.FromResult("{\"status\": \"success\"}"));

        var httpService = new HttpService();
        var result = await httpService.FetchDataAsync("https://api.example.com/data");

        Assert.Contains("success", result);
    }

    [Theory]
    [InlineData("endpoint1", "data1")]
    [InlineData("endpoint2", "data2")]
    public async Task Async_Theory_Test(string endpoint, string expectedData)
    {
        using var mock = Mock.Setup(() => ApiClient.GetAsync(endpoint))
            .Returns(Task.FromResult(new ApiResponse { Data = expectedData }));

        var service = new ApiService();
        var result = await service.GetDataAsync(endpoint);

        Assert.Equal(expectedData, result.Data);
    }
}
```

## MSTest Integration

### Basic MSTest Setup

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticMock;

[TestClass]
public class MSTestSMockTests
{
    [TestInitialize]
    public void Initialize()
    {
        // Optional test initialization
        Console.WriteLine("MSTest starting with SMock");
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Optional test cleanup
        Console.WriteLine("MSTest completed");
    }

    [TestMethod]
    public void Basic_SMock_Test()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var result = DateTime.Now;
        Assert.AreEqual(new DateTime(2024, 1, 1), result);
    }
}
```

### MSTest Data-Driven Tests

```csharp
[TestClass]
public class DataDrivenMSTests
{
    [TestMethod]
    [DataRow("test1", "result1")]
    [DataRow("test2", "result2")]
    [DataRow("test3", "result3")]
    public void DataRow_SMock_Test(string input, string expectedOutput)
    {
        using var mock = Mock.Setup(() => DataProcessor.Process(input))
            .Returns(expectedOutput);

        var result = DataProcessor.Process(input);
        Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
    public void DynamicData_SMock_Test(string input, string expectedOutput)
    {
        using var mock = Mock.Setup(() => DataProcessor.Process(input))
            .Returns(expectedOutput);

        var result = DataProcessor.Process(input);
        Assert.AreEqual(expectedOutput, result);
    }

    public static IEnumerable<object[]> GetTestData()
    {
        return new[]
        {
            new object[] { "dynamic1", "result1" },
            new object[] { "dynamic2", "result2" },
            new object[] { "dynamic3", "result3" }
        };
    }
}
```

### MSTest Class Initialize/Cleanup

```csharp
[TestClass]
public class ClassLevelMSTests
{
    private static IDisposable _classLevelMock;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // Setup mocks for the entire test class
        _classLevelMock = Mock.Setup(() => ConfigurationManager.AppSettings["TestMode"])
            .Returns("true");

        Console.WriteLine("Class-level mock initialized");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        // Clean up class-level mocks
        _classLevelMock?.Dispose();
        Console.WriteLine("Class-level mock disposed");
    }

    [TestMethod]
    public void Test_With_Class_Mock_1()
    {
        var testMode = ConfigurationManager.AppSettings["TestMode"];
        Assert.AreEqual("true", testMode);

        // Add method-specific mocks
        using var dateMock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var service = new ConfigurableService();
        var result = service.GetConfiguredValue();

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Test_With_Class_Mock_2()
    {
        var testMode = ConfigurationManager.AppSettings["TestMode"];
        Assert.AreEqual("true", testMode);
    }
}
```

## SpecFlow Integration

### SpecFlow Step Definitions with SMock

```csharp
// Feature file (Example.feature)
/*
Feature: File Processing with SMock
    In order to test file processing
    As a developer
    I want to mock file system calls

Scenario: Process existing file
    Given a file "test.txt" exists with content "Hello World"
    When I process the file "test.txt"
    Then the result should be "HELLO WORLD"

Scenario Outline: Process multiple files
    Given a file "<filename>" exists with content "<content>"
    When I process the file "<filename>"
    Then the result should be "<expected>"

Examples:
    | filename | content     | expected    |
    | file1.txt| hello       | HELLO       |
    | file2.txt| world       | WORLD       |
*/

[Binding]
public class FileProcessingSteps
{
    private readonly Dictionary<string, IDisposable> _activeMocks = new();
    private string _result;

    [Given(@"a file ""([^""]*)"" exists with content ""([^""]*)""")]
    public void GivenAFileExistsWithContent(string filename, string content)
    {
        // Setup file existence mock
        var existsMock = Mock.Setup(() => File.Exists(filename))
            .Returns(true);
        _activeMocks[$"exists_{filename}"] = existsMock;

        // Setup file read mock
        var readMock = Mock.Setup(() => File.ReadAllText(filename))
            .Returns(content);
        _activeMocks[$"read_{filename}"] = readMock;
    }

    [When(@"I process the file ""([^""]*)""")]
    public void WhenIProcessTheFile(string filename)
    {
        var processor = new FileProcessor();
        _result = processor.ProcessFile(filename);
    }

    [Then(@"the result should be ""([^""]*)""")]
    public void ThenTheResultShouldBe(string expectedResult)
    {
        Assert.AreEqual(expectedResult, _result);
    }

    [AfterScenario]
    public void CleanupMocks()
    {
        foreach (var mock in _activeMocks.Values)
        {
            mock?.Dispose();
        }
        _activeMocks.Clear();
    }
}
```

### SpecFlow Hooks with SMock

```csharp
[Binding]
public class SMockHooks
{
    private static IDisposable _globalMock;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        // Setup global mocks for the entire test run
        _globalMock = Mock.Setup(() => Environment.GetEnvironmentVariable("TEST_ENVIRONMENT"))
            .Returns("SpecFlow");
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        // Cleanup global mocks
        _globalMock?.Dispose();
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        Console.WriteLine($"Starting feature: {featureContext.FeatureInfo.Title} with SMock support");
    }

    [BeforeScenario]
    public void BeforeScenario(ScenarioContext scenarioContext)
    {
        Console.WriteLine($"Starting scenario: {scenarioContext.ScenarioInfo.Title}");
    }

    [AfterScenario]
    public void AfterScenario(ScenarioContext scenarioContext)
    {
        Console.WriteLine($"Completed scenario: {scenarioContext.ScenarioInfo.Title}");
    }
}
```

## Custom Test Frameworks

### Generic Integration Pattern

For custom or less common test frameworks, follow this general pattern:

```csharp
public abstract class SMockTestBase
{
    private readonly List<IDisposable> _testMocks = new();

    protected IDisposable CreateMock<T>(Expression<Func<T>> expression, T returnValue)
    {
        var mock = Mock.Setup(expression).Returns(returnValue);
        _testMocks.Add(mock);
        return mock;
    }

    protected IDisposable CreateMock<T>(Expression<Action> expression)
    {
        var mock = Mock.Setup(expression);
        _testMocks.Add(mock);
        return mock;
    }

    // Call this in your test framework's cleanup method
    protected virtual void CleanupMocks()
    {
        _testMocks.ForEach(mock => mock?.Dispose());
        _testMocks.Clear();
    }

    // Call this in your test framework's setup method
    protected virtual void InitializeTest()
    {
        Console.WriteLine("SMock test initialized");
    }
}

// Example usage with a custom framework
public class CustomFrameworkTest : SMockTestBase
{
    [CustomTestMethod] // Your framework's test attribute
    public void MyCustomTest()
    {
        // Initialize if needed
        InitializeTest();

        try
        {
            // Create mocks using the helper methods
            CreateMock(() => DateTime.Now, new DateTime(2024, 1, 1));
            CreateMock(context => Console.WriteLine(context.It.IsAny<string>()));

            // Your test logic here
            var result = DateTime.Now;
            Assert.Equal(new DateTime(2024, 1, 1), result);
        }
        finally
        {
            // Ensure cleanup
            CleanupMocks();
        }
    }
}
```

### Framework-Agnostic Mock Manager

```csharp
public class FrameworkAgnosticMockManager : IDisposable
{
    private readonly List<IDisposable> _mocks = new();
    private readonly Dictionary<string, object> _mockResults = new();

    public IDisposable SetupMock<T>(Expression<Func<T>> expression, T returnValue, string key = null)
    {
        var mock = Mock.Setup(expression).Returns(returnValue);
        _mocks.Add(mock);

        if (key != null)
        {
            _mockResults[key] = returnValue;
        }

        return mock;
    }

    public IDisposable SetupMockWithCallback<T>(Expression<Func<T>> expression, T returnValue, Action<T> callback)
    {
        var mock = Mock.Setup(expression)
            .Callback(() => callback(returnValue))
            .Returns(returnValue);

        _mocks.Add(mock);
        return mock;
    }

    public T GetMockResult<T>(string key)
    {
        return _mockResults.ContainsKey(key) ? (T)_mockResults[key] : default(T);
    }

    public void Dispose()
    {
        _mocks.ForEach(mock => mock?.Dispose());
        _mocks.Clear();
        _mockResults.Clear();
    }
}

// Usage example
public class AnyFrameworkTest
{
    private FrameworkAgnosticMockManager _mockManager;

    // Call in your framework's setup method
    public void Setup()
    {
        _mockManager = new FrameworkAgnosticMockManager();
    }

    // Call in your framework's teardown method
    public void Teardown()
    {
        _mockManager?.Dispose();
    }

    // Your test method
    public void TestMethod()
    {
        _mockManager.SetupMock(() => DateTime.Now, new DateTime(2024, 1, 1), "test_date");

        var result = DateTime.Now;
        var expectedDate = _mockManager.GetMockResult<DateTime>("test_date");

        // Use your framework's assertion method
        AssertEqual(expectedDate, result);
    }

    private void AssertEqual<T>(T expected, T actual)
    {
        // Your framework's assertion implementation
        if (!EqualityComparer<T>.Default.Equals(expected, actual))
        {
            throw new Exception($"Expected {expected}, but got {actual}");
        }
    }
}
```

## CI/CD Integration

### Azure DevOps Pipeline

```yaml
# azure-pipelines.yml
trigger:
- master
- develop

pool:
  vmImage: 'windows-latest'

variables:
  buildConfiguration: 'Release'
  solution: '**/*.sln'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

- task: VSBuild@1
  inputs:
    solution: '$(solution)'
    platform: 'Any CPU'
    configuration: '$(buildConfiguration)'

- task: VSTest@2
  inputs:
    platform: 'Any CPU'
    configuration: '$(buildConfiguration)'
    testSelector: 'testAssemblies'
    testAssemblyVer2: |
      **\*Tests.dll
      !**\*TestAdapter.dll
      !**\obj\**
    searchFolder: '$(System.DefaultWorkingDirectory)'
    runInParallel: true
    codeCoverageEnabled: true
    testRunTitle: 'SMock Integration Tests'
  displayName: 'Run SMock Tests'

- task: PublishTestResults@2
  condition: succeededOrFailed()
  inputs:
    testRunner: VSTest
    testResultsFiles: '**/*.trx'
    buildPlatform: 'Any CPU'
    buildConfiguration: '$(buildConfiguration)'
```

### GitHub Actions

```yaml
# .github/workflows/test.yml
name: SMock Tests

on:
  push:
    branches: [ master, develop ]
  pull_request:
    branches: [ master ]

jobs:
  test:
    runs-on: ${{ matrix.os }}
    strategy:
      matrix:
        os: [ubuntu-latest, windows-latest, macos-latest]
        dotnet-version: ['6.0.x', '7.0.x', '8.0.x']

    steps:
    - uses: actions/checkout@v3

    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: ${{ matrix.dotnet-version }}

    - name: Restore dependencies
      run: dotnet restore

    - name: Build
      run: dotnet build --no-restore --configuration Release

    - name: Test
      run: dotnet test --no-build --configuration Release --verbosity normal --collect:"XPlat Code Coverage" --logger trx --results-directory ./TestResults

    - name: Upload test results
      uses: actions/upload-artifact@v3
      if: always()
      with:
        name: test-results-${{ matrix.os }}-${{ matrix.dotnet-version }}
        path: ./TestResults

    - name: Upload coverage reports to Codecov
      uses: codecov/codecov-action@v3
      with:
        files: ./TestResults/**/coverage.cobertura.xml
        fail_ci_if_error: true
```

### Jenkins Pipeline

```groovy
pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                bat '''
                    dotnet test --configuration Release --no-build --verbosity normal ^
                    --collect:"XPlat Code Coverage" ^
                    --logger "trx;LogFileName=TestResults.trx" ^
                    --results-directory ./TestResults
                '''
            }
            post {
                always {
                    // Publish test results
                    publishTestResults testResultsPattern: 'TestResults/**/*.trx'

                    // Publish coverage report
                    publishCoverage adapters: [
                        istanbulCoberturaAdapter('TestResults/**/coverage.cobertura.xml')
                    ], sourceFileResolver: sourceFiles('STORE_LAST_BUILD')
                }
            }
        }
    }

    post {
        always {
            cleanWs()
        }
    }
}
```

### Docker Integration

```dockerfile
# Test.Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy project files
COPY *.sln ./
COPY src/ src/
COPY tests/ tests/

# Restore dependencies
RUN dotnet restore

# Build
RUN dotnet build --configuration Release --no-restore

# Run tests
RUN dotnet test --configuration Release --no-build --verbosity normal \
    --collect:"XPlat Code Coverage" \
    --logger "trx;LogFileName=TestResults.trx" \
    --results-directory ./TestResults

# Create test results image
FROM scratch AS test-results
COPY --from=build /app/TestResults ./TestResults
```

## Best Practices Summary

### Framework-Specific Recommendations

1. **NUnit**: Use `[OneTimeSetUp]` for expensive mock setup, leverage parallel execution
2. **xUnit**: Use collection fixtures for shared mocks, prefer constructor injection
3. **MSTest**: Use `[ClassInitialize]` for class-level mocks, leverage data-driven tests
4. **SpecFlow**: Use hooks for mock lifecycle management, keep step definitions clean

### General Integration Guidelines

- **Isolation**: Ensure each test has proper mock cleanup
- **Performance**: Reuse expensive mock setups when appropriate
- **Debugging**: Add diagnostic logging for complex mock scenarios
- **CI/CD**: Include performance regression tests in your pipeline
- **Documentation**: Document mock setup patterns for your team

This integration guide should help you effectively use SMock with any .NET testing framework while following best practices for maintainable and reliable tests.