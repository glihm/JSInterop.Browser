JSInterop.Browser
=================

A library to work with Blazor and web browsers web APIs easily.

The motivation for this library is that there are few projects with the overall browser's web apis in a single place.
The idea of this project is to create a very .NET friendly way to interact with the browsers from C# code in Blazor.

The idea is to:
* Implement low level interop code into `JSInterop.Browser.WebApis`.
* Implement a higher level of abstraction (for cryptography to start) in `JSInterop.Browser.Cryptography` where a C# developper will not have to care about JSInterop. With the idea I have in mind, using DI, a C# developper will be able to do `this.aesGCMBrowser.Encrypt(key, data)` using an injected `aesGCMBrowser`, which will abstract the all JSInterop process.

Work **actively** in progress... Only on branch `main` for now. :)
