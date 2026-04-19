---
name: SMock Tests
description: This skill should be used when the user asks to "create tests", "add tests", "write tests", "use smock to test", "mock static methods in tests", "update tests to use SMock", "generate test structure", "add unit tests for a class or method", "check my tests", "fix my tests", or "/smock-tests" is invoked. Also activate when the user mentions "SMock" in context while discussing tests, asks about mocking static or instance methods, or is working on a .NET test project. Discovers or creates .NET test project structure, proactively finds untested or improperly mocked code across the entire codebase, and writes/updates tests using SMock — without asking the user which classes to test.
version: 0.2.0
argument-hint: "[sequential|hierarchical]"
allowed-tools: Bash, Glob, Grep, Read, Write, Edit
---

# SMock Tests

Discover the test structure, find gaps and non-SMock mocking patterns, and write or update .NET tests using the SMock library — proactively and without asking the user for guidance on which classes to test.

## Arguments

An optional argument selects the default API style for generated tests:

- `sequential` — use `using var mock = Mock.Setup(...).Returns(value)` (**default**)
- `hierarchical` — use `Mock.Setup(..., () => { /* validation */ }).Returns(value)`

## Core Operating Principle

**Never ask the user which classes or methods to test.** Discover all of it automatically:

1. Scan source projects for public classes and methods
2. Cross-reference against existing test files to find gaps
3. Check existing tests for non-SMock mocking (manual fakes, wrapper classes, `Moq`, etc.)
4. Write or update tests accordingly

Proceed with all discovered work unless the user has explicitly scoped the request.

## Step 0: Discover Source Code

Before touching tests, map the source:

```bash
# Find all source .csproj files (exclude test projects)
find . -name "*.csproj" | grep -iv "\.tests" | head -20

# List public classes in source projects
grep -rl "public class\|public static class\|public interface" src/ \
  --include="*.cs" | grep -iv "test" | head -40

# Find methods that call static classes (likely need mocking)
grep -rn "static\|DateTime\.\|File\.\|Path\.\|Environment\." src/ \
  --include="*.cs" | grep -iv "test" | head -30
```

Read each discovered source file to understand:
- Public classes and their dependencies
- Static method calls that will need mocking
- Async vs sync methods

## Step 1: Discover and Audit Test Structure

```bash
# Find existing test projects
find . -name "*.csproj" | grep -i "\.tests" | head -20
ls src/ 2>/dev/null | grep -i test
```

**If a test project exists:**
- Read its `.csproj` for `TargetFramework` and package references
- Find existing test class files matching the source classes
- Audit for non-SMock patterns:

```bash
# Find tests NOT using SMock
grep -rl "using StaticMock" src/ --include="*.cs" | sort > /tmp/smock_files.txt
find src/ -name "*Tests*.cs" | sort > /tmp/all_test_files.txt
comm -23 /tmp/all_test_files.txt /tmp/smock_files.txt  # tests missing SMock

# Find competing mock patterns to replace
grep -rn "new Mock<\|\.Setup(\|Moq\.\|FakeItEasy\.\|wrapper\|wrapper class" \
  src/ --include="*.cs" | grep -i test
```

**If no test project exists:** proceed to Step 2.

## Step 2: Create Test Project (if needed)

Naming conventions (non-negotiable):

| Item        | Convention                          | Example                        |
|-------------|-------------------------------------|--------------------------------|
| Test project | `OriginalProject.Tests`            | `MyService.Tests`              |
| Test class  | `OriginalClassNameTests`            | `FileServiceTests`             |
| Test method | `Test` + MethodName + Description   | `TestGetUserReturnsActiveUser` |

1. Create `src/OriginalProject.Tests/` directory
2. Create the `.csproj` file (see `references/test-structure.md` for full template — includes `<Optimize>false</Optimize>`, SMock package ref, NUnit)
3. Add to the solution: `dotnet sln add src/OriginalProject.Tests/OriginalProject.Tests.csproj`
4. Mirror source folder structure in the test project

## Step 3: Write or Update Tests with SMock

Always add `using StaticMock;` and `using NUnit.Framework.Legacy;` at the top of each test file.

For every public class or method discovered in Step 0 that lacks adequate SMock coverage: write tests. Don't skip a class because it wasn't mentioned by the user.

For existing tests using non-SMock patterns: replace them with the SMock equivalents below.

### Sequential API (default)

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

After writing tests, update (or create) the CLAUDE.md at the project root:

1. Check if a `CLAUDE.md` exists at the root of the project being tested
2. If it exists, find the testing section and add or update an SMock usage note
3. If it doesn't exist, create one with at minimum:
   - The test project name and location
   - That SMock is used for mocking (NuGet package name: `SMock`)
   - Which API style is the default (Sequential or Hierarchical)
   - Naming conventions (project / class / method)

```markdown
## Testing

Tests live in `src/OriginalProject.Tests/`. Use the SMock library (`using StaticMock;`) for all mocking.

- **Default style**: Sequential (`using var mock = Mock.Setup(...).Returns(value)`)
- **Test class naming**: `OriginalClassNameTests`
- **Test method naming**: `Test` + MethodName [+ Description]
```

## Step 5: Validate Compilation and Tests Pass

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