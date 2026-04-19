# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SMock is a .NET library for mocking static and instance methods and properties. It's built on top of the MonoMod library and provides two distinct API styles:

- **Hierarchical Setup**: Mock setup with validation actions - `Mock.Setup(expression, action).Returns(value)`
- **Sequential Setup**: Disposable mock setup - `using var _ = Mock.Setup(expression).Returns(value)`

The library targets multiple frameworks: .NET Standard 2.0 and .NET Framework (4.62, 4.7, 4.71, 4.72, 4.8, 4.81), published as the "SMock" NuGet package.

## Prerequisites

The development environment requires:
- .NET 8.0, 9.0, and 10.0 SDKs installed for cross-platform testing
- All tests run on Windows by default; Unix/macOS testing requires framework-specific commands

## Development Commands

### Build and Test
```bash
# Navigate to src directory first
cd src

# Restore dependencies
dotnet restore

# Build (Release configuration recommended)
dotnet build --configuration Release --no-restore

# Run all tests (Windows)
dotnet test --no-build --configuration Release --verbosity minimal

# Run tests for specific framework (Unix/macOS)
dotnet test --framework net8.0 --no-build --configuration Release --verbosity minimal
dotnet test --framework net9.0 --no-build --configuration Release --verbosity minimal
dotnet test --framework net10.0 --no-build --configuration Release --verbosity minimal

# Run a single test class
dotnet test --filter "ClassName=SetupMockReturnsTests"

# Run tests with specific category
dotnet test --filter "TestCategory=Hierarchical"
```

### Working with Individual Projects
```bash
# Build only the main library
dotnet build src/StaticMock/StaticMock.csproj

# Run only unit tests
dotnet test src/StaticMock.Tests/StaticMock.Tests.csproj

# Run benchmarks
dotnet run --project src/StaticMock.Tests.Benchmark/StaticMock.Tests.Benchmark.csproj
```

### Package Building and Publishing
```bash
# Pack the library for NuGet publication
dotnet pack --configuration Release --output packages src/StaticMock/StaticMock.csproj

# Publish to NuGet (requires API key)
dotnet nuget push packages/*.nupkg -k <NUGET_API_KEY> -s https://api.nuget.org/v3/index.json
```

## Architecture Overview

### Core Components

**Mock Entry Points** (`Mock.Hierarchical.cs`, `Mock.Sequential.cs`):
- Partial class split into two files for the two API styles
- All setup methods route through `SetupMockHelper` for consistency

**Hook Management System**:
- `HookBuilderFactory`: Determines whether to create static or instance hook builders
- `StaticHookBuilder`/`InstanceHookBuilder`: Create method hooks using MonoMod
- `MonoModHookManager`: Manages hook lifecycle and method interception

**Mock Implementations** (`Mocks/`):
- Hierarchical mocks: Support inline validation during setup
- Sequential mocks: Return disposable objects for automatic cleanup
- Type-specific mocks: `IFuncMock<T>`, `IAsyncFuncMock<T>`, `IActionMock`

**Context System** (`Entities/Context/`):
- `SetupContext`: Provides access to `It` parameter matching
- `It`: Argument matchers like `IsAny<T>()`, `Is<T>(predicate)`

### Key Design Patterns

**Factory Pattern**: `HookBuilderFactory` creates appropriate builders based on static vs instance methods

**Expression Tree Processing**: Converts Lambda expressions into `MethodInfo` for runtime hook installation

**Disposable Pattern**: Sequential mocks implement `IDisposable` for automatic cleanup

## Testing Structure

### Test Organization
- `Tests/Hierarchical/`: Tests for hierarchical API style
- `Tests/Sequential/`: Tests for sequential API style
- `Tests/*/ReturnsTests/`: Mock return value functionality
- `Tests/*/ThrowsTests/`: Exception throwing functionality
- `Tests/*/CallbackTests/`: Callback execution tests

### Test Entities (`StaticMock.Tests.Common/TestEntities/`):
- `TestStaticClass`: Static methods for testing
- `TestStaticAsyncClass`: Async static methods
- `TestInstance`: Instance methods
- `TestGenericInstance`: Generic type testing

### Running Specific Test Scenarios
```bash
# Test hierarchical returns functionality
dotnet test --filter "FullyQualifiedName~Hierarchical.ReturnsTests"

# Test sequential callback functionality
dotnet test --filter "FullyQualifiedName~Sequential.CallbackTests"

# Test async functionality across both styles
dotnet test --filter "TestMethod~Async"
```

## Multi-Framework Support

The project targets multiple .NET versions to ensure broad compatibility:
- **netstandard2.0**: Uses `MonoMod.Core` and `System.Reflection.Emit`
- **.NET Framework 4.62-4.81**: Uses `MonoMod.RuntimeDetour`

When adding new features, ensure compatibility across all target frameworks by checking conditional compilation in the `.csproj` files.

## Documentation

API documentation is generated using DocFX:
- Configuration: `docfx_project/docfx.json`
- Published to: https://svetlova.github.io/static-mock/

To work with documentation locally, use the DocFX toolchain in the `docfx_project/` directory.

## Claude Code Plugin

A Claude Code plugin lives in `smock-plugin/` and is installable from any project via:

```
/plugins install github:SvetlovA/static-mock
```

Then invoke with `/smock-plugin:smock-tests [sequential|hierarchical]` to scaffold or update tests using SMock. The skill discovers the existing test structure, writes tests, updates the project's CLAUDE.md, and validates compilation.

## SMock Usage Policy

When writing any tests in this repository, **always use the SMock library** (`using StaticMock;`) for mocking. Never use manual workarounds, wrapper classes, or other mocking approaches for method interception.

### API Style Choice

- **Sequential API** (default for most tests): `using var mock = Mock.Setup(() => Target.Method()).Returns(value);`
  - Use `using var` — mandatory for cleanup
  - Best for straightforward return-value mocking
- **Hierarchical API** (when inline validation is needed): `Mock.Setup(() => Target.Method(), () => { /* assert inside */ }).Returns(value);`
  - No disposal needed — cleanup is automatic
  - Use when you need to assert on mock behavior inline

### Non-Negotiable Rules

1. Sequential mocks **must** use `using var` — omitting it leaves hooks active and corrupts subsequent tests
2. Always access parameter matchers through the context lambda: `context.It.IsAny<T>()`, never standalone `It`
3. Test projects **must** include `<Optimize>false</Optimize>` in `.csproj` to prevent compiler inlining from breaking hooks
4. Async methods: prefer `.ReturnsAsync(value)` (shorthand on `IAsyncFuncMock<T>`); alternatively `.Returns(Task.FromResult(value))` or `.Returns(async () => value)`
5. Void methods (`IActionMock`) use `.Callback<T>(action)` for side effects — `Callback` does **not** exist on `IFuncMock`

### Test Naming Convention

- Test project: `OriginalProjectName.Tests`
- Test class: `OriginalClassNameTests`
- Test method: `Test` + MethodName [+ ScenarioDescription] (e.g., `TestGetUserReturnsActiveUser`)