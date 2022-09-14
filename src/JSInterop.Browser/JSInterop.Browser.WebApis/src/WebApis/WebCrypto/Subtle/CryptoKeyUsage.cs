using System.Text.Json.Serialization;
using System.Text.Json;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;

/// <summary>
/// CryptoKey's usages.
/// 
/// Easier to express the usage using a C#
/// enum, which is converted into string before
/// being sent to JS.
/// </summary>
[Flags]
[JsonConverter(typeof(CryptoKeyUsageConveter))]
public enum CryptoKeyUsage
{
    Encrypt = 1 << 0,
    Decrypt = 1 << 1,
    Sign = 1 << 2,
    Verify = 1 << 3,
    DeriveKey = 1 << 4,
    DeriveBits = 1 << 5,
    WrapKey = 1 << 6,
    UnwrapKey = 1 << 7,
}


/// <summary>
/// JSON converter.
/// </summary>
public class CryptoKeyUsageConveter : JsonConverter<CryptoKeyUsage>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override bool
    CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(string) || typeToConvert == typeof(CryptoKeyUsage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override CryptoKeyUsage
    Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected StartArray token.");
        }

        List<string> values = new();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            values.Add(reader.GetString() ?? "");
        }

        return CryptoKeyUsages.FromStringArray(values.ToArray());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void
    Write(Utf8JsonWriter writer, CryptoKeyUsage value, JsonSerializerOptions options)
    {
        string[] usages = CryptoKeyUsages.ToStringArray(value);
        writer.WriteStartArray();
        foreach (string u in usages)
        {
            writer.WriteStringValue(u);
        }
        writer.WriteEndArray();
    }
}


/// <summary>
/// CryptoKey's types.
/// </summary>
public static class CryptoKeyUsages
{
    /// <summary>
    /// Converts a bitmask of usages to string array.
    /// </summary>
    /// <param name="type"> CryptoKeyUsage. </param>
    /// <returns> String representation of the usages. </returns>
    public static string[]
    ToStringArray(CryptoKeyUsage usage)
    {
        List<string> usages = new();
        foreach (CryptoKeyUsage u in Enum.GetValues(typeof(CryptoKeyUsage)))
        {
            if ((int)u == 0)
            {
                continue;
            }

            if (usage.HasFlag(u))
            {
                usages.Add(_toCamelCase(u.ToString()));
            }
        }

        return usages.ToArray();
    }

    /// <summary>
    /// Converts a string array to a bitmask of usages.
    /// </summary>
    /// <param name="typestr"> String representation of the type. </param>
    /// <returns> CryptoKeyUsage. </returns>
    /// <exception cref="InvalidCastException"> Given string is not mapped to a CryptoKeyUsage. </exception>
    public static CryptoKeyUsage
    FromStringArray(string[] usages)
    {
        CryptoKeyUsage usage = 0;
        foreach (string u in usages)
        {
            CryptoKeyUsage uFlag = u switch
            {
                "encrypt" => CryptoKeyUsage.Encrypt,
                "decrypt" => CryptoKeyUsage.Decrypt,
                "sign" => CryptoKeyUsage.Sign,
                "verify" => CryptoKeyUsage.Verify,
                "wrapKey" => CryptoKeyUsage.WrapKey,
                "unwrapKey" => CryptoKeyUsage.UnwrapKey,
                "deriveBits" => CryptoKeyUsage.DeriveBits,
                "deriveKey" => CryptoKeyUsage.DeriveKey,
                _ => throw new InvalidCastException($"String {u} is not mapped to a CrytpoKeyUsage.")
            };

            usage |= uFlag;
        }

        return usage;
    }

    /// <summary>
    /// Converts a string to camel case string.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private static string
    _toCamelCase(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        if (s.Length == 1)
        {
            return s.ToLower();
        }

        return s.Substring(0, 1).ToLower() + s.Substring(1);
    }
}