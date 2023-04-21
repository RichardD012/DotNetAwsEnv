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
    public static async Task<IEnumerable<KeyValuePair<string, string>>> LoadAsync(string? path)
    {
        var resultList = new List<KeyValuePair<string, string>>();
        //Handle processing of options and path
        if (string.IsNullOrEmpty(path))
        {
            //worst case scenario - no clue where to get data from
            return resultList;
        }

        try
        {
            using var client = new AmazonSimpleSystemsManagementClient(); //TODO: Make options for region
            var parameters = new List<Parameter>();
            string? nextToken = null;
            do
            {
                var request = new GetParametersByPathRequest
                {
                    Path = path,
                    Recursive = true,
                    WithDecryption = true,
                    NextToken = nextToken
                };
                var response = await client.GetParametersByPathAsync(request).ConfigureAwait(false);
                nextToken = response.NextToken;
                parameters.AddRange(response.Parameters);
            } while (!string.IsNullOrEmpty(nextToken));

            foreach (var param in parameters)
            {
                var name = param.Name.Replace(path, string.Empty);
                Environment.SetEnvironmentVariable(name, param.Value);
                resultList.Add(new (name, param.Value));
            }
        }
        catch (Exception e)
        {
            if (e.Message.Contains("token has expired"))
            {
                //Ignore this
            }

            throw;
        }

        return resultList;
    }

    public static IEnumerable<KeyValuePair<string, string>> Load(string? path)
    {
        var result = LoadAsync(path);
        result.Wait();
        return result.Result;
    }
}