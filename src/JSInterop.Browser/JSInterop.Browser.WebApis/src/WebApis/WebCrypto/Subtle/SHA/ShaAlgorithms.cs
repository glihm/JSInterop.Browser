using System.Text.Json.Serialization;
using System.Text.Json;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.SHA;


/// <summary>
/// SHA algorithm supported by SubtleCrypto.
/// </summary>
[JsonConverter(typeof(ShaHashConverter))]
public enum ShaAlgorithm
{
    SHA1,
    SHA256,
    SHA384,
    SHA512
}


/// <summary>
/// JSON converter.
/// </summary>
public class ShaHashConverter : JsonConverter<ShaAlgorithm>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override bool
    CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(string) || typeToConvert == typeof(ShaAlgorithm);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override ShaAlgorithm
    Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token.");
        }

        if (reader.Read() && reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException("Expected PropertyName token.");
        }

        if (reader.GetString() != "name")
        {
            throw new JsonException("Expected 'name' property name.");
        }

        if (reader.Read() && reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException("Expected String token.");
        }

        ShaAlgorithm sha = ShaAlgorithms.FromString(reader.GetString() ?? "__");

        // end of object.
        reader.Read();

        return sha;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void
    Write(Utf8JsonWriter writer, ShaAlgorithm value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("name", ShaAlgorithms.ToString(value));
        writer.WriteEndObject();
        //writer.WriteStringValue(ShaAlgorithms.ToString(value));

    }
}


/// <summary>
/// SHA algorithms.
/// </summary>
public static class ShaAlgorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hash"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public static ShaAlgorithm
    FromString(string hash)
    {
        return hash switch
        {
            "SHA-1" => ShaAlgorithm.SHA1,
            "SHA-256" => ShaAlgorithm.SHA256,
            "SHA-384" => ShaAlgorithm.SHA384,
            "SHA-512" => ShaAlgorithm.SHA512,
            _ => throw new InvalidCastException($"String {hash} can't be casted into ShaAlgorithm.")
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="algo"></param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException"></exception>
    public static string
    ToString(ShaAlgorithm algo)
    {
        return algo switch
        {
            ShaAlgorithm.SHA1 => "SHA-1",
            ShaAlgorithm.SHA256 => "SHA-256",
            ShaAlgorithm.SHA384 => "SHA-384",
            ShaAlgorithm.SHA512 => "SHA-512",
            _ => throw new InvalidCastException($"ShaAlgorithm {algo} can't be mapped into a known string.")
        };
    }
}
