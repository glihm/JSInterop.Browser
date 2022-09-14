JSInterop.Browser.WebApis
=========================

This directory contains implementations of low-level interop with web browser's web APIs.
The main list of web APIs may be found in [mozilla.org](https://developer.mozilla.org/en-US/docs/Web/API).

For now, all the web APIs are in the same project. In the future, some web APIs may be split into different projects to allow the use of separate packages.

About the testing, the first idea is to use a Blazor WASM application to test each web API feature. In the future, stuff like `cypress.io` or similar will be used to add some automation combined with GitHub actions to publish the packages and run the tests.
