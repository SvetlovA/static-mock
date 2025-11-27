# Migration Guide

This guide helps you migrate between different versions of SMock and provides guidance for upgrading from other mocking frameworks.

## Table of Contents
- [Version Migration](#version-migration)
- [Breaking Changes](#breaking-changes)
- [Upgrading from Other Mocking Frameworks](#upgrading-from-other-mocking-frameworks)
- [Common Migration Issues](#common-migration-issues)
- [Migration Tools and Scripts](#migration-tools-and-scripts)

## Version Migration

### Upgrading to Latest Version

When upgrading SMock, always check the [release notes](https://github.com/SvetlovA/static-mock/releases) for breaking changes.

#### Package Update Commands

```powershell
# Package Manager Console
Update-Package SMock

# .NET CLI
dotnet add package SMock --version [latest-version]

# Check current version
dotnet list package SMock
```

### Version Compatibility Matrix

| SMock Version | .NET Framework | .NET Standard | .NET Core/.NET | MonoMod Version |
|---------------|----------------|---------------|----------------|-----------------|
| 1.0.x         | 4.62+          | 2.0+          | 2.0+           | RuntimeDetour   |
| 1.1.x         | 4.62+          | 2.0+          | 3.1+           | RuntimeDetour   |
| 1.2.x         | 4.62+          | 2.0+          | 5.0+           | Core            |
| 2.0.x         | 4.62+          | 2.0+          | 6.0+           | Core            |

## Breaking Changes

### Version 2.0 Breaking Changes

#### Namespace Changes
```csharp
// Old (v1.x)
using StaticMock.Core;
using StaticMock.Extensions;

// New (v2.0+)
using StaticMock;
```

#### API Method Renaming
```csharp
// Old (v1.x)
Mock.SetupStatic(() => DateTime.Now).Returns(testDate);

// New (v2.0+)
Mock.Setup(() => DateTime.Now).Returns(testDate);
```

#### Configuration Changes
```csharp
// Old (v1.x)
MockConfiguration.Configure(options =>
{
    options.EnableDebugMode = true;
    options.ThrowOnSetupFailure = false;
});

// New (v2.0+) - Configuration is now automatic
// No manual configuration needed
```

### Version 1.2 Breaking Changes

#### Parameter Matching Updates
```csharp
// Old (v1.1)
Mock.Setup(() => MyClass.Method(Any<string>())).Returns("result");

// New (v1.2+)
Mock.Setup(() => MyClass.Method(It.IsAny<string>())).Returns("result");
```

#### Async Method Handling
```csharp
// Old (v1.1) - Limited async support
Mock.Setup(() => MyClass.AsyncMethod()).ReturnsAsync("result");

// New (v1.2+) - Full async support
Mock.Setup(() => MyClass.AsyncMethod()).Returns(Task.FromResult("result"));
```

## Upgrading from Other Mocking Frameworks

### From Moq

SMock can complement Moq for static method scenarios. Here's how to migrate common patterns:

#### Basic Mocking
```csharp
// Moq (interface/virtual methods only)
var mock = new Mock<IFileService>();
mock.Setup(x => x.ReadFile("test.txt")).Returns("content");

// SMock (static methods)
using var mock = Mock.Setup(() => File.ReadAllText("test.txt"))
    .Returns("content");
```

#### Parameter Matching
```csharp
// Moq
mock.Setup(x => x.Process(It.IsAny<string>())).Returns("result");

// SMock
using var mock = Mock.Setup(() => MyClass.Process(It.IsAny<string>()))
    .Returns("result");
```

#### Callback Verification
```csharp
// Moq
var callCount = 0;
mock.Setup(x => x.Log(It.IsAny<string>()))
    .Callback<string>(msg => callCount++);

// SMock
var callCount = 0;
using var mock = Mock.Setup(() => Logger.Log(It.IsAny<string>()))
    .Callback<string>(msg => callCount++);
```

#### Exception Throwing
```csharp
// Moq
mock.Setup(x => x.Connect()).Throws<ConnectionException>();

// SMock
using var mock = Mock.Setup(() => DatabaseHelper.Connect())
    .Throws<ConnectionException>();
```

### From NSubstitute

```csharp
// NSubstitute (interfaces only)
var service = Substitute.For<IDataService>();
service.GetData("key").Returns("value");

// SMock (static methods)
using var mock = Mock.Setup(() => StaticDataService.GetData("key"))
    .Returns("value");

// NSubstitute - Parameter matching
service.GetData(Arg.Any<string>()).Returns("value");

// SMock - Parameter matching
using var mock = Mock.Setup(() => StaticDataService.GetData(It.IsAny<string>()))
    .Returns("value");
```

### From Microsoft Fakes (Shims)

Microsoft Fakes Shims are similar to SMock but with different syntax:

```csharp
// Microsoft Fakes Shims
[TestMethod]
public void TestWithShims()
{
    using (ShimsContext.Create())
    {
        System.IO.Fakes.ShimFile.ReadAllTextString = (path) => "mocked content";

        // Test code here
    }
}

// SMock equivalent
[Test]
public void TestWithSMock()
{
    using var mock = Mock.Setup(() => File.ReadAllText(It.IsAny<string>()))
        .Returns("mocked content");

    // Test code here
}
```

#### Key Differences:
- **SMock** uses familiar lambda syntax like other modern mocking frameworks
- **SMock** supports both sequential and hierarchical APIs
- **SMock** has built-in parameter matching with `It` class
- **SMock** works with any test framework, not just MSTest

## Common Migration Issues

### Issue 1: Assembly Loading Problems

**Problem**: After upgrading, you get `FileNotFoundException` for MonoMod assemblies.

**Solution**: Clean and restore your project:
```bash
dotnet clean
dotnet restore
dotnet build
```

**Advanced Solution**: If the issue persists, add explicit MonoMod references:
```xml
<PackageReference Include="MonoMod.Core" Version="[version]" />
<PackageReference Include="MonoMod.RuntimeDetour" Version="[version]" />
```

### Issue 2: Mock Setup Not Working After Upgrade

**Problem**: Existing mock setups stop working after version upgrade.

**Diagnosis**:
```csharp
// Check if the method signature matches exactly
Mock.Setup(() => MyClass.Method(It.IsAny<string>()))
    .Returns("test");

// Verify in your actual call
var result = MyClass.Method("actual_parameter"); // Must match parameter types
```

**Solution**: Use parameter matching consistently:
```csharp
// Instead of exact matching
Mock.Setup(() => MyClass.Method("specific_value")).Returns("result");

// Use flexible matching
Mock.Setup(() => MyClass.Method(It.IsAny<string>())).Returns("result");
```

### Issue 3: Performance Degradation After Upgrade

**Problem**: Tests run slower after upgrading SMock.

**Solution**: Review mock disposal patterns:
```csharp
// Ensure proper disposal (Sequential API)
[Test]
public void TestMethod()
{
    using var mock1 = Mock.Setup(() => Service1.Method()).Returns("result1");
    using var mock2 = Mock.Setup(() => Service2.Method()).Returns("result2");

    // Test logic
} // Mocks automatically disposed

// Or use Hierarchical API for automatic cleanup
[Test]
public void TestMethod()
{
    Mock.Setup(() => Service1.Method(), () => {
        // Validation logic
    }).Returns("result1");

    // No explicit disposal needed
}
```

### Issue 4: Compilation Errors with Generic Methods

**Problem**: Generic method mocking fails after upgrade.

```csharp
// This might fail after upgrade
Mock.Setup(() => GenericService.Process<string>(It.IsAny<string>()))
    .Returns("result");
```

**Solution**: Use explicit generic type specification:
```csharp
// Specify generic types explicitly
Mock.Setup(() => GenericService.Process(It.IsAny<string>()))
    .Returns("result");

// Or use non-generic overloads when available
Mock.Setup(() => GenericService.ProcessString(It.IsAny<string>()))
    .Returns("result");
```

## Migration Tools and Scripts

### Automated Migration Script

Here's a PowerShell script to help with common migration tasks:

```powershell
# SMock-Migration.ps1
param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectPath,

    [Parameter(Mandatory=$false)]
    [string]$FromVersion = "1.x",

    [Parameter(Mandatory=$false)]
    [string]$ToVersion = "2.x"
)

function Update-SMockUsages {
    param([string]$FilePath)

    $content = Get-Content $FilePath -Raw

    # Update namespace imports
    $content = $content -replace 'using StaticMock\.Core;', 'using StaticMock;'
    $content = $content -replace 'using StaticMock\.Extensions;', 'using StaticMock;'

    # Update method calls
    $content = $content -replace 'Mock\.SetupStatic', 'Mock.Setup'
    $content = $content -replace 'Any<([^>]+)>', 'It.IsAny<$1>'

    Set-Content $FilePath $content
    Write-Host "Updated: $FilePath"
}

# Find all C# files in the project
Get-ChildItem -Path $ProjectPath -Recurse -Include "*.cs" | ForEach-Object {
    Update-SMockUsages $_.FullName
}

Write-Host "Migration complete. Please review changes and test thoroughly."
```

### Version-Specific Migration Helpers

#### v1.x to v2.0 Migration Checklist

- [ ] Update package reference to SMock 2.0+
- [ ] Update namespace imports (`using StaticMock;`)
- [ ] Replace `Mock.SetupStatic` with `Mock.Setup`
- [ ] Update parameter matching (`Any<T>` → `It.IsAny<T>`)
- [ ] Remove manual configuration code
- [ ] Test all mock setups
- [ ] Verify test execution

#### v1.1 to v1.2 Migration Checklist

- [ ] Update async method mocking patterns
- [ ] Replace `Any<T>` with `It.IsAny<T>`
- [ ] Update generic method mocking syntax
- [ ] Test parameter matching thoroughly

### Migration Validation

After migration, use this test to validate SMock is working correctly:

```csharp
[TestFixture]
public class SMockMigrationValidation
{
    [Test]
    public void Validate_Basic_Static_Mocking()
    {
        using var mock = Mock.Setup(() => DateTime.Now)
            .Returns(new DateTime(2024, 1, 1));

        var result = DateTime.Now;
        ClassicAssert.AreEqual(new DateTime(2024, 1, 1), result);
    }

    [Test]
    public void Validate_Parameter_Matching()
    {
        using var mock = Mock.Setup(() => File.ReadAllText(It.IsAny<string>()))
            .Returns("test content");

        var result = File.ReadAllText("any-file.txt");
        ClassicAssert.AreEqual("test content", result);
    }

    [Test]
    public async Task Validate_Async_Mocking()
    {
        using var mock = Mock.Setup(() => Task.Delay(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        await Task.Delay(1000); // Should complete immediately
        ClassicAssert.Pass("Async mocking works correctly");
    }

    [Test]
    public void Validate_Callback_Functionality()
    {
        var callbackExecuted = false;

        using var mock = Mock.Setup(() => Console.WriteLine(It.IsAny<string>()))
            .Callback<string>(_ => callbackExecuted = true);

        Console.WriteLine("test");
        ClassicAssert.IsTrue(callbackExecuted);
    }
}
```

## Getting Help with Migration

If you encounter issues during migration:

1. **Check Release Notes**: Review the specific version's release notes for known issues
2. **Search Issues**: Check [GitHub Issues](https://github.com/SvetlovA/static-mock/issues) for similar problems
3. **Community Support**: Ask in [GitHub Discussions](https://github.com/SvetlovA/static-mock/discussions)
4. **Create Issue**: If you find a bug, create a detailed issue with:
   - Source and target versions
   - Minimal reproduction code
   - Error messages and stack traces
   - Environment details (.NET version, OS, etc.)

## Post-Migration Best Practices

After successful migration:

- **Run Full Test Suite**: Ensure all tests pass with the new version
- **Performance Testing**: Compare test execution times before and after
- **Code Review**: Review mock setups for optimization opportunities
- **Documentation Update**: Update team documentation with new patterns
- **Training**: Share new features and patterns with your team

This migration guide should help you smoothly transition between SMock versions and from other mocking frameworks. For additional support, consult the [troubleshooting guide](troubleshooting.md).

## Working Migration Examples

The migration examples shown in this guide are based on actual working test cases. You can find complete, debugged migration examples in the SMock test suite:

- **[Migration Examples](https://github.com/SvetlovA/static-mock/blob/master/src/StaticMock.Tests/Tests/Examples/MigrationGuide/MigrationExamples.cs)** - `src/StaticMock.Tests/Tests/Examples/MigrationGuide/MigrationExamples.cs`

These examples demonstrate:
- **Current working syntax** - All examples compile and pass tests with the latest SMock version
- **Best practices** - Proper usage patterns for both Sequential and Hierarchical APIs
- **Real-world scenarios** - Practical migration patterns you can copy and adapt
- **Parameter matching** - Up-to-date syntax for `It.IsAny<T>()` and other matchers

### Running Migration Examples

```bash
# Navigate to the src directory
cd src

# Run the migration examples specifically
dotnet test --filter "ClassName=MigrationExamples"

# Or run all example tests
dotnet test --filter "FullyQualifiedName~Examples"
```