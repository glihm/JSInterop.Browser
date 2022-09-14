namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;

/// <summary>
/// Parameters interface for JS interop.
/// <br />
/// SubtleCrypto is almost always expecting parameters/algorithms
/// which have a name key.
/// <br />
/// In some rare occasions, only a string may be passed
/// as a parameter for algorithms. In those scenarios,
/// the function accepting the IParams extracts the name only.
/// </summary>
public interface IParams
{
    /// <summary>
    /// Name of the param/algorithm.
    /// </summary>
    public string Name { get; }
}
