namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.RSA;

/// <summary>
/// RSA PSS Params.
/// </summary>
public class RsaPssParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public long SaltLength { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RsaPssParams()
    {
        this.Name = RsaAlgorithms.RSA_PSS;
    }

    // TODO: check if C# can check that. Math.ceil((keySizeInBits - 1)/8) - digestSizeInBytes - 2
}
