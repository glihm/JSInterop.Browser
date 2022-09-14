namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.AES;

/// <summary>
/// AES Import parameters.
/// </summary>
public class AesImportParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public AesImportParams(string name)
    {
        this.Name = name;
    }
}
