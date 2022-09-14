namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;

/// <summary>
/// CryptoKeyPair representation.
/// <br />
/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/CryptoKeyPair"/>
/// </summary>
public class CryptoKeyPair<TAlgorithm>
    where TAlgorithm : IGenParams
{
    /// <summary>
    /// 
    /// </summary>
    public CryptoKey<TAlgorithm>? PublicKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public CryptoKey<TAlgorithm>? PrivateKey { get; set; }
}
