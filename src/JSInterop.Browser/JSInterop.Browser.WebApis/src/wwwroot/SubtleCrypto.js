'use strict';


/*
 * Generates symmetric key.
 */
async function generateKeySymmetric(algorithm, keyUsages)
{
    console.log("ALGO -> ", algorithm);
    try
    {
        const cryptoKey = await window.crypto.subtle.generateKey(algorithm, true, keyUsages);
        const keyJwk = await window.crypto.subtle.exportKey("jwk", cryptoKey);

        const ck =
        {
            type: cryptoKey.type,
            algorithm: cryptoKey.algorithm,
            usages: cryptoKey.usages,
            keyJwk: JSON.stringify(keyJwk),
        };

        console.log(ck);
        return ck;
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Generates asymmetric key.
 */
async function generateKeyAsymmetric(algorithm, keyUsages)
{
    try
    {
        const cryptoKeyPair = await window.crypto.subtle.generateKey(algorithm, true, keyUsages);

        const privateKey = cryptoKeyPair.privateKey;
        const publicKey = cryptoKeyPair.publicKey;

        const privateKeyJwk = await window.crypto.subtle.exportKey("jwk", privateKey);
        const publicKeyJwk = await window.crypto.subtle.exportKey("jwk", publicKey);

        const ckp =
        {
            privateKey:
            {
                type: privateKey.type,
                algorithm: privateKey.algorithm,
                usages: privateKey.usages,
                keyJwk: JSON.stringify(privateKeyJwk),
            },
            publicKey:
            {
                type: publicKey.type,
                algorithm: publicKey.algorithm,
                usages: publicKey.usages,
                keyJwk: JSON.stringify(publicKeyJwk),
            }
        };

        return ckp;
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Exports buffer keys. JWK keys lived in C# only.
 */
async function exportKeyBuffer(format, keyJwkStr, importParams, usages)
{
    try
    {
        if (format == "jwk")
        {
            console.error("Format jwk is already available from dotnet.")
            return null;
        }

        const cryptoKey = await window.crypto.subtle.importKey("jwk", JSON.parse(keyJwkStr), importParams, true, usages);
        const buf = await window.crypto.subtle.exportKey(format, cryptoKey);
        return new Uint8Array(buf);
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Imports buffer key.
 */
async function importKeyBuffer(format, keyDataBuffer, algorithm, usages)
{
    try
    {
        if (format == "jwk")
        {
            console.error("Use dedicated function to import JWK formatted key.");
            return null;
        }

        const cryptoKey = await window.crypto.subtle.importKey(format, keyDataBuffer, algorithm, true, usages);
        const keyJwk = await window.crypto.subtle.exportKey("jwk", cryptoKey);

        const ck =
        {
            type: cryptoKey.type,
            algorithm: cryptoKey.algorithm,
            usages: cryptoKey.usages,
            keyJwk: JSON.stringify(keyJwk),
        };

        return ck;
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Import string jwk key.
 */
async function importKeyJwk(keyJwkStr, algorithm, usages)
{
    const cryptoKey = await window.crypto.subtle.importKey("jwk", JSON.parse(keyJwkStr), algorithm, true, usages);
    const keyJwk = await window.crypto.subtle.exportKey("jwk", cryptoKey);

    const ck =
    {
        type: cryptoKey.type,
        algorithm: cryptoKey.algorithm,
        usages: cryptoKey.usages,
        keyJwk: JSON.stringify(keyJwk),
    };

    return ck;
}

/*
 * Encrypts data.
 */
async function encrypt(algorithm, data, keyJwkStr, importParams, usages)
{
    try
    {
        const cryptoKey = await window.crypto.subtle.importKey("jwk", JSON.parse(keyJwkStr), importParams, false, usages);
        const cipher = await window.crypto.subtle.encrypt(algorithm, cryptoKey, data);
        return new Uint8Array(cipher);
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Decrypts data.
 */
async function decrypt(algorithm, data, keyJwkStr, importParams, usages)
{
    try
    {
        const cryptoKey = await window.crypto.subtle.importKey("jwk", JSON.parse(keyJwkStr), importParams, false, usages);
        const plain = await window.crypto.subtle.decrypt(algorithm, cryptoKey, data);
        return new Uint8Array(plain);
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Signs data.
 */
async function sign(algorithm, data, keyJwkStr, importParams, usages)
{
    try
    {
        const cryptoKey = await window.crypto.subtle.importKey("jwk", JSON.parse(keyJwkStr), importParams, false, usages);
        const signature = await window.crypto.subtle.sign(algorithm, cryptoKey, data);
        return new Uint8Array(signature);
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Verifies data with signature.
 */
async function verify(algorithm, signature, data, keyJwkStr, importParams, usages)
{
    try
    {
        const cryptoKey = await window.crypto.subtle.importKey("jwk", JSON.parse(keyJwkStr), importParams, false, usages);
        return await window.crypto.subtle.verify(algorithm, cryptoKey, signature, data);
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}

/*
 * Computes a digest from data.
 */
async function digest(algorithm, data)
{
    try
    {
        const d = await window.crypto.subtle.digest(algorithm, data);
        return new Uint8Array(d);
    }
    catch (e)
    {
        console.error(e);
        return null;
    }
}


export {
    generateKeySymmetric,
    generateKeyAsymmetric,
    encrypt,
    decrypt,
    digest,
    exportKeyBuffer,
    importKeyBuffer,
    importKeyJwk,
    sign,
    verify,

}
