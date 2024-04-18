using ArweaveAO.Models;
using ArweaveAO.Requests;

namespace ArweaveAO.Tests
{
    [TestClass]
    public class AODataClientTest
    {
        private const string CRED = "Sa0iBLPNyJQrwpTTG-tWLQU-1QeUAJA73DdxGGiKoJc";
        private const string AOWW = "0E6drptNUP8R3k3FiiUWbA-4zCp3QJArsCCF96VV9NY";

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
            var result2 = await api.GetBalance(CRED, "0E6drptNUP8R3k3FiiUWbA-4zCp3QJArsCCF96VV9NY");

            //Assert.IsNotNull(result);
            Assert.IsNotNull(result2);
        }

        [TestMethod]
        public async Task GetAOWWBalanceTest()
        {
            var api = new TokenClient(new HttpClient());
            var result = await api.GetBalance(AOWW, "eV-KRpB8wKowayHUUf7OpyKaUdr1WpTrRqkgiQdDVDk");
            var result2 = await api.GetBalance(AOWW, "0E6drptNUP8R3k3FiiUWbA-4zCp3QJArsCCF96VV9NY");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result2);
        }
    }
}