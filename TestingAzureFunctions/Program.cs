using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Hosting;

namespace TestingAzureFunctions;

public static class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .Build();

        host.Run();
    }

    [Function("http-greeter")]
    public static async Task<HttpResponseData> Greet([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        if (!req.Body.TryParseJson<HttpGreeterRequest>(out var body) || body is null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync($"Hello, {body.Name}!");
        return response;
    }
}

public record HttpGreeterRequest
{
    public string Name { get; init; }
}

