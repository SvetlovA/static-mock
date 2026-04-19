# Common SMock Testing Patterns

Ready-to-adapt patterns for frequent testing scenarios.

---

## Time-Dependent Testing

Mock `DateTime.Now`, `DateTime.UtcNow`, or `DateTimeOffset.Now` to produce deterministic tests:

```csharp
[Test]
public void TestCalculateExpiryDateReturnsCorrectDate()
{
    var fixedNow = new DateTime(2024, 6, 15, 12, 0, 0);

    using var mock = Mock.Setup(() => DateTime.Now).Returns(fixedNow);

    var expiry = SubscriptionService.CalculateExpiry(30); // 30-day subscription

    ClassicAssert.AreEqual(new DateTime(2024, 7, 15), expiry.Date);
}
```

---

## File System Mocking

```csharp
[Test]
public void TestLoadConfigReturnsValidConfig()
{
    using var existsMock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
        .Returns(true);
    using var readMock = Mock.Setup(context => File.ReadAllText(context.It.IsAny<string>()))
        .Returns("{\"port\": 8080, \"host\": \"localhost\"}");

    var config = ConfigLoader.Load("app.json");

    ClassicAssert.AreEqual(8080, config.Port);
    ClassicAssert.AreEqual("localhost", config.Host);
}

[Test]
public void TestLoadConfigThrowsWhenFileNotFound()
{
    using var mock = Mock.Setup(context => File.Exists(context.It.IsAny<string>()))
        .Returns(false);

    ClassicAssert.Throws<FileNotFoundException>(() => ConfigLoader.Load("missing.json"));
}
```

---

## Async Method Mocking

```csharp
[Test]
public async Task TestFetchDataReturnsExpectedContent()
{
    var expectedData = "{\"users\":[]}";

    using var mock = Mock.Setup(context =>
        HttpClient.GetStringAsync(context.It.IsAny<string>()))
        .Returns(Task.FromResult(expectedData));

    var result = await DataFetcher.FetchUsers("https://api.example.com/users");

    ClassicAssert.AreEqual(0, result.Count);
}

// Dynamic async return
[Test]
public async Task TestRetryLogicWithEventualSuccess()
{
    var callCount = 0;
    using var mock = Mock.Setup(context =>
        ApiClient.CallAsync(context.It.IsAny<string>()))
        .Returns(async () =>
        {
            callCount++;
            if (callCount < 3) throw new HttpRequestException("Temporary failure");
            return "success";
        });

    var result = await RetryService.CallWithRetry("endpoint", maxAttempts: 3);

    ClassicAssert.AreEqual("success", result);
    ClassicAssert.AreEqual(3, callCount);
}
```

---

## Exception Testing

```csharp
[Test]
public void TestHandlerCatchesExternalException()
{
    using var mock = Mock.Setup(() => ExternalService.Process())
        .Throws<ServiceUnavailableException>();

    ClassicAssert.Throws<ServiceUnavailableException>(() =>
        SystemUnderTest.ExecuteWithExternalCall());
}

[Test]
public void TestHandlerCatchesAndWrapsException()
{
    using var mock = Mock.Setup(context =>
        Database.Query(context.It.IsAny<string>()))
        .Throws(new SqlException("Connection timeout"));

    var ex = ClassicAssert.Throws<DataAccessException>(() =>
        Repository.GetById(1));
    ClassicAssert.IsTrue(ex.Message.Contains("timeout"));
}
```

---

## Callback / Call Counting

```csharp
[Test]
public void TestAuditLogCalledOncePerOperation()
{
    var auditMessages = new List<string>();

    using var mock = Mock.Setup(context =>
        AuditLogger.Record(context.It.IsAny<string>()))
        .Callback<string>(msg => auditMessages.Add(msg));

    PaymentService.ProcessPayment(100m, "USD");

    ClassicAssert.AreEqual(1, auditMessages.Count);
    ClassicAssert.IsTrue(auditMessages[0].Contains("ProcessPayment"));
}
```

---

## Conditional / Input-Dependent Returns

