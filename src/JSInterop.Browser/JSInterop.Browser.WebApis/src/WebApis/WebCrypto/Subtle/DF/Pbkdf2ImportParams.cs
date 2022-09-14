namespace JSInterop.Browser.WebApis.WebCrypto.Subtle.DF;

/// <summary>
/// PBKDF2 import params.
/// </summary>
public class Pbkdf2ImportParams : IParams
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="iv"></param>
    public Pbkdf2ImportParams(byte[] iv)
    {
        this.Name = "PBKDF2";
    }

}
