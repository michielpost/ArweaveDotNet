using ArweaveAO.Models;
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
    }
}