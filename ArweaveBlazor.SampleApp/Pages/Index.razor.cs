using ArweaveBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace ArweaveBlazor.SampleApp.Pages
{
    public partial class Index
    {
        [Inject]
        public ArweaveService ArweaveService { get; set; } = default!;

        public bool HasArConnectExtension { get; set; }
        public bool IsWalletConnected { get; set; }

        private string _morpheus = "sOQYMwbbTr5MlPwp-KUmbXgCCvfoVjgTOBuUDQJZAIU";

        protected override async Task OnInitializedAsync()
        {
            HasArConnectExtension = await ArweaveService.HasArConnectAsync();

        }

        public async Task ConnectArweaveApp()
        {
            await ArweaveService.ConnectArweaveAppAsync("Sample App");
            IsWalletConnected = await ArweaveService.CheckIsConnected();
        }

        string? jwk;
        public async Task GenerateWallet()
        {
            jwk = await ArweaveService.GenerateWallet();
            Console.WriteLine("get address");
            var address = await ArweaveService.GetAddress(jwk);
            Console.WriteLine("jwk address: " + address);
            IsWalletConnected = !string.IsNullOrEmpty(address);
        }

        public async Task DownloadWallet()
        {
            string fileName = "arweave-wallet.json";

            await ArweaveService.SaveFile(fileName, jwk ?? string.Empty);
        }

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;

            // Ensure file is not null and has content
            if (file != null && file.Size > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.OpenReadStream().CopyToAsync(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (var reader = new StreamReader(memoryStream))
                    {
                        jwk = await reader.ReadToEndAsync();

                        var address = await ArweaveService.GetAddress(jwk);
                        Console.WriteLine("jwk address: " + address);
                        IsWalletConnected = !string.IsNullOrEmpty(address);
                    }
                }
            }
        }


        public async Task Connect()
        {
            await ArweaveService.ConnectArConnectAsync(new string[] { "ACCESS_ADDRESS", "SIGN_TRANSACTION" }, "Sample App");
            IsWalletConnected = await ArweaveService.CheckIsConnected();
        }

        public async Task Disconnect()
        {
            jwk = null;
            await ArweaveService.DisconnectAsync();
            IsWalletConnected = await ArweaveService.CheckIsConnected();
        }

        public async Task GetBalance()
        {
            var result = await ArweaveService.GetWalletBalanceAsync("1seRanklLU_1VTGkEk7P0xAwMJfA7owA1JHW5KyZKlY");
            Console.WriteLine("Balance: " + result);

        }

        public async Task GetActiveAddress()
        {
            var result = await ArweaveService.GetActiveAddress();
            Console.WriteLine("Active address: " + result);
        }

        string? msgId;
        public async Task Send()
        {
            msgId = await ArweaveService.SendAsync(jwk, _morpheus, null, "Morpheus?");

        }

        public async Task StartMonitor()
        {
            if (jwk != null)
            {
                var address = await ArweaveService.GetAddress(this.jwk);
                Console.WriteLine("Monitor: " + address);
                await ArweaveService.MonitorAsync(jwk, address);
            }

        }

        public async Task SendDryRun()
        {
            //var result = await ArweaveService.SendDryRunAsync<string>(_morpheus, null, "Morpheus?");

            string CRED = "Sa0iBLPNyJQrwpTTG-tWLQU-1QeUAJA73DdxGGiKoJc";

            string address = "eV-KRpB8wKowayHUUf7OpyKaUdr1WpTrRqkgiQdDVDk";
            var tags = new List<Tag>
            {
                new Tag { Name = "Action", Value = "Balance"},
                new Tag { Name = "Target", Value = address},
                new Tag { Name = "Type", Value = "Message"},
                new Tag { Name = "Variant", Value = "ao.TN.1"},
                new Tag { Name = "Protocol", Value = "ao"},
            };
            var balanceTest = await ArweaveService.SendDryRunAsync<string>(CRED, address, null, tags);

        }

        public async Task GetResult()
        {
            if(msgId != null)
            {
               var result = await ArweaveService.GetResultAsync<string>(_morpheus, msgId);
                Console.WriteLine("Result: " + result);
            }

        }

        public async Task GetResults()
        {
            await ArweaveService.GetResultsAsync(_morpheus, 10);

        }

        public async Task SetConnection()
        {
            await ArweaveService.SetConnection("https://arweave.dev", "https://arweave.dev/graphql", "https://ao-mu-1.onrender.com", "https://ao-cu-1.onrender.com");

        }

    }
}
