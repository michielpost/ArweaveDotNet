using ArweaveAO.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ArweaveAO
{
    public abstract class ClientAPI
    {
        private readonly ArweaveConfig config;
        protected readonly HttpClient Http;

        protected ClientAPI(IOptions<ArweaveConfig> config, HttpClient http)
        {
            this.config = config.Value;
            Http = http;
        }

        protected async Task<TReturn?> GetAsync<TReturn>(string relativeUri)
        {
            HttpResponseMessage res = await Http.GetAsync(GetComputeUnitUrl(relativeUri));
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>();
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        protected async Task<TReturn?> PostAsync<TReturn, TRequest>(string relativeUri, TRequest request)
        {
            HttpResponseMessage res = await Http.PostAsJsonAsync<TRequest>(GetComputeUnitUrl(relativeUri), request);
            if (res.IsSuccessStatusCode)
            {
                return await res.Content.ReadFromJsonAsync<TReturn>();
            }
            else
            {
                string msg = await res.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
                throw new Exception(msg);
            }
        }

        public string? GetComputeUnitUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            Uri combinedUri = new Uri(new Uri(config.ComputeUnitUrl), path);
            return combinedUri.ToString();
        }
    }
}
