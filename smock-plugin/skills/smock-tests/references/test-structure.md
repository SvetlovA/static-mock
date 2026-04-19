# Test Project Structure

Reference for test project creation and naming conventions in this repository.

---

## Naming Conventions

| Item | Pattern | Example |
|------|---------|---------|
| Test project directory | `src/OriginalProject.Tests/` | `src/MyService.Tests/` |
| Test project file | `OriginalProject.Tests.csproj` | `MyService.Tests.csproj` |
| Test class namespace | `OriginalProject.Tests.Tests` | `MyService.Tests.Tests` |
| Test class name | `OriginalClassNameTests` | `FileServiceTests` |
| Test method name | `Test` + MethodName [+ Description] | `TestGetUserReturnsActiveUser` |

The `[+ Description]` suffix is optional but recommended when a class has multiple tests for the same method that cover different scenarios.

---

## .csproj Template

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <!-- Required: prevents compiler optimizations from breaking SMock hooks -->
    <Optimize>false</Optimize>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="NUnit" Version="4.*" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.*" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.*" />
    <PackageReference Include="SMock" Version="*" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\OriginalProject\OriginalProject.csproj" />
  </ItemGroup>
</Project>
```

**Critical**: `<Optimize>false</Optimize>` must be set. Without it, compiler inlining in Release builds prevents SMock hooks from intercepting calls.

---

## Adding to Solution

After creating the project file:

```bash
# Add test project to solution
dotnet sln add src/OriginalProject.Tests/OriginalProject.Tests.csproj

# Restore and verify it compiles
dotnet restore
dotnet build src/OriginalProject.Tests/OriginalProject.Tests.csproj
```

---

## Test Class Template

```csharp
using NUnit.Framework;
using NUnit.Framework.Legacy;
using StaticMock;
// Add any other using statements needed for the class under test

namespace OriginalProject.Tests.Tests;

[TestFixture]
public class OriginalClassNameTests
{
    // [SetUp] and [TearDown] optional — SMock cleans up automatically with 'using var'

    [Test]
    public void TestMethodNameDoesExpectedBehavior()
    {
        // Arrange
        using var mock = Mock.Setup(() => TargetClass.Method()).Returns(expectedValue);

        // Act
        var result = systemUnderTest.MethodThatCallsTarget();

        // Assert
        ClassicAssert.AreEqual(expectedValue, result);
    }
}
```

---

## Mirroring Source Structure

Test files should mirror the source file structure:

```
src/
├── MyService/
│   ├── Services/
│   │   └── FileService.cs           # source
│   └── Utilities/
│       └── PathHelper.cs            # source
└── MyService.Tests/
    ├── Tests/
    │   ├── Services/
    │   │   └── FileServiceTests.cs  # matches FileService.cs
    │   └── Utilities/
    │       └── PathHelperTests.cs   # matches PathHelper.cs
    └── MyService.Tests.csproj
```

---

## NUnit Attributes Reference

| Attribute | Purpose |
|-----------|---------|
| `[TestFixture]` | Marks class as containing tests |
| `[Test]` | Marks method as a test case |
| `[SetUp]` | Runs before each test |
| `[TearDown]` | Runs after each test |
| `[OneTimeSetUp]` | Runs once before all tests in fixture |
| `[OneTimeTearDown]` | Runs once after all tests in fixture |
| `[TestCase(arg1, arg2)]` | Parameterized test |
| `[Category("name")]` | Groups tests for filtering |
| `[Ignore("reason")]` | Skips test |

---

## MethodImpl Attribute for Release Builds

If tests pass in Debug but fail in Release (mocks not intercepting):

```csharp
using System.Runtime.CompilerServices;

[Test]
[MethodImpl(MethodImplOptions.NoOptimization)]
public void TestMethodThatMightBeInlined()
{
    // ...
}
```

This is a per-method escape hatch. The project-level `<Optimize>false</Optimize>` is the preferred solution.

---

## Running Tests

```bash
# All tests
cd src
dotnet test --no-build --configuration Debug --verbosity minimal

# Specific framework
dotnet test --framework net8.0 --no-build --configuration Debug

# Specific class
dotnet test --filter "ClassName=FileServiceTests"

# Specific method pattern
dotnet test --filter "FullyQualifiedName~TestGetUser"

# Specific category
dotnet test --filter "TestCategory=Unit"
```

---

## Existing Repository Test Conventions

This repository's test projects follow:
- `StaticMock.Tests` — main test suite
- `StaticMock.Tests.Common` — shared test entities (`TestStaticClass`, `TestInstance`, etc.)
- Tests organized into `Tests/Hierarchical/` and `Tests/Sequential/` sub-folders
- Sub-folders: `ReturnsTests/`, `ThrowsTests/`, `CallbackTests/`
- Class naming: `{Style}{Category}Tests` (e.g., `GenericSetupMockReturnsTests`, `MockThrowsTests`)
