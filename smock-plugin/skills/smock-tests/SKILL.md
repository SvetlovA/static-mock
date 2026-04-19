---
name: SMock Tests
description: This skill should be used when the user asks to "create tests", "add tests", "write tests", "use smock to test", "mock static methods in tests", "update tests to use SMock", "generate test structure", "add unit tests for a class or method", or "/smock-tests" is invoked. Discovers or creates .NET test project structure and writes/updates tests using the SMock mocking library.
version: 0.1.0
argument-hint: "[sequential|hierarchical]"
allowed-tools: Bash, Glob, Grep, Read, Write, Edit
---

# SMock Tests

This skill discovers the existing test structure in the repository (or creates one from scratch) and writes or updates .NET tests using the SMock library.

## Arguments

An optional argument selects the default API style for generated tests:

- `sequential` — use `using var mock = Mock.Setup(...).Returns(value)` (default when no argument given)
- `hierarchical` — use `Mock.Setup(..., () => { /* validation */ }).Returns(value)`

When no argument is provided, Sequential is the default.

## Step 1: Discover Test Structure

Run these to find existing test projects:

```bash
find . -name "*.csproj" | grep -i "\.tests" | head -20
ls src/ 2>/dev/null | grep -i test
```

**If test project exists**: Read its `.csproj` to understand `TargetFramework` and package references. Find test class files matching the source class being tested and add tests there.

**If no test project exists**: Create one from scratch following Step 2.

## Step 2: Test Project Structure (create from scratch)

Naming conventions (non-negotiable):

| Item | Convention | Example |
|------|------------|---------|
| Test project | `OriginalProject.Tests` | `MyService.Tests` |
| Test class | `OriginalClassNameTests` | `FileServiceTests` |
| Test method | `Test` + MethodName [+ Description] | `TestGetUserReturnsActiveUser` |

To create a new test project:

1. Create `src/OriginalProject.Tests/` directory
2. Create the `.csproj` file (see `references/test-structure.md` for full template)
3. Add to the solution: `dotnet sln add src/OriginalProject.Tests/OriginalProject.Tests.csproj`
4. Create test class files mirroring the source folder structure

## Step 3: Write Tests with SMock

Always add `using StaticMock;` and `using NUnit.Framework.Legacy;` at the top of each test file.

### Sequential API (default)

Best for most tests — automatic cleanup via `IDisposable`:

```csharp
[Test]
public void TestMethodNameDoesExpectedBehavior()
{
    using var mock = Mock.Setup(() => TargetClass.StaticMethod())
        .Returns(expectedValue);

    var result = systemUnderTest.MethodThatCallsStatic();

    ClassicAssert.AreEqual(expectedValue, result);
}
```

With parameter matching:
```csharp
using var mock = Mock.Setup(context => TargetClass.Method(context.It.IsAny<string>()))
    .Returns(expectedValue);
```

### Hierarchical API

Best when inline validation of mock behavior is required — no `using` or dispose needed:

```csharp
[Test]
public void TestMethodNameWithInlineValidation()
{
    Mock.Setup(context => TargetClass.Method(context.It.IsAny<string>()), () =>
    {
        var result = TargetClass.Method("input");
        ClassicAssert.IsNotNull(result);
        ClassicAssert.AreEqual(expectedValue, result);
    }).Returns(expectedValue);
}
```

### Core SMock Rules

1. Sequential mocks **must** use `using var` — omitting it leaves the hook active and corrupts other tests
2. Access `It` only through the lambda's `context` parameter: `context.It.IsAny<T>()` — never standalone `It`
3. Async methods: `.ReturnsAsync(value)` (preferred) or `.Returns(Task.FromResult(value))`
4. Void methods (`IActionMock`): `.Callback<T>(p => { })` — only `IActionMock` has `Callback`, not `IFuncMock`
5. Exceptions (no-arg): `.Throws<ExceptionType>()` — requires `ExceptionType` to have a parameterless constructor
6. Exceptions (with args): `.Throws<ExceptionType>(new object[] { "message", arg2 })` or `.Throws(typeof(ExceptionType), "message")`
7. Instance methods and static properties use the same lambda syntax as static methods
8. Add `[MethodImpl(MethodImplOptions.NoOptimization)]` to a test method if mocks don't intercept in Release builds

## Step 4: Update Project CLAUDE.md

After writing tests, update (or create) the CLAUDE.md at the project root to record that SMock is in use:

1. Check if a `CLAUDE.md` exists at the root of the project being tested
2. If it exists, find the testing section and add or update an SMock usage note
3. If it doesn't exist, create one with at minimum a section covering:
   - The test project name and location
   - That SMock is used for mocking (with the NuGet package name `SMock`)
   - Which API style is the default (Sequential or Hierarchical, matching the argument used)
   - The naming convention (project / class / method)

Example section to add:

```markdown
## Testing

Tests live in `src/OriginalProject.Tests/`. Use the SMock library (`using StaticMock;`) for all mocking.

- **Default style**: Sequential (`using var mock = Mock.Setup(...).Returns(value)`)
- **Test class naming**: `OriginalClassNameTests`
- **Test method naming**: `Test` + MethodName [+ Description]
```

## Step 5: Validate Compilation and Tests Pass

After writing or modifying test files, verify everything compiles and tests pass:

```bash
cd src

# Build in Debug (required — optimizer can break SMock hooks)
dotnet build --configuration Debug

# Run the tests for the affected project
dotnet test --configuration Debug --framework net8.0 --verbosity minimal \
  --filter "FullyQualifiedName~OriginalProject.Tests"
```

If the build fails:
- Check for missing `using` statements (`using StaticMock;`, `using NUnit.Framework;`, etc.)
- Verify the `.csproj` references both the SMock package and the source project
- Ensure `<Optimize>false</Optimize>` is present in the test `.csproj`

If tests fail:
- Check that `using var` is used for all Sequential mocks
- Verify `.Throws<T>()` has the parameterless constructor constraint satisfied
- Confirm `context.It.IsAny<T>()` is used — not a standalone `It` reference

## Additional Resources

Consult these references for detailed information:

- **`references/api-reference.md`** — All `Mock.Setup` overloads, `IFuncMock`, `IActionMock`, `IAsyncFuncMock`, `SetupProperty`, `SetupAction`, `SetupDefault`, complete `Throws` and `Callback` signatures
- **`references/test-structure.md`** — Full `.csproj` template, solution integration, namespace conventions, NUnit attributes, test organization
- **`references/patterns.md`** — Common patterns: time-dependent testing, file system mocking, async mocking, callbacks, conditional returns, exception testing, multiple coordinated mocks
