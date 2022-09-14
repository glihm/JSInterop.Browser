using System.Text.Json.Serialization;

using JSInterop.Browser.WebApis.WebCrypto.Subtle;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.AES;

/// <summary>
/// AES generator params.
/// </summary>
public class AesKeyGenParams : IGenParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public int Length { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="length"></param>
    [JsonConstructor]
    public AesKeyGenParams(string name, int length)
    {
        this.Name = name;
        this.Length = length;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public object
    GetImportParams()
    {
        return new AesImportParams(this.Name);
    }
}
