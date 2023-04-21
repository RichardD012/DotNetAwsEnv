namespace DotNetAwsEnv;
public sealed class AwsEnvOptions
{
    //this is from https://github.com/Droplr/aws-env
    public const string BasePathEnv = "AWS_ENV_PATH";
    public string Path { get; set; } = string.Empty;
    public string PathEnv { get; set; } = BasePathEnv;
}