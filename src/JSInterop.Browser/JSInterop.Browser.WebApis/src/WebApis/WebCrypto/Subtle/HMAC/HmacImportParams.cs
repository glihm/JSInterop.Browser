using System.Text.Json.Serialization;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.HMAC;

/// <summary>
/// HMAC import params.
/// </summary>
public class HmacImportParams : IParams
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
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Length { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public HmacImportParams(string hash, int? length = null)
    {
        this.Name = "HMAC";
        this.Hash = hash;
        this.Length = length;
    }
}
