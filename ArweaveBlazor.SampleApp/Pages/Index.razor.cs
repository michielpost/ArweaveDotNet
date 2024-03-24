using Microsoft.AspNetCore.Components;

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


        public async Task Connect()
        {
            await ArweaveService.ConnectArConnectAsync(new string[] { "ACCESS_ADDRESS", "SIGN_TRANSACTION" }, "Sample App");
            IsWalletConnected = await ArweaveService.CheckIsConnected();
        }

        public async Task Disconnect()
        {
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
            msgId = await ArweaveService.SendAsync(_morpheus, "Morpheus?");

        }

        public async Task SendDryRun()
        {
            await ArweaveService.SendDryRunAsync<string>(_morpheus, "Morpheus?");
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

    }
}
