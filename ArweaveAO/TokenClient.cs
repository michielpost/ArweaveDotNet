using ArweaveAO.Extensions;
using ArweaveAO.Models;
using ArweaveAO.Models.Token;
using ArweaveAO.Requests;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArweaveAO
{
    /// <summary>
    /// TokenClient, helper to interact with processes implementing the ao token standard
    /// </summary>
    public class TokenClient : AODataClient
    {
        public TokenClient(IOptions<ArweaveConfig> config, HttpClient http) : base(config, http) { }

        public async Task<TokenData?> GetTokenMetaData(string processId)
        {
            try
            {
                var request = new DryRunRequest
                { 
                    Target = processId,
                    Tags = new List<Tag>
                    {
                        new Tag { Name = "Action", Value = "Info"},
                        new Tag { Name = "Type", Value = "Message"},
                        new Tag { Name = "Variant", Value = "ao.TN.1"},
                        new Tag { Name = "Protocol", Value = "ao"},
                    }
                };
                var result = await DryRun(processId, request);
                if (result == null || !result.Messages.Any())
                    return null;

                var tokenData = new TokenData();
                tokenData.TokenId = processId;
                tokenData.Name = result.GetFirstTagValue("Name");
                tokenData.Ticker = result.GetFirstTagValue("Ticker");
                tokenData.Logo = result.GetFirstTagValue("Logo");
                
                
                string? denomination = result.GetFirstTagValue("Denomination");
                if(!string.IsNullOrWhiteSpace(denomination) && int.TryParse(denomination, out int denominationInt))
                    tokenData.Denomination = denominationInt;

                return tokenData;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("not found", StringComparison.InvariantCultureIgnoreCase))
                    return null;

                // Deal with exception
                throw;
            }
        }

        public async Task<BalanceData?> GetBalance(string tokenId, string address)
        {
            try
            {
                var request = new DryRunRequest
                {
                    Target = tokenId,
                    Owner = address,
                    Tags = new List<Tag>
                    {
                        new Tag { Name = "Action", Value = "Balance"},
                        new Tag { Name = "Target", Value = address},
                        new Tag { Name = "Type", Value = "Message"},
                        new Tag { Name = "Variant", Value = "ao.TN.1"},
                        new Tag { Name = "Protocol", Value = "ao"},
                    }
                };
                var result = await DryRun(tokenId, request); 
                if (result == null || !result.Messages.Any())
                    return null;

                var balanceData = new BalanceData();
                balanceData.TokenId = tokenId;
                balanceData.Ticker = result.GetFirstTagValue("Ticker");
                balanceData.Account = result.GetFirstTagValue("Account");
                
                string? balance = result.GetFirstTagValue("Balance");
                if (!string.IsNullOrWhiteSpace(balance) && long.TryParse(balance, out long balanceLong))
                    balanceData.Balance = balanceLong;

                return balanceData;
            }
            catch (Exception e)
            {
                // Deal with exception
                throw;
            }
        }
    }
}
