using Microsoft.JSInterop;

namespace JSInterop.Browser;

/// <summary>
/// Base class to bind JS code associted with a web API
/// to C# classes.
/// 
/// This class factorizes the way each module may call JSRuntime
/// in a generic manner (using the JSRuntime), or the scopes JS
/// functions into the web API module.
/// </summary>
public abstract class JSWebApiModule : IAsyncDisposable
{
    /// <summary>
    /// Name of the web API being implemented.
    /// </summary>
    public string WebApiName { get; protected set; }

    /// <summary>
    /// Reference to the JS runtime for generic calls.
    /// </summary>
    protected IJSRuntime JSRuntime { get; }

    /// <summary>
    /// Reference to the lazy loaded module for the web API.
    /// </summary>
    protected Lazy<Task<IJSObjectReference>>? JSModule { get; set; }

    /// <summary>
    /// Constructor (usually DI).
    /// </summary>
    /// <param name="jsRuntime"> JS runtime. </param>
    /// <param name="webApiName"> Name of the web API. </param>
    public JSWebApiModule(IJSRuntime jsRuntime, string webApiName)
    {
        this.WebApiName = webApiName;
        this.JSRuntime = jsRuntime;

        this.ConfigureLazyLoading();
    }
    
    /// <summary>
    /// Configures the lazy loading based on the web api path.
    /// </summary>
    protected virtual void
    ConfigureLazyLoading()
    {
        string fmt = "./_content/JSInterop.Browser.WebApis/{0}.js";
        string modulePath = string.Format(fmt, this.WebApiName);
        this.JSModule = new(() => this.JSRuntime.InvokeAsync<IJSObjectReference>("import", modulePath).AsTask());
    }

    /// <summary>
    /// Disposes allocated resources, mainly the lazy loaded module.
    /// </summary>
    /// <returns></returns>
    public async ValueTask
    DisposeAsync()
    {
        await this.DisposeInDerivedAsync();

        if (this.JSModule is not null)
        {
            IJSObjectReference m = await this.JSModule.Value;
            await m.DisposeAsync();
        }
    }

    /// <summary>
    /// Disposes allocated resources in the derived class.
    /// This method is always called by DisposeAsync.
    /// 
    /// Override this method in the derived class if there
    /// is a need to free additional resources. The module
    /// itself is freed by the DisposeAsync call.
    /// </summary>
    /// <returns></returns>
    protected virtual async ValueTask
    DisposeInDerivedAsync()
    {
        // TODO: nothing more elegant than that..
        //       the idea is to not enforce a derived class to override this function
        //       if the derived class has nothing to dispose.
        await Task.Delay(0);
    }

    /// <summary>
    /// Invokes a JS function returning void in the scope of the module.
    /// </summary>
    /// <param name="identifier"> Identifier of the function to invoke. </param>
    /// <param name="args"> List of arguments that are JSON-Serializable. </param>
    /// <returns></returns>
    protected virtual async ValueTask
    ModuleInvokeVoidAsync(string identifier, params object?[]? args)
    {
        this._throwIfModuleIsNull();

        IJSObjectReference m = await this.JSModule!.Value;
        await m.InvokeVoidAsync(identifier, args);
    }

    /// <summary>
    /// Invokes a JS function in the scope of the module
    /// returning a JSON-Serializable result.
    /// </summary>
    /// <typeparam name="TValue"> Type of the returned value. </typeparam>
    /// <param name="identifier"> Identifier of the function to invoke. </param>
    /// <param name="args"> List of arguments that are JSON-Serializable. </param>
    /// <returns></returns>
    protected virtual async ValueTask<TValue>
    ModuleInvokeAsync<TValue>(string identifier, params object?[]? args)
    {
        this._throwIfModuleIsNull();

        IJSObjectReference m = await this.JSModule!.Value;
        return await m.InvokeAsync<TValue>(identifier, args);
    }

    /// <summary>
    /// Overload of <see cref="ModuleInvokeAsync">ModuleInvokeAsync</see> with a cancellation token.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="identifier"> Identifier of the function to invoke. </param>
    /// <param name="cancellationToken"> Cancellation token. </param>
    /// <param name="args"> List of arguments that are JSON-Serializable. </param>
    /// <returns></returns>
    protected virtual async ValueTask<TValue>
    ModuleInvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, params object?[]? args)
    {
        this._throwIfModuleIsNull();

        IJSObjectReference m = await this.JSModule!.Value;
        return await m.InvokeAsync<TValue>(identifier, cancellationToken, args);
    }

    /// <summary>
    /// Returns a string corresponding to the JS
    /// identifier to retrieve the web API object in JS.
    /// </summary>
    /// <returns> A string refering to the web API identifier in JS. </returns>
    protected virtual string
    GetWindowProperty()
    {
        string fmt = "window.{0}";
        return string.Format(fmt, this.WebApiName);
    }

    /// <summary>
    /// Throws if the JS module was not initialized correctly.
    /// </summary>
    /// <exception cref="InvalidOperationException"> JSModule is null. </exception>
    private void
    _throwIfModuleIsNull()
    {
        if (this.JSModule is null)
        {
            throw new InvalidOperationException($"js module for WebApi {this.WebApiName} is null, invoke is not available.");
        }
    }
}
