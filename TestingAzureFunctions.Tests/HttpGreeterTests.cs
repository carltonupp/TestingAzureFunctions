using System.IO;
using System.Net;
using System.Threading.Tasks;
using TestingAzureFunctions.Tests.Mocks;
using Xunit;

namespace TestingAzureFunctions.Tests;

public class HttpGreeterTests : IClassFixture<HttpFunction>
{
    private readonly HttpFunction _sut;

    public HttpGreeterTests(HttpFunction sut)
    {
        _sut = sut;
    }
    
    [Fact]
    public async Task InvalidJsonReturnsBadRequest()
    {
        var request = new MockHttpRequestData(await GetBodyStream("{invalid}"));
        var response = await TestingAzureFunctions.Program.Greet(request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    private static async Task<Stream> GetBodyStream(string json)
    {
        var stream = new MemoryStream();
        await using var writer = new StreamWriter(stream);
        await writer.WriteAsync(json);
        return stream;
    }
}