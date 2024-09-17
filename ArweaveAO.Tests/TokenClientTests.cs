using ArweaveAO.Models;
using ArweaveAO.Requests;
using Microsoft.Extensions.Options;

namespace ArweaveAO.Tests
{
    [TestClass]
    public class AODataClientTest
    {
        private const string CRED = "Sa0iBLPNyJQrwpTTG-tWLQU-1QeUAJA73DdxGGiKoJc";
        private const string AOWW = "0E6drptNUP8R3k3FiiUWbA-4zCp3QJArsCCF96VV9NY";

        private const string AO = "m3PaWzK4PTG9lAaqYQPaPdOcXdO8hYqi5Fe9NWqXd0w";
        private const string AOPROXY = "Pi-WmAQp2-mh-oWH9lWpz5EthlUDj_W0IusAv-RXhRk";


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

            var api = new AODataClient(Options.Create(new ArweaveConfig()), new HttpClient());

            var result = await api.DryRun(CRED, request);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetBalanceTest()
        {
            var api = new TokenClient(Options.Create(new ArweaveConfig()), new HttpClient());
            var result = await api.GetBalance(CRED, "eV-KRpB8wKowayHUUf7OpyKaUdr1WpTrRqkgiQdDVDk");
            var result2 = await api.GetBalance(CRED, "0E6drptNUP8R3k3FiiUWbA-4zCp3QJArsCCF96VV9NY");

            //Assert.IsNotNull(result);
            Assert.IsNotNull(result2);
        }

        [TestMethod]
        public async Task GetAOWWBalanceTest()
        {
            var api = new TokenClient(Options.Create(new ArweaveConfig()), new HttpClient());
            var result = await api.GetBalance(AOWW, "eV-KRpB8wKowayHUUf7OpyKaUdr1WpTrRqkgiQdDVDk");
            var result2 = await api.GetBalance(AOWW, "0E6drptNUP8R3k3FiiUWbA-4zCp3QJArsCCF96VV9NY");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result2);
        }

        [TestMethod]
        public async Task GetAoProxyBalanceTest()
        {
            string address = "GQ-v3YuFG1Uq3YCLQQQJ1FmdjNmWvQtse35WSxXkK1k";

            var api = new TokenClient(Options.Create(new ArweaveConfig()), new HttpClient());
            //var result = await api.GetBalance(AO, address);
            var result2 = await api.GetBalance(AOPROXY, address);

            //Assert.IsNotNull(result);
            Assert.IsNotNull(result2);

            //Assert.AreEqual(result.Balance, result2.Balance);

        }
    }
}