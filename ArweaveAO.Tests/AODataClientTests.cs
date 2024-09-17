using ArweaveAO.Models;
using ArweaveAO.Requests;
using Microsoft.Extensions.Options;

namespace ArweaveAO.Tests
{
    [TestClass]
    public class TokenClientTests
    {
        private const string CRED = "Sa0iBLPNyJQrwpTTG-tWLQU-1QeUAJA73DdxGGiKoJc";
        private const string Morpheus = "sOQYMwbbTr5MlPwp-KUmbXgCCvfoVjgTOBuUDQJZAIU";

        [TestMethod]
        public async Task GetTokenMetaDataTest()
        {
            var api = new TokenClient(Options.Create(new ArweaveConfig()), new HttpClient());
            var result = await api.GetTokenMetaData(CRED);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAtomicAssetMetaDataTest()
        {
            var api = new TokenClient(Options.Create(new ArweaveConfig()), new HttpClient());
            //var result = await api.GetTokenMetaData("kBQOWxXVSj21ZhLqMTFEIJllEal1z_l8YgRRdxIm7pw");
            var result = await api.GetTokenMetaData("lqCBFE9tsqPHlRl1w0j15ZoL-Mp6870AiFVYftvt7Jo");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetResultTest()
        {
            var api = new TokenClient(Options.Create(new ArweaveConfig()), new HttpClient());
            var result = await api.GetResult(Morpheus, "r3Ep9viadnszb107G5Yyo3gMjElVRxBeuC-yKXBS9z4");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task DryRunTest()
        {
            var processId = "eV-KRpB8wKowayHUUf7OpyKaUdr1WpTrRqkgiQdDVDk";

            var request = new DryRunRequest
            {
                Target = processId,
                Tags = new List<Tag>
                    {
                        new Tag { Name = "Action", Value = "blahblah"},
                        new Tag { Name = "Type", Value = "Message"},
                        new Tag { Name = "Variant", Value = "ao.TN.1"},
                        new Tag { Name = "Protocol", Value = "ao"},
                    }
            };

            var api = new AODataClient(Options.Create(new ArweaveConfig()), new HttpClient());

            var result = await api.DryRun(processId, request);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetHandlersTest()
        {
            var processId = "0E6drptNUP8R3k3FiiUWbA-4zCp3QJArsCCF96VV9NY";
            var owner = "4NdFkWsgFQIEmJnzFSYrO88UmRPf0ABfVh_fRc2u130";

            var api = new AODataClient(Options.Create(new ArweaveConfig()), new HttpClient());

            var result = await api.GetHandlers(processId, owner);

            Assert.IsNotNull(result);
        }
    }
}