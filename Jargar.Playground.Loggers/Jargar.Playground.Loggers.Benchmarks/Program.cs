using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

// all benchmarks from given assembly are going to be executed
BenchmarkRunner.Run(
    typeof(Program).Assembly,
    ManualConfig
                .Create(DefaultConfig.Instance)
                .WithOptions(ConfigOptions.JoinSummary)
                .WithOptions(ConfigOptions.DisableLogFile));