```csharp
[Test]
public void TestEnvironmentSwitchingByConfig()
{
    var settings = new Dictionary<string, string>
    {
        ["ENV"] = "staging",
        ["LOG_LEVEL"] = "verbose",
        ["TIMEOUT"] = "30"
    };

    using var mock = Mock.Setup(context =>
        Environment.GetEnvironmentVariable(context.It.IsAny<string>()))
        .Returns<string>(key => settings.GetValueOrDefault(key));

    ClassicAssert.AreEqual("staging", FeatureFlags.GetEnvironment());
    ClassicAssert.AreEqual(30, FeatureFlags.GetTimeout());
}
```

---

## Sequential Return Values (Call Progression)

```csharp
[Test]
public void TestPollerEventuallySucceeds()
{
    var callCount = 0;

    using var mock = Mock.Setup(() => StatusChecker.IsReady())
        .Returns(() => ++callCount >= 3); // returns false, false, true

    var poller = new Poller(maxRetries: 5);
    var result = poller.WaitForReady();

    ClassicAssert.IsTrue(result);
    ClassicAssert.AreEqual(3, callCount);
}
```

---

## Multiple Coordinated Mocks

```csharp
[Test]
public void TestOrderProcessingFullFlow()
{
    var order = new Order { Id = 42, Amount = 150m };

    using var inventoryMock = Mock.Setup(context =>
        Inventory.IsAvailable(context.It.IsAny<int>())).Returns(true);

    using var paymentMock = Mock.Setup(context =>
        PaymentGateway.Charge(context.It.IsAny<decimal>()))
        .Returns(new PaymentResult { Success = true, TransactionId = "txn_abc" });

    using var notifyMock = Mock.Setup(context =>
        EmailService.SendOrderConfirmation(context.It.IsAny<Order>()));

    var result = OrderProcessor.Process(order);

    ClassicAssert.IsTrue(result.Success);
    ClassicAssert.AreEqual("txn_abc", result.TransactionId);
}
```

---

## Instance Method Mocking

```csharp
[Test]
public void TestReportGeneratorUsesFormattedDate()
{
    var formatter = new DateFormatter();
    var fixedDate = new DateTime(2024, 12, 25);

    using var mock = Mock.Setup(() => formatter.FormatForReport(fixedDate))
        .Returns("December 25, 2024");

    var report = ReportBuilder.Create(formatter, fixedDate);

    ClassicAssert.IsTrue(report.Header.Contains("December 25, 2024"));
}
```

---

## Properties (Static and Instance)

```csharp
[Test]
public void TestMachineNameUsedInLogging()
{
    using var mock = Mock.Setup(() => Environment.MachineName).Returns("TEST-NODE-01");

    var entry = Logger.BuildLogEntry("startup");

    ClassicAssert.IsTrue(entry.Contains("TEST-NODE-01"));
}
```

---

## Hierarchical API — Inline Validation Pattern

Use when you need to assert on mock parameters during the test:

```csharp
[Test]
public void TestServicePassesCorrectParametersToRepository()
{
    var capturedId = 0;

    Mock.Setup(context => UserRepository.FindById(context.It.IsAny<int>()), () =>
    {
        var user = UserRepository.FindById(99);
        ClassicAssert.IsNotNull(user);
    }).Returns(new User { Id = 99, Name = "TestUser" });

    var result = UserService.GetUserProfile(99);

    ClassicAssert.AreEqual("TestUser", result.DisplayName);
}
```

---

## Testing Void Methods (SetupAction)

```csharp
[Test]
public void TestCacheInvalidationIsCalledOnUpdate()
{
    var invalidated = false;

    using var mock = Mock.SetupAction(typeof(CacheManager), "Invalidate")
        .Callback(() => invalidated = true);

    DataService.UpdateRecord(1, "new-value");

    ClassicAssert.IsTrue(invalidated);
}
```

---

## Nested / Chained Static Calls

When the system under test triggers a chain of static calls, mock each layer:

```csharp
[Test]
public void TestDataPipelineFullChain()
{
    using var fetchMock = Mock.Setup(context =>
        DataSource.Fetch(context.It.IsAny<string>())).Returns("[1,2,3]");

    using var parseMock = Mock.Setup(context =>
        JsonParser.Parse(context.It.IsAny<string>())).Returns(new[] { 1, 2, 3 });

    using var saveMock = Mock.Setup(context =>
        DataStore.Save(context.It.IsAny<object>())).Returns(true);

    var success = Pipeline.Run("source-key");

    ClassicAssert.IsTrue(success);
}
```
