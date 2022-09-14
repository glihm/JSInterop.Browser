using System.Text.Json.Serialization;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.EC;

/// <summary>
/// EC generator params.
/// </summary>
public class EcKeyGenParams : IGenParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    public string NamedCurve { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="namedCurve"></param>
    [JsonConstructor]
    public EcKeyGenParams(string name, string namedCurve)
    {
        this.Name = name;
        this.NamedCurve = namedCurve;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public object
    GetImportParams()
    {
        return new EcKeyImportParams(this.Name, this.NamedCurve);
    }
}
