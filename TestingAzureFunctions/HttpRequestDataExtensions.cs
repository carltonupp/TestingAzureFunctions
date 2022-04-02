using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TestingAzureFunctions;

public static class HttpRequestDataExtensions
{
    public static bool TryParseJson<TOutputType>(this Stream @this, out TOutputType? result)
    {
        using var streamReader = new StreamReader(@this, encoding: Encoding.UTF8);
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
        catch (Exception ex) when(ex is JsonSerializationException or JsonReaderException)
        {
            result = default;
            return false;
        }
    }
}