namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;

/// <summary>
/// Generator algorithms use to generate CryptoKey.
/// <br />
/// Generator parameters are parameters that are also
/// used during an importation in order to define
/// the key properties.
/// </summary>
public interface IGenParams : IParams
{
    /// <summary>
    /// Retrieves an instance of the ImportParams
    /// related to the current GenParams. ImportParams
    /// are always a subset of the generator params.
    /// </summary>
    /// <returns></returns>
    public object
    GetImportParams();

}
