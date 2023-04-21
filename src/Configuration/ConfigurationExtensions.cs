using Microsoft.Extensions.Configuration;

namespace DotNetAwsEnv.Configuration;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddDotNetAwsEnv(
        this IConfigurationBuilder builder,
        string? path)
    {
        builder.Add(new DotNetAwsEnvConfigurationSource(path));
        return builder;
    }
}