using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

namespace DotNetAwsEnv.Configuration;

public class DotNetAwsEnvConfigurationSource : IConfigurationSource
{
    private readonly string? _path;
    
    public DotNetAwsEnvConfigurationSource(
        string? path)
    {
        _path = path;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DotNetAwsEnvConfigurationProvider(_path);
    }
}