using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace TestingAzureFunctions;

public class GreeterHttpFunction
{
    [Function("http-greeter")]
    public async Task<HttpResponseData> Greet(
        [HttpTrigger(AuthorizationLevel.Function, "post")] 
        HttpRequestData req)
    {
        var response = req.CreateResponse(req.Body.TryParseJson<HttpGreeterRequest>(out var body) switch
        {
            false => HttpStatusCode.BadRequest,
            true when string.IsNullOrWhiteSpace(body?.Name) => HttpStatusCode.BadRequest,
            true => HttpStatusCode.OK
        });

        if (response.StatusCode is HttpStatusCode.OK) 
            await response.WriteStringAsync($"Hello, {body?.Name}!");

        return response;
    }
}

public record HttpGreeterRequest(string Name);