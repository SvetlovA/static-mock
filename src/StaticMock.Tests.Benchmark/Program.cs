using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

// Configure benchmarks for quick results
var config = DefaultConfig.Instance
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddJob(Job.Dry.WithToolchain(InProcessEmitToolchain.Instance)) // Fastest mode for quick results
    .WithOptions(ConfigOptions.DisableOptimizationsValidator); // Disable optimization validation to prevent hangs

// If no arguments provided, run all benchmarks
if (args.Length == 0)
{
    Console.WriteLine("Running all SMock benchmarks...");
    Console.WriteLine("This may take several minutes. Use --filter to run specific benchmarks.");
    Console.WriteLine();
}

// Use BenchmarkSwitcher to allow filtering specific benchmarks
var switcher = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly);
switcher.Run(args, config);