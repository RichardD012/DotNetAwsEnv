using Amazon;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;

namespace DotNetAwsEnv;

/// <summary>
/// Fetch parameters from AWS systems manager parameter store
/// and loads them as environment variables
/// </summary>
public static class AwsEnv
{
    public static async Task Load(string? awsEnvPath)
    {
        if (string.IsNullOrEmpty(awsEnvPath))
        {
            return;
        }
        using var client = new AmazonSimpleSystemsManagementClient(RegionEndpoint.USEast1);
        var parameters = new List<Parameter>();
        string? nextToken = null;
        do
        {
            var request = new GetParametersByPathRequest
            {
                Path = awsEnvPath,
                Recursive = true,
                WithDecryption = true,
                NextToken = nextToken
            };
            var response = await client.GetParametersByPathAsync(request);
            nextToken = response.NextToken;
            parameters.AddRange(response.Parameters);
        } while (!string.IsNullOrEmpty(nextToken));
        foreach (var param in parameters)
        {
            var name = param.Name.Replace(awsEnvPath, string.Empty);
            Environment.SetEnvironmentVariable(name, param.Value);
        }
    }
}