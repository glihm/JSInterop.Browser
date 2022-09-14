using System.Text;

using JSInterop.Browser.WebApis.WebCrypto.Subtle.SHA;

using Microsoft.JSInterop;

namespace JSInterop.Browser.WebApis.WebCrypto.Subtle;

/// <summary>
/// WebCrypto SubtleCrypto interop.
/// <br />
/// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto"/>
/// <br />
/// The interop with SubtleCrypto is based on the JWK format,
/// which can be pass to JS as string and get from JS as string easily.
/// The crypto keys <b>NEVER</b> live in JS more than the time of their generation
/// or when they are imported to process some data.
/// They are <b>only</b> conserved in C#.<br />
/// This is the reason why the JWK part of the C# CryptoKey must always
/// be passed to be imported in JS to do the operation.
/// </summary>
public class SubtleCrypto : JSWebApiModule
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="jsRuntime"> JS runtime. </param>
    public SubtleCrypto(IJSRuntime jsRuntime)
        : base(jsRuntime, "SubtleCrypto")
    {
    }

    /// <summary>
    /// Generates a CryptoKey for symmetric cryptography.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/generateKey"/>
    /// </summary>
    /// <param name="algorithm"> A generator algorithm. </param>
    /// <param name="usages"> CryptoKey's usages. </param>
    /// <returns> CryptoKey on success, null otherwise. </returns>
    public async ValueTask<CryptoKey<TAlgorithm>?>
    GenerateKeySymmetric<TAlgorithm>(TAlgorithm algorithm, CryptoKeyUsage usages)
        where TAlgorithm : IGenParams
    {
        return await this.ModuleInvokeAsync<CryptoKey<TAlgorithm>?>(
            "generateKeySymmetric",
            algorithm,
            usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Generates a CryptoKeyPair for asymmetric cryptography.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/generateKey"/>
    /// </summary>
    /// <param name="algorithm"> A generator algorithm. </param>
    /// <param name="usages"> CryptoKey's usages. </param>
    /// <returns> CryptoKeyPair on success, null otherwise. </returns>
    public async ValueTask<CryptoKeyPair<TAlgorithm>?>
    GenerateKeyAsymmetric<TAlgorithm>(TAlgorithm algorithm, CryptoKeyUsage usages)
        where TAlgorithm : IGenParams
    {
        return await this.ModuleInvokeAsync<CryptoKeyPair<TAlgorithm>?>(
            "generateKeyAsymmetric",
            algorithm,
            usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Exports a key.
    /// <br />
    /// For 'jwk' format, the key is returned in a buffer to only have one signature
    /// and can be converted back using Encoding.ASCII.GetString(buffer).
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/exportKey"/>
    /// </summary>
    /// <param name="format"> Data format in which the key should be exported. </param>
    /// <param name="key"> CryptoKey to export. </param>
    /// <returns></returns>
    public async ValueTask<byte[]?>
    ExportKey<TAlgorithm>(string format, CryptoKey<TAlgorithm> key)
        where TAlgorithm : IGenParams
    {
        if (format == CryptoKeyFormats.JSONWebToken)
        {
            // This is the only format returning a string.
            // Also, due to this library design, the 'jwk' form of the
            // CryptoKey is already present in C# to interop with JS.
            // We convert it as a buffer to follow the current method return type.
            // The caller should then revert is to a string.
            //
            // TODO: check if ASCII is actually ok in all scenarios, or if it's better
            //       to write an other function just for this format?
            return Encoding.ASCII.GetBytes(key.KeyJwk);
        }

        return await this.ModuleInvokeAsync<byte[]?>(
            "exportKeyBuffer",
            format,
            key.KeyJwk,
            key.Algorithm.GetImportParams(),
            key.Usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Imports a key.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/importKey">
    /// PBKDF2 and HKDF algorithms are not supported because WebApi
    /// enforced those keys to be NON-EXTRACTABLE. For this reason,
    /// the key is generated on the fly when derivation is needed.
    /// This is then pointless to generate a key using this library in
    /// derivation function scenarios.
    /// </summary>
    /// <typeparam name="TAlgorithm"> IGenParams used to describe the CryptoKey. </typeparam>
    /// <param name="format"> Format of the key (not 'jwk'). </param>
    /// <param name="key"> Key. </param>
    /// <param name="algorithm"> Import algorithm. </param>
    /// <param name="usages"> CryptoKey's usages. </param>
    /// <returns> CryptoKey on success, null otherwise. </returns>
    /// <exception cref="ArgumentException"> Format is 'jwk'. </exception>
    public async ValueTask<CryptoKey<TAlgorithm>?>
    ImportKey<TAlgorithm>(string format, byte[] key, IParams algorithm, CryptoKeyUsage usages)
        where TAlgorithm : IGenParams
    {
        if (format == CryptoKeyFormats.JSONWebToken)
        {
            throw new ArgumentException("Use 'ImportKey' with dedicated 'jwk' overload to import a JWK formatted key.");
        }

        if (algorithm.Name == "PBKDF2" || algorithm.Name == "HKDF")
        {
            throw new ArgumentException("PBKDF2 and HKDF do NOT require explicit importKey using this library.");
        }

        return await this.ModuleInvokeAsync<CryptoKey<TAlgorithm>?>(
            "importKeyBuffer",
            format,
            key,
            algorithm,
            usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Imports a key from 'jwk' format only.
    /// <br />
    /// PBKDF2 and HKDF algorithms are not supported because WebApi
    /// enforced those keys to be NON-EXTRACTABLE. For this reason,
    /// the key is generated on the fly when derivation is needed.
    /// This is then pointless to generate a key using this library in
    /// derivation function scenarios.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/importKey">
    /// </summary>
    /// <typeparam name="TAlgorithm"> IGenParams used to describe the CryptoKey. </typeparam>
    /// <param name="keyJwkStr"> String representation of 'jwk' formatted key. </param>
    /// <param name="algorithm"> Import algorithm. </param>
    /// <param name="usages"> CryptoKey's usages. </param>
    /// <returns> CryptoKey on success, null otherwise. </returns>
    public async ValueTask<CryptoKey<TAlgorithm>?>
    ImportKey<TAlgorithm>(string keyJwkStr, IParams algorithm, CryptoKeyUsage usages)
        where TAlgorithm : IGenParams
    {
        if (algorithm.Name == "PBKDF2" || algorithm.Name == "HKDF")
        {
            throw new ArgumentException("PBKDF2 and HKDF do NOT require explicit importKey using this library.");
        }

        return await this.ModuleInvokeAsync<CryptoKey<TAlgorithm>?>(
            "importKeyJwk",
            keyJwkStr,
            algorithm,
            usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Encrypts data with the given CryptoKey.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/encrypt">
    /// </summary>
    /// <param name="algorithm"> Params to configure encryption. </param>
    /// <param name="key"> CryptoKey. </param>
    /// <param name="data"> Plain text to be encrypted. </param>
    /// <returns> Cipher text on success, null otherwise. </returns>
    public async ValueTask<byte[]?>
    Encrypt<TAlgorithm>(IParams algorithm, CryptoKey<TAlgorithm> key, byte[] data)
        where TAlgorithm : IGenParams
    {
        return await this.ModuleInvokeAsync<byte[]?>(
            "encrypt",
            algorithm,
            data,
            key.KeyJwk,
            key.Algorithm.GetImportParams(),
            key.Usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Decrypts data with the given CryptoKey.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/decrypt">
    /// </summary>
    /// <param name="algorithm"> Params to configure decryption. </param>
    /// <param name="key"> CryptoKey. </param>
    /// <param name="data"> Cipher text data to be decrypted. </param>
    /// <returns> Plain text on success, null otherwise. </returns>
    public async ValueTask<byte[]?>
    Decrypt<TAlgorithm>(IParams algorithm, CryptoKey<TAlgorithm> key, byte[] data)
        where TAlgorithm : IGenParams
    {
        return await this.ModuleInvokeAsync<byte[]?>(
            "decrypt",
            algorithm,
            data,
            key.KeyJwk,
            key.Algorithm.GetImportParams(),
            key.Usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Signs data with the given CryptoKey.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/sign">
    /// </summary>
    /// <param name="algorithm"> Params to configure signing. </param>
    /// <param name="key"> CryptoKey. </param>
    /// <param name="data"> Data to be signed. </param>
    /// <returns> Signature on success, null otherwise. </returns>
    public async ValueTask<byte[]?>
    Sign<TAlgorithm>(IParams algorithm, CryptoKey<TAlgorithm> key, byte[] data)
        where TAlgorithm : IGenParams
    {
        return await this.ModuleInvokeAsync<byte[]?>(
            "sign",
            algorithm,
            data,
            key.KeyJwk,
            key.Algorithm.GetImportParams(),
            key.Usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Verifies a signature on the data with the given CryptoKey.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/verify">
    /// </summary>
    /// <param name="algorithm"> Params to configure signature verification. </param>
    /// <param name="key"> CryptoKey. </param>
    /// <param name="signature"> Signature to verify the data. </param>
    /// <param name="data"> Data to compute signature verification. </param>
    /// <returns> Signature on success, null otherwise. </returns>
    public async ValueTask<bool>
    Verify<TAlgorithm>(IParams algorithm, CryptoKey<TAlgorithm> key, byte[] signature, byte[] data)
        where TAlgorithm : IGenParams
    {
        return await this.ModuleInvokeAsync<bool>(
            "verify",
            algorithm,
            signature,
            data,
            key.KeyJwk,
            key.Algorithm.GetImportParams(),
            key.Usages).ConfigureAwait(false);
    }

    /// <summary>
    /// Generates a digest of the given data.
    /// <br />
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/SubtleCrypto/digest">
    /// </summary>
    /// <param name="algorithm"> SHA Algorithm to compute the digest. </param>
    /// <param name="data"> Data to be digested. </param>
    /// <returns> Digest on success, null otherwise. </returns>
    public async ValueTask<byte[]?>
    Digest(ShaAlgorithm algorithm, byte[] data)
    {
        return await this.ModuleInvokeAsync<byte[]?>(
            "digest",
            ShaAlgorithms.ToString(algorithm),
            data).ConfigureAwait(false);
    }

    /// <summary>
    /// Overrides base method to get web api object identifier.
    /// </summary>
    /// <returns></returns>
    protected override string
    GetWindowProperty()
    {
        return "window.crypto.subtle";
    }
}
