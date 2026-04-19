# Claude Code Plugin

SMock ships a **Claude Code plugin** that teaches Claude how to write and maintain .NET tests using SMock — automatically discovering what needs mocking, checking correctness, and keeping the SMock package up to date.

## Installation

From inside any project open in Claude Code:

```
/plugins install github:SvetlovA/static-mock
```

Or load for a single session only:

```bash
claude --plugin-dir /path/to/static-mock/smock-plugin
```

## Usage

```
/smock-plugin:smock-tests [sequential|hierarchical]
```

| Argument | Effect |
|----------|--------|
| *(omitted)* | Sequential API — default |
| `sequential` | `using var mock = Mock.Setup(...).Returns(value)` |
| `hierarchical` | `Mock.Setup(..., () => { ... }).Returns(value)` with inline validation |

The skill also activates automatically when you ask Claude to *"add tests"*, *"write unit tests using SMock"*, *"check my tests"*, *"update SMock version"*, or similar phrases.

## What the Skill Does

### 1. Discover Source Code

Before touching any test files, the skill scans source projects for public classes and methods, identifies static calls that require mocking (file system, time, environment variables, external services), and maps what tests already exist.

### 2. Check and Update the SMock Version

The skill fetches the latest SMock version from NuGet and compares it against every test `.csproj` in the project. If any reference is outdated, it patches the version and runs `dotnet restore` automatically.

```bash
# Performed automatically — no manual steps needed
curl https://api.nuget.org/v3-flatcontainer/smock/index.json → latest version
grep 'Include="SMock"' **/*.csproj → current version
# Patches and restores if stale
```

### 3. Decide Where Mocking Is Needed

The skill applies a decision framework before writing any test — SMock is only used where it genuinely adds value.

**Mock with SMock when the code calls:**

| Category | Examples |
|---|---|
| Non-deterministic BCL statics | `DateTime.Now`, `Guid.NewGuid()`, `Random.Next()` |
| File system | `File.Exists()`, `File.ReadAllText()`, `Directory.*` |
| Environment / process | `Environment.GetEnvironmentVariable()`, `Environment.MachineName` |
| Network / I/O | `HttpClient` static methods, `WebRequest`, `Dns.*` |
| External service façades | Static wrappers over DB, cache, message bus, third-party APIs |
| System clock / timer | `Stopwatch.GetTimestamp()`, `TimeProvider` statics |

**Do not mock with SMock when:**

- The method is a **pure function** with no side effects
- The dependency is an **interface or abstract class** — use constructor injection instead
- The logic is a **simple transformation** (parsing, mapping, arithmetic)
- The call goes to another **domain class you own** with no external dependencies
- **No isolation is needed** — the real implementation is cheap, fast, and deterministic

### 4. Audit Existing Tests (no-argument mode)

When called without arguments on a codebase that already uses SMock throughout, the skill performs a full **correctness and coverage audit** instead of rewriting anything.

**Correctness checks:**
- Sequential mocks missing `using var` — leaves hooks active and corrupts other tests
- Standalone `It.IsAny<T>()` instead of `context.It.IsAny<T>()`
- `.Callback()` called on `IFuncMock` (only valid on `IActionMock`)
- Test `.csproj` missing `<Optimize>false</Optimize>`

**Coverage checks:**
- Source methods that call non-deterministic or I/O statics but have no corresponding test
- Tests that exist but don't set up a `Mock.Setup` for the dependency they're supposed to isolate

The skill produces a structured report before making any changes:

```
## SMock Audit Report

### Correctness Issues
- [File:Line] Missing `using var` on Sequential mock
- [File:Line] Standalone `It.IsAny<T>()` — should be `context.It.IsAny<T>()`

### Coverage Gaps
- [OrderService.CreateOrder] calls DateTime.UtcNow — no test mocking this dependency

### All Clear
- No competing mock frameworks found
- <Optimize>false</Optimize> present in all test projects
```

If nothing is found: **"All clear — SMock usage is correct and coverage is complete."**

### 5. Write or Update Tests

For every gap found, the skill writes tests using the selected API style (Sequential by default).

**Sequential (default):**

```csharp
[Test]
public void TestCreateOrderSetsCorrectTimestamp()
{
    var fixedNow = new DateTime(2024, 6, 15, 12, 0, 0);

    using var mock = Mock.Setup(() => DateTime.UtcNow).Returns(fixedNow);

    var order = OrderService.CreateOrder("item-1");

    ClassicAssert.AreEqual(fixedNow, order.CreatedAt);
}
```

**Hierarchical (when inline validation is needed):**

```csharp
[Test]
public void TestCreateOrderCallsAuditLog()
{
    Mock.Setup(context => AuditLog.Record(context.It.IsAny<string>()), () =>
    {
        var order = OrderService.CreateOrder("item-1");
        ClassicAssert.IsNotNull(order);
    }).Returns(true);
}
```

### 6. Update CLAUDE.md

After writing tests, the skill updates (or creates) a `CLAUDE.md` in the project root so that Claude always uses SMock in future sessions.

### 7. Validate

```bash
dotnet build --configuration Debug
dotnet test --configuration Debug --framework net8.0 --verbosity minimal
```

Build failures and test failures are diagnosed and fixed automatically before reporting success.

## Naming Conventions

The skill enforces these conventions and will not deviate from them:

| Item | Convention | Example |
|------|------------|---------|
| Test project | `OriginalProject.Tests` | `OrderService.Tests` |
| Test class | `OriginalClassNameTests` | `OrderServiceTests` |
| Test method | `Test` + MethodName [+ Description] | `TestCreateOrderSetsCorrectTimestamp` |

## Non-Negotiable Rules Enforced

1. Sequential mocks **must** use `using var`
2. `It` matchers must go through the context lambda: `context.It.IsAny<T>()` — never standalone `It`
3. Test projects **must** have `<Optimize>false</Optimize>` to prevent inlining from breaking hooks
4. Async methods use `.ReturnsAsync(value)` (preferred) or `.Returns(Task.FromResult(value))`
5. Void methods use `.Callback<T>(action)` on `IActionMock` — `Callback` does not exist on `IFuncMock`