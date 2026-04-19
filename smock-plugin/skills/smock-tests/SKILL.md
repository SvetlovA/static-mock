---
name: SMock Tests
description: This skill should be used when the user asks to "create tests", "add tests", "write tests", "use smock to test", "mock static methods in tests", "update tests to use SMock", "generate test structure", "add unit tests for a class or method", "check my tests", "fix my tests", "update SMock version", "check SMock version", or "/smock-tests" is invoked. Also activate when the user mentions "SMock" in context while discussing tests, asks about mocking static or instance methods, or is working on a .NET test project. Discovers or creates .NET test project structure, checks and updates the SMock package to the latest version, proactively finds untested or improperly mocked code across the entire codebase, and writes/updates tests using SMock only where mocking is actually needed — without asking the user which classes to test.
version: 0.3.1
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
4. Write or update tests accordingly — **only where mocking is warranted** (see decision framework below)

Proceed with all discovered work unless the user has explicitly scoped the request.

## When to Use SMock (Decision Framework)

Before writing any test, evaluate whether the method under test actually requires mocking. Adding unnecessary mocks makes tests brittle and harder to read.

### Mock with SMock when the code calls:

| Category | Examples |
|---|---|
| Non-deterministic BCL statics | `DateTime.Now`, `DateTime.UtcNow`, `Guid.NewGuid()`, `Random.Next()` |
| File system | `File.Exists()`, `File.ReadAllText()`, `Directory.GetFiles()`, `Path.*` |
| Environment / process | `Environment.GetEnvironmentVariable()`, `Environment.MachineName`, `Environment.Exit()` |
| Network / I/O | `HttpClient` static methods, `WebRequest`, `Dns.GetHostName()` |
| External services | Any static façade over a database, cache, message bus, or third-party API |
| System clock / timer | `Stopwatch.GetTimestamp()`, `TimeProvider` static members |
| Volatile or side-effecting statics | Logging singletons called via static, global config readers |

### Do NOT mock with SMock when:

- The method under test is a **pure function** — given the same input it always returns the same output with no side effects
- The dependency is an **interface or abstract class** — use constructor injection and pass a test double directly
- The logic is a **simple data transformation** (parsing, mapping, arithmetic, string manipulation)
- The call is to another **internal/domain class** you own and that class has no external dependencies
- **No isolation is actually needed** — the real implementation is cheap, fast, and deterministic in tests

### How to apply during source scanning (Step 0)

For each public method found, check: does the method body (or any method it transitively calls) reach a static that is non-deterministic, has I/O side effects, or depends on external state? If yes → SMock candidate. If no → write a plain test without mocking.

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

## Step 0.5: Check and Update SMock Version

After locating test `.csproj` files, verify all SMock references use the latest published version.

```bash
# Fetch the latest stable SMock version from NuGet
LATEST=$(curl -s "https://api.nuget.org/v3-flatcontainer/smock/index.json" \
  | grep -oE '"[0-9]+\.[0-9]+\.[0-9]+"' | tr -d '"' | sort -V | tail -1)
echo "Latest SMock on NuGet: $LATEST"

# Find all .csproj files referencing SMock
grep -rl 'Include="SMock"' . --include="*.csproj"
```

For each `.csproj` found, read the current `Version` attribute on the SMock `PackageReference`:

```bash
grep 'SMock' path/to/Project.Tests.csproj
```

**If the version is outdated** (pinned to an older release, not `*`, or `*` is resolving to an old locked version):

1. Edit the `.csproj` — update the `Version` attribute to the fetched `$LATEST` value
2. Run `dotnet restore` in the `src/` directory to pull the updated package
3. Confirm the restore succeeded before continuing

**If `Version="*"` is already present**: run `dotnet restore` once to ensure the wildcard resolves to the latest, then confirm via:

```bash
dotnet list path/to/Project.Tests.csproj package | grep SMock
```

Skip this step only when no test `.csproj` files exist yet (they will be created in Step 2 using the latest version).

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

**If non-SMock patterns are found:** proceed to Step 2 / Step 3 to fix them.

**If every test file already uses SMock** (the `comm` output is empty and no competing patterns found): proceed directly to **Step 1.5** for a full correctness and coverage audit.

**If no test project exists:** proceed to Step 2.

## Step 1.5: Correctness and Coverage Audit

Run this step whenever the codebase is already on SMock and the skill is invoked without a specific fix request. The goal is to surface real problems — incorrect usage and uncovered scenarios — not cosmetic issues.

### Correctness: scan for SMock anti-patterns

```bash
# Sequential mocks missing 'using var' (leaves hook active, corrupts other tests)
grep -rn "var [a-zA-Z_]* = Mock\.Setup" src/ --include="*.cs" | grep -v "using var"

# Standalone 'It' not accessed through context lambda
grep -rn "\bIt\.IsAny\|\bIt\.Is(" src/ --include="*.cs"

# Callback called on IFuncMock (only valid on IActionMock)
grep -rn "\.Returns(.*)\s*\n?\s*\.Callback\|\.Returns(.*).Callback" src/ --include="*.cs"

# Missing <Optimize>false</Optimize> in test .csproj files
grep -rL "Optimize>false" . --include="*.csproj" | grep -i test
```

For each hit, read the surrounding context and determine if it is a genuine violation. Report findings grouped by file, with line numbers.

### Coverage: find mocking gaps

For each source method identified as an SMock candidate in Step 0 (non-deterministic / I/O / external):

1. Check whether a corresponding test method exists in the test project
2. Check whether that test method actually contains a `Mock.Setup` call targeting the external dependency

```bash
# List all public methods in source that touch non-deterministic statics
grep -rn "DateTime\.\|File\.\|Environment\.\|Guid\.NewGuid\|Random\." src/ \
  --include="*.cs" | grep -iv test

# For each source class, check if a *Tests class exists
# e.g. for FileService.cs → grep for FileServiceTests in test files
```

For each gap found (mock candidate with no test or no `Mock.Setup`):
- Note the source method and what it calls that needs mocking
- Note whether no test exists at all, or a test exists but doesn't mock the dependency

### Audit Report Format

After completing the audit, produce a concise report before making any changes:

```
## SMock Audit Report

### Correctness Issues
- [FILE:LINE] Missing `using var` on Sequential mock
- [FILE:LINE] Standalone `It.IsAny<T>()` — should be `context.It.IsAny<T>()`
- [FILE] Missing <Optimize>false</Optimize> in .csproj

### Coverage Gaps
- [SourceClass.Method] calls DateTime.Now — no test mocking this dependency
- [SourceClass.OtherMethod] calls File.ReadAllText — test exists but does not mock File access

### All Clear
- (listed items that were checked and passed)
```

Then fix all issues found using Step 3. If nothing is found, report "All clear — SMock usage is correct and coverage is complete."

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