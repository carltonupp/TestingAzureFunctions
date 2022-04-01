using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;

namespace TestingAzureFunctions.Tests.Mocks;

public sealed class MockHttpRequestData : HttpRequestData
{
    private static readonly FunctionContext Context = Mock.Of<FunctionContext>();
    
    public MockHttpRequestData(Stream body) : base(Context)
    {
        this.Body = body;
    }

    public override HttpResponseData CreateResponse()
    {
        throw new NotImplementedException();
    }

    public override Stream Body { get; }
    public override HttpHeadersCollection Headers { get; }
    public override IReadOnlyCollection<IHttpCookie> Cookies { get; }
    public override Uri Url { get; }
    public override IEnumerable<ClaimsIdentity> Identities { get; }
    public override string Method { get; }
}