using Microsoft.JSInterop;

using JSInterop.Browser.WebApis.WebCrypto.Subtle;

namespace JSInterop.Browser.WebApis.WebCrypto;

/// <summary>
/// WebCrypto API interop.
/// <see href=""/>
/// </summary>
public class Crypto : JSWebApiModule
{
    /// <summary>
    /// SubtleCrypto interop.
    /// </summary>
    public SubtleCrypto Subtle { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="jsRuntime"></param>
    public Crypto(IJSRuntime jsRuntime)
        : base(jsRuntime, "Crypto")
    {
        this.Subtle = new(jsRuntime);
    }

    /// <summary>
    /// Implements <see href="https://developer.mozilla.org/en-US/docs/Web/API/Crypto/getRandomValues">getRandomValues</see> interop.
    /// </summary>
    /// <param name="buffer"> Buffer to be filled with random values. </param>
    public async ValueTask
    GetRandomValues(byte[] buffer)
    {
        byte[]? jsBuffer = await this.ModuleInvokeAsync<byte[]?>(
            "getRandomValuesFromCount", 
            buffer.Length).ConfigureAwait(false);

        if (jsBuffer is null)
        {
            // TODO: throw?
            // buffer is not modified.
            return;
        }

        // TODO: Is it better to return the allocated array by the interop? In this case
        //       the signature may be different from crypto web API, but better to avoid
        //       the called doing a extra allocation?
        Array.Copy(jsBuffer, buffer, buffer.Length);
    }

    /// <summary>
    /// Implements <see href="https://developer.mozilla.org/en-US/docs/Web/API/Crypto/randomUUID">randomUUID</see> interop.
    /// </summary>
    /// <returns> Newly generated UUID. </returns>
    public async ValueTask<Guid>
    RandomUUID()
    {
        string fn = $"{this.GetWindowProperty()}.randomUUID";
        string? uuidStr = await this.JSRuntime.InvokeAsync<string>(fn)
                                              .ConfigureAwait(false);

        if (string.IsNullOrEmpty(uuidStr))
        {
            return Guid.Empty;
        }

        return Guid.Parse(uuidStr);
    }

    /// <summary>
    /// Overrides base method to get web api object identifier.
    /// </summary>
    /// <returns></returns>
    protected override string
    GetWindowProperty()
    {
        return "window.crypto";
    }
}
