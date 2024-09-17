using ArweaveAO.Models;
using ArweaveAO.Requests;
using ArweaveAO.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArweaveAO
{
    public class AODataClient : ClientAPI
    {
        public AODataClient(IOptions<ArweaveConfig> config, HttpClient http) : base(config, http) { }


        public async Task<List<string>> GetHandlers(string processId, string? owner)
        {
            var request = new DryRunRequest()
            { 
                Target = processId,
                Owner = owner ?? processId,
                Data = "require('json').encode(require('.stringify').format(Handlers.list))",
                 Tags = new System.Collections.Generic.List<Tag>
                 {
                     new Tag() { Name = "Action", Value = "Eval" }
                 }
            };

            var result = await DryRunSingleOutput(processId, request);

            var handlers = new List<string>();

            string? jsonString = result?.Output?.Data?.Output;

            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                try
                {
                    var clean = jsonString
                        .Replace(@"\n", string.Empty)
                        .Replace(@"\u001b[0m", string.Empty)
                        .Replace(@"\u001b[31m", string.Empty)
                        .Replace(@"\u001b[32m\", string.Empty)
                        .Replace(@"\u001b[34m", string.Empty);

                    string pattern = @"name\s*=\s*""([^""]+)""";

                    MatchCollection matches = Regex.Matches(clean, pattern);

                    foreach (Match match in matches)
                    {
                        var value = match.Groups[1].Value;
                        handlers.Add(value.Trim('\\').Trim());
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return handlers;
        }

        public async Task<MessageResult?> DryRun(string processId, DryRunRequest request)
        {
            try
            {
                var result = await PostAsync<MessageResult?, DryRunRequest>($"dry-run?process-id={processId}", request);
                return result;

            }
            catch (Exception e)
            {
                // Deal with exception
                throw;
            }
        }

        public async Task<MessageResultSingleOutput?> DryRunSingleOutput(string processId, DryRunRequest request)
        {
            try
            {
                var result = await PostAsync<MessageResultSingleOutput?, DryRunRequest>($"dry-run?process-id={processId}", request);
                return result;

            }
            catch (Exception e)
            {
                // Deal with exception
                throw;
            }
        }

        public async Task<MessageResult?> GetResult(string processId, string msgId)
        {
            try
            {
                var result = await GetAsync<MessageResult?>($"result/{msgId}?process-id={processId}");
                return result;

            }
            catch (Exception e)
            {
                // Deal with exception
                throw;
            }
        }

    }
}
