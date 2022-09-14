namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.EC;

/// <summary>
/// ECDSA params.
/// </summary>
public class EcdsaParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public EcdsaParams(string hash)
    {
        this.Name = EcAlgorithms.ECDSA;
        this.Hash = hash;
    }

}
