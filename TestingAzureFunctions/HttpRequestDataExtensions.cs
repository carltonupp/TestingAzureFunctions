using System.IO;
using Newtonsoft.Json;

namespace TestingAzureFunctions;

public static class HttpRequestDataExtensions
{
    public static bool TryParseJson<TOutputType>(this Stream @this, out TOutputType? result)
    {
        using var streamReader = new StreamReader(@this);
        var json = streamReader.ReadToEnd();

        if (string.IsNullOrWhiteSpace(json))
        {
            result = default;
            return false;
        }

        try
        {
            result = JsonConvert.DeserializeObject<TOutputType>(json);
            return true;

        }
        catch (JsonSerializationException)
        {
            result = default;
            return false;
        }
    }
}