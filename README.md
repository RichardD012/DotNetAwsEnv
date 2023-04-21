# DotNetAwsEnv

A .NET library to load AWS parameter store variables into the environment. This is based on the [aws-env](https://github.com/Droplr/aws-env) project for loading data into the environment.

## Installation

Available on [NuGet](https://www.nuget.org/packages/DotNetAwsEnv/)

Visual Studio:

```powershell
PM> Install-Package DotNetAwsEnv
```

.NET CLI:

```bash
dotnet add package DotNetAwsEnv
```

## Usage

### Load parameters file

`Load()` or `LoadAsync()` will automatically search for variables in parameter store and inject them into the environment.

```csharp
DotNetAwsEnv.AwsEnv.Load("ssm/path/prefix");
await DotNetAwsEnv.AwsEnv.LoadAsync("ssm/path/prefix", cancellationToken);
```

### Using .NET Configuration provider

Integrating with the usual ConfigurationBuilder can be used as well:
```csharp
var configuration = new ConfigurationBuilder()
    .AddDotNetAwsEnv("ssm/path/prefix")
    .Build();
```

This will inject these variables into the Configuration Provider and can be used across the application with `IConfiguration`.