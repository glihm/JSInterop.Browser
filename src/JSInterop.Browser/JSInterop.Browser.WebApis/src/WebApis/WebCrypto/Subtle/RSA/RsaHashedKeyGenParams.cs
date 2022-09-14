using System.Text.Json.Serialization;

using JSInterop.Browser.WebApis.WebCrypto.Subtle.SHA;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.RSA;

/// <summary>
/// RSA generator params.
/// </summary>
public class RsaHashedKeyGenParams : IGenParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public int ModulusLength { get; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[] PublicExponent { get; }

    /// <summary>
    /// 
    /// </summary>
    public ShaAlgorithm Hash { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="length"></param>
    [JsonConstructor]
    public RsaHashedKeyGenParams(string name, int modulusLength, ShaAlgorithm hash, byte[]? publicExponent = null)
    {
        this.Name = name;
        this.ModulusLength = modulusLength;
        this.Hash = hash;
        if (publicExponent is not null)
        {
            this.PublicExponent = publicExponent;
        }
        else
        {
            this.PublicExponent = new byte[] { 1, 0, 1 };
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public object
    GetImportParams()
    {
        return new RsaHashedImportParams(this.Name, ShaAlgorithms.ToString(this.Hash));
    }
}
