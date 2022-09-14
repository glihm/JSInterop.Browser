using System.Text.Json;
using System.Text.Json.Serialization;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;


/// <summary>
/// CryptoKey types.
/// </summary>
[JsonConverter(typeof(CryptoKeyTypeConveter))]
public enum CryptoKeyType
{
    Secret,
    Private,
    Public,
}

/// <summary>
/// JSON converter.
/// </summary>
public class CryptoKeyTypeConveter : JsonConverter<CryptoKeyType>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override bool
    CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(string) || typeToConvert == typeof(CryptoKeyType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override CryptoKeyType
    Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string typestr = reader.GetString() ?? "";
        return CryptoKeyTypes.FromString(typestr);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void
    Write(Utf8JsonWriter writer, CryptoKeyType value, JsonSerializerOptions options)
    {
        writer.WriteString("type", CryptoKeyTypes.ToString(value));
    }
}

/// <summary>
/// CryptoKey's types.
/// </summary>
public static class CryptoKeyTypes
{
    /// <summary>
    /// Converts a type to it's SubtleCrypto string value.
    /// </summary>
    /// <param name="type"> CryptoKeyType. </param>
    /// <returns> String representation of the type. </returns>
    /// <exception cref="InvalidCastException"> Enum value is not mapped. </exception>
    public static string
    ToString(CryptoKeyType type)
    {
        return type switch
        {
            CryptoKeyType.Secret => "secret",
            CryptoKeyType.Private => "private",
            CryptoKeyType.Public => "public",
            _ => throw new InvalidCastException($"CryptoKeyType {type} is not mapped to a string.")
        };
    }

    /// <summary>
    /// Converts a string to a enumeration CryptoKeyType.
    /// </summary>
    /// <param name="typestr"> String representation of the type. </param>
    /// <returns> CryptoKeyType. </returns>
    /// <exception cref="InvalidCastException"> Given string is not mapped to a CryptoKeyType. </exception>
    public static CryptoKeyType
    FromString(string typestr)
    {
        return typestr switch
        {
            "secret" => CryptoKeyType.Secret,
            "private" => CryptoKeyType.Private,
            "public" => CryptoKeyType.Public,
            _ => throw new InvalidCastException($"String {typestr} can't be converted to a CryptoKeyType.")
        };
    }
}
