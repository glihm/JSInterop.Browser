namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.RSA;

/// <summary>
/// RSA SSA PKCS1 params.
/// </summary>
public class RsaSsaPkcs1Params : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public RsaSsaPkcs1Params()
    {
        this.Name = RsaAlgorithms.RSASSA_PKCS1_v1_5;
    }

}
