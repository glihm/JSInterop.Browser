using System.Text.Json.Serialization;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.RSA;

/// <summary>
/// RSA OAEP params.
/// </summary>
public class RsaOaepParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? Label { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RsaOaepParams()
    {
        this.Name = RsaAlgorithms.RSA_OAEP;
    }

}
