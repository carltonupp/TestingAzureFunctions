using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;

namespace TestingAzureFunctions.Tests.Mocks;

public sealed class MockHttpRequestData : HttpRequestData
{
    // No behaviour is actually needed from this.
    private static readonly FunctionContext Context = Mock.Of<FunctionContext>();
    
    public MockHttpRequestData(string body) : base(Context)
    {
        // I added the body parameter just to clean up boilerplate.
        var bytes = Encoding.UTF8.GetBytes(body);
        Body = new MemoryStream(bytes);
    }

    public override HttpResponseData CreateResponse()
    {
        // The actual response creation is done via extension methods
        return new MockHttpResponseData(Context);
    }

    public override Stream Body { get; }
    public override HttpHeadersCollection Headers { get; }
    public override IReadOnlyCollection<IHttpCookie> Cookies { get; }
    public override Uri Url { get; }
    public override IEnumerable<ClaimsIdentity> Identities { get; }
    public override string Method { get; }
}