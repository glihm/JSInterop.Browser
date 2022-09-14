using JSInterop.Browser.WebApis.WebCrypto.Subtle;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.DF;

/// <summary>
/// HKDF import params.
/// </summary>
public class HkdfImportParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iv"></param>
    public HkdfImportParams(byte[] iv)
    {
        this.Name = "HKDF";
    }

}
