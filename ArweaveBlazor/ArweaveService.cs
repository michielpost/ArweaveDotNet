using ArweaveBlazor.Models;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArweaveBlazor
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class ArweaveService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        //private readonly Lazy<Task<IJSObjectReference>> arweaveTask;
        //private readonly Lazy<Task<IJSObjectReference>> aoTask;

        public ArweaveService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => LoadScripts("./_content/ArweaveBlazor/arweaveJsInterop.js", jsRuntime).AsTask());
            //arweaveTask = new(() => LoadScripts("https://unpkg.com/arweave/bundles/web.bundle.min.js", jsRuntime).AsTask());
            //aoTask = new(() => LoadScripts("https://www.unpkg.com/@permaweb/aoconnect@0.0.59/dist/browser.js", jsRuntime).AsTask());
            InitArweave();
        }

        private async ValueTask InitArweave()
        {
            var module = await moduleTask.Value;

            await module.InvokeVoidAsync("InitArweave");
        }

        public async Task SetConnection(string? gateway, string? graphql, string? mu, string? cu)
        {
            var module = await moduleTask.Value;

            await module.InvokeVoidAsync("SetConnection", gateway, graphql, mu, cu);
        }

        public ValueTask<IJSObjectReference> LoadScripts(string url, IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<IJSObjectReference>("import", url);
        }

        public async ValueTask<bool> HasArConnectAsync()
        {
            var module = await moduleTask.Value;

            var result = await module.InvokeAsync<bool>("HasArConnect");
            return result;
        }

        public async Task<string> GenerateWallet()
        {
            var module = await moduleTask.Value;

            var result = await module.InvokeAsync<string>("GenerateWallet");
            return result;
        }

        public async Task<string> SaveFile(string fileName, string fileContent)
        {
            var module = await moduleTask.Value;

            var result = await module.InvokeAsync<string>("SaveFile", fileName, fileContent);
            return result;
        }

        public async Task<string> GetAddress(string jwk)
        {
            var module = await moduleTask.Value;

            var result = await module.InvokeAsync<string>("GetAddress", jwk);
            return result;
        }

        public async ValueTask ConnectArweaveAppAsync(string? appName = null, string? appLogo = null)
        {
            var module = await moduleTask.Value;

            try
            {
                await module.InvokeVoidAsync("ConnectArweaveApp", appName, appLogo);
            }
            catch (JSException jsex)
            { }
        }

        public async ValueTask ConnectArConnectAsync(string[] permissions, string? appName = null, string? appLogo = null)
        {
            var module = await moduleTask.Value;

            try
            { 
            var appInfo = new
            {
                name = appName,
                logo = appLogo
            };

            await module.InvokeVoidAsync("ConnectArConnect", permissions, appInfo);
            }
            catch (JSException jsex)
            { }
        }

        public async ValueTask DisconnectAsync()
        {
            var module = await moduleTask.Value;

            try
            {
                await module.InvokeVoidAsync("Disconnect");
            }
            catch(JSException jsex)
            { }
        }

        public async Task<bool> CheckIsConnected()
        {
            var address = await GetActiveAddress();
            return !string.IsNullOrEmpty(address);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwk">Null for browser wallet</param>
        /// <param name="processId"></param>
        /// <param name="data"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public async ValueTask<string> SendAsync(string? jwk, string processId, string? owner, string? data, List<Tag>? tags = null)
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<string>("Send", jwk, processId, owner, data, tags);
            return result;
        }

        public async ValueTask<T?> SendDryRunAsync<T>(string processId, string? owner, string? data, List<Tag>? tags = null)
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<T?>("SendDryRun", processId, owner, data, tags);
            return result;
        }

        /// <summary>
        /// Create a process
        /// </summary>
        /// <param name="jwk"></param>
        /// <param name="moduleTxId"></param>
        /// <param name="tags"></param>
        /// <param name="scheduler">https://cookbook_ao.g8way.io/guides/aoconnect/spawning-processes.html</param>
        /// <returns></returns>
        public async ValueTask<string> CreateProcess(string? jwk, string? moduleTxId = "bkjb55i07GUCUSWROtKK4HU1mBS_X0TyH3M5jMV6aPg", List<Tag>? tags = null, string scheduler = "_GQ33BkPtZrqxA84vM8Zk-N2aO0toNNu_C-l-rawrBA")
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<string>("CreateProcess", jwk, moduleTxId, tags, scheduler);
            return result;
        }

        public async ValueTask<T?> GetResultAsync<T>(string processId, string msgId)
        {
            var module = await moduleTask.Value;

            var result = await module.InvokeAsync<T?>("GetResult", processId, msgId);
            return result;
        }

        public async ValueTask GetResultsAsync(string processId, int limit)
        {
            var module = await moduleTask.Value;

            try
            {
                await module.InvokeVoidAsync("GetResults", processId, limit);
            }
            catch (JSException jsex)
            { }
        }

        public async ValueTask<string> GetWalletBalanceAsync(string address)
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<string>("GetWalletBalance", address);
            return result;
        }

        public async ValueTask<string?> GetActiveAddress()
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<string?>("GetActiveAddress");
            return result;
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

       

    }
}