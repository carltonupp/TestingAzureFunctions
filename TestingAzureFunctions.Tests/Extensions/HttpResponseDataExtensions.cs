using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace TestingAzureFunctions.Tests.Extensions;

internal static class HttpResponseDataExtensions
{
    public static async Task<string> GetResponseBody(this HttpResponseData response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(response.Body);
        return await reader.ReadToEndAsync();
    }
}