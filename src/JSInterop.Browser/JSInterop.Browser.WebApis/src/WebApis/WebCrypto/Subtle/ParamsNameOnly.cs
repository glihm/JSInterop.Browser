namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;

/// <summary>
/// Simple IParams implementation with name only.
/// </summary>
public class ParamsNameOnly : IParams
{
    /// <summary>
    /// Name of the param.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"> Name of the param. </param>
    public ParamsNameOnly(string name)
    {
        this.Name = name;
    }
}
