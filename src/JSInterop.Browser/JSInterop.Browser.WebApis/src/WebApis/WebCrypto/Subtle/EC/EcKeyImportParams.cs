namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.EC;

/// <summary>
/// EC import params.
/// </summary>
public class EcKeyImportParams
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
    /// <param name="nameCurved"></param>
    public EcKeyImportParams(string name, string nameCurved)
    {
        this.Name = name;
        this.NamedCurve = nameCurved;
    }
}
