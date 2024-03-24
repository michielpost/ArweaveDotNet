using ArweaveAO.Models;
using ArweaveAO.Requests;

namespace ArweaveAO.Tests
{
    [TestClass]
    public class AODataClientTest
    {
        private const string CRED = "Sa0iBLPNyJQrwpTTG-tWLQU-1QeUAJA73DdxGGiKoJc";

        [TestMethod]
        public async Task DryRunTest()
        {
            var request = new DryRunRequest
            {
                Target = CRED,
                Tags = new List<Tag>
                    {
                        new Tag { Name = "Action", Value = "Info"},
                        new Tag { Name = "Type", Value = "Message"},
                        new Tag { Name = "Variant", Value = "ao.TN.1"},
                        new Tag { Name = "Protocol", Value = "ao"},
                    }
            };

            var api = new AODataClient(new HttpClient());

            var result = await api.DryRun(CRED, request);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBalanceTest()
        {
            var api = new TokenClient(new HttpClient());
            var result = await api.GetBalance(CRED, "eV-KRpB8wKowayHUUf7OpyKaUdr1WpTrRqkgiQdDVDk");

            Assert.IsNotNull(result);
        }
    }
}