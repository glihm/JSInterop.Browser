using JSInterop.Browser.WebApis.WebCrypto.Subtle;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.HMAC;

/// <summary>
/// HMAC params.
/// </summary>
public class HmacParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public HmacParams()
    {
        this.Name = "HMAC";
    }

}
