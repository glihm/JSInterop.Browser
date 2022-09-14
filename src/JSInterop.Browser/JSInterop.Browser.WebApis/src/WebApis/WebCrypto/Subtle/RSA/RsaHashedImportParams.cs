namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.RSA;

/// <summary>
/// RSA import params.
/// </summary>
public class RsaHashedImportParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public string Hash { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public RsaHashedImportParams(string name, string hash)
    {
        this.Name = name;
        this.Hash = hash;

    }
}
