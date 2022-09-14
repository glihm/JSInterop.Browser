using System.Text.Json.Serialization;

using JSInterop.Browser.WebApis.WebCrypto.Subtle;
using JSInterop.Browser.WebApis.WebCrypto.Subtle.SHA;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.HMAC;


/// <summary>
/// HMAC generator params.
/// </summary>
public class HmacKeyGenParams : IGenParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public ShaAlgorithm Hash { get; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Length { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="length"></param>
    [JsonConstructor]
    public HmacKeyGenParams(ShaAlgorithm hash, int? length = null)
    {
        this.Name = "HMAC";
        this.Hash = hash;
        this.Length = length;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public object
    GetImportParams()
    {
        return new HmacImportParams(ShaAlgorithms.ToString(this.Hash), this.Length);
    }
}
