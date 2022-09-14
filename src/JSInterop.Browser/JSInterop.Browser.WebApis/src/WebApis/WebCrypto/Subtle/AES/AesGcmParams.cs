using System.Text.Json.Serialization;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.AES;

/// <summary>
/// Aes GCM parameters.
/// </summary>
public class AesGcmParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public byte[] Iv { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? AdditionalData { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TagLength { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iv"></param>
    public AesGcmParams(byte[] iv)
    {
        this.Name = "AES-GCM";
        this.Iv = iv;

        // TODO: See later for a constructor generating random IVs? Or the caller can easily do that calling Crypto directly...?
    }

}
