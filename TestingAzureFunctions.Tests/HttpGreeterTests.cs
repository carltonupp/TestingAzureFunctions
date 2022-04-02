using System.Net;
using System.Threading.Tasks;
using Shouldly;
using TestingAzureFunctions.Tests.Extensions;
using TestingAzureFunctions.Tests.Mocks;
using Xunit;

namespace TestingAzureFunctions.Tests;

public class HttpGreeterTests : IClassFixture<GreeterHttpFunction>
{
    private readonly GreeterHttpFunction _sut;

    public HttpGreeterTests(GreeterHttpFunction sut)
    {
        _sut = sut;
    }

    [Theory]
    [InlineData("{invalid}")]
    [InlineData("{'name': ''}")]
    [InlineData("")]
    public async Task InvalidRequestBodyReturnsBadRequest(string body)
    {
        var request = new MockHttpRequestData(body);
        var response = await _sut.Greet(request);
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("John Wick")]
    [InlineData("Bryan Mills")]
    [InlineData("Katniss Eberdeen")]
    public async Task ValidRequestBodyReturnsOkWithMessage(string name)
    {
        var request = new MockHttpRequestData($"{{ 'name': '{name}' }}");
        var response = await _sut.Greet(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var message = await response.GetResponseBody();
        message.ShouldBe($"Hello, {name}!");
    }
}