using Microsoft.Extensions.Configuration;

namespace DotNetAwsEnv.Configuration;

//Most of this is duplciated from https://github.com/tonerdo/dotnet-env/tree/3dbee19770951a75785d398d02d7eed92261d0fe
public class DotNetAwsEnvConfigurationProvider : ConfigurationProvider
{
    private readonly string _path;

    public DotNetAwsEnvConfigurationProvider(
        string path)
    {
        _path = path;
    }

    public override void Load()
    {
        IEnumerable<KeyValuePair<string, string>> values;

        values = AwsEnv.Load(_path);

        // Since the Load method does not take care of cloberring, We have to check it here!
        foreach (var value in values)
        {
            var key = NormalizeKey(value.Key);
            this.Data[key] = value.Value;

        }
    }
    private static string NormalizeKey(string key) => key.Replace("__", ConfigurationPath.KeyDelimiter);
}