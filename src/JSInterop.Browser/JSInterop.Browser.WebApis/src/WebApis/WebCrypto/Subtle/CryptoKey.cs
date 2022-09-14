using System.Text.Json.Serialization;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;

/// <summary>
/// CryptoKey.
/// <br />
/// A CryptoKey lives <b>only</b> in C#. When an operation requires
/// a CryptoKey, the library gives all the parameters to JS
/// in order to import the key and proceed to the cryptographic operation.
/// <br />
/// The CryptoKey is stored in C# memory using 'jwk' format, which is easier
/// to interop with JS. For this reason, the 'extractable' parameter is not relevant in
/// this library as all keys directly extracted to be sent back to C#.
/// <br />
/// The only scenarios where this is not possible is in derivation functions like
/// PBKDF2 where SubtleCrypto enforced the key to be NOT-EXTRACTABLE. In those
/// scenarios, the key is generated on the fly before the derivation and C#
/// only keeps the raw key.
/// <br />
/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/CryptoKey"/>
/// </summary>
public class CryptoKey<TAlgorithm>
    where TAlgorithm : IGenParams
{
    /// <summary>
    /// Type of the CryptoKey.
    /// </summary>
    public CryptoKeyType Type { get; }

    /// <summary>
    /// Generator algorithm used to generate and configure the CryptoKey.
    /// </summary>
    public TAlgorithm Algorithm { get; }

    /// <summary>
    /// Usages of the CryptoKey.
    /// </summary>
    public CryptoKeyUsage Usages { get; }

    /// <summary>
    /// Representation of the CryptoKey in JWT format
    /// to be passed back-and-forth to JS.
    /// </summary>
    public string KeyJwk { get; }

    /// <summary>
    /// Deserialization constructor.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="algorithm"></param>
    /// <param name="usages"></param>
    /// <param name="keyJwk"></param>
    [JsonConstructor]
    public CryptoKey(CryptoKeyType type, TAlgorithm algorithm, CryptoKeyUsage usages, string keyJwk)
    {
        Type = type;
        Algorithm = algorithm;
        Usages = usages;
        KeyJwk = keyJwk;
    }

}

