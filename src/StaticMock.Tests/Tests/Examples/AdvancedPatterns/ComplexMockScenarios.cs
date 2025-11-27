using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StaticMock.Tests.Tests.Examples.AdvancedPatterns;

[TestFixture]
public class ComplexMockScenarios
{
    [Test]
    public void Mock_Nested_Static_Calls()
    {
        // Mock the configuration reading
        using var configMock = Mock.Setup(() => Environment.GetEnvironmentVariable("DATABASE_PROVIDER"))
            .Returns("SqlServer");

        // Mock the connection string building
        using var connectionMock = Mock.Setup(context =>
                string.Concat(context.It.IsAny<string>(), context.It.IsAny<string>()))
            .Returns("Server=localhost;Database=test;");

        // Test nested calls
        var provider = Environment.GetEnvironmentVariable("DATABASE_PROVIDER");
        var connectionString = string.Concat("Server=", "localhost;Database=test;");

        ClassicAssert.AreEqual("SqlServer", provider);
        ClassicAssert.AreEqual("Server=localhost;Database=test;", connectionString);
    }

    [Test]
    [MethodImpl(MethodImplOptions.NoOptimization)]
    public void Multi_Mock_Coordination()
    {
        const string userToken = "auth_token_123";
        const int userId = 1;

        // Mock authentication
        using var authMock = Mock.Setup(context =>
                Convert.ToInt32(context.It.IsAny<string>()))
            .Returns(userId);

        // Mock audit logging
        var auditCalls = new List<string>();
        using var auditMock = Mock.Setup(context =>
                File.WriteAllText(context.It.IsAny<string>(), context.It.IsAny<string>()))
            .Callback<string, string>((path, content) => auditCalls.Add($"{path}:{content}"));

        // Execute coordinated operations
        var parsedUserId = Convert.ToInt32(userToken);
        File.WriteAllText("audit.log", $"User {parsedUserId} accessed system");

        ClassicAssert.AreEqual(userId, parsedUserId);
        ClassicAssert.AreEqual(1, auditCalls.Count);
        ClassicAssert.IsTrue(auditCalls[0].Contains("User 1 accessed system"));
    }

    [Test]
    public void Dynamic_Behavior_Based_On_History()
    {
        var callHistory = new List<string>();
        var attemptCount = 0;

        using var mock = Mock.Setup(context =>
                File.ReadAllText(context.It.IsAny<string>()))
            .Returns<string>(filename =>
            {
                callHistory.Add(filename);
                attemptCount++;

                // First two calls fail, third succeeds
                if (attemptCount <= 2)
                    throw new IOException("Service temporarily unavailable");

                return "Retrieved data";
            });

        // Simulate retry logic
        string result = null;
        for (var i = 0; i < 5; i++)
        {
            try
            {
                result = File.ReadAllText("/api/data");
                break;
            }
            catch (IOException)
            {
                if (i == 4) throw; // Re-throw on final attempt
            }
        }

        ClassicAssert.AreEqual("Retrieved data", result);
        ClassicAssert.AreEqual(3, callHistory.Count);
        ClassicAssert.IsTrue(callHistory.All(call => call == "/api/data"));
    }

    [Test]
    public void Stateful_Mock_Pattern()
    {
        var mockState = new Dictionary<string, object>();

        // Mock cache get operations
        using var getMock = Mock.Setup(context =>
                Environment.GetEnvironmentVariable(context.It.IsAny<string>()))
            .Returns<string>(key => mockState.TryGetValue(key, out var value) ? value?.ToString() : null);

        // Mock cache set operations - simulated with file write
        using var setMock = Mock.Setup(context =>
                File.WriteAllText(context.It.IsAny<string>(), context.It.IsAny<string>()))
            .Callback<string, string>((key, value) => mockState[key] = value);

        // First call should miss cache
        var result1 = Environment.GetEnvironmentVariable("key1");
        ClassicAssert.IsNull(result1);

        // Set a value
        File.WriteAllText("key1", "cached_value");

        // Second call should hit cache
        var result2 = Environment.GetEnvironmentVariable("key1");
        ClassicAssert.AreEqual("cached_value", result2);

        // Verify state was maintained
        ClassicAssert.IsTrue(mockState.ContainsKey("key1"));
        ClassicAssert.AreEqual("cached_value", mockState["key1"]);
    }

    [Test]
    public void Conditional_Mock_Selection()
    {
        var responseTemplates = new Dictionary<string, string>
        {
            ["users.json"] = "[{\"id\": 1, \"name\": \"User 1\"}]",
            ["products.json"] = "[{\"id\": 1, \"name\": \"Product 1\", \"price\": 99.99}]",
            ["orders.json"] = "[{\"id\": 1, \"userId\": 1, \"total\": 99.99}]"
        };

        using var mock = Mock.Setup(context =>
                File.ReadAllText(context.It.IsAny<string>()))
            .Returns<string>(filename =>
            {
                var fileName = Path.GetFileName(filename);
                return responseTemplates.TryGetValue(fileName, out var template)
                    ? template
                    : throw new FileNotFoundException($"File not found: {fileName}");
            });

        // Test different endpoint calls
        var usersData = File.ReadAllText("data/users.json");
        var productsData = File.ReadAllText("config/products.json");
        var ordersData = File.ReadAllText("temp/orders.json");

        ClassicAssert.IsTrue(usersData.Contains("User 1"));
        ClassicAssert.IsTrue(productsData.Contains("Product 1"));
        ClassicAssert.IsTrue(ordersData.Contains("userId"));

        // Test file not found
        Assert.Throws<FileNotFoundException>(() => File.ReadAllText("unknown.json"));
    }

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

        var logMessages = new List<string>();
        using var loggerMock = Mock.Setup(context =>
                Console.WriteLine(context.It.IsAny<string>()))
            .Callback<string>(message =>
            {
                // Only log debug messages in development
                var env = Environment.GetEnvironmentVariable("ENVIRONMENT");
                var debugMode = Environment.GetEnvironmentVariable("DEBUG_MODE");

                if (env == "Development" && debugMode == "true")
                {
                    logMessages.Add(message);
                }
            });

        // Test environment-based logging
        Console.WriteLine("Debug: Application starting");
        Console.WriteLine("Info: Processing request");

        var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
        var debugMode = Environment.GetEnvironmentVariable("DEBUG_MODE");

        ClassicAssert.AreEqual("Development", environment);
        ClassicAssert.AreEqual("true", debugMode);
        ClassicAssert.AreEqual(2, logMessages.Count);
    }
}