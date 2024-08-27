using ArweaveAO.Models;
using ArweaveAO.Requests;
using Microsoft.Extensions.Options;

namespace ArweaveAO.Tests
{
    [TestClass]
    public class AddressValidatorTests
    {
      

        [TestMethod]
        public async Task ValidateValidAddress()
        {
            //Ethereum
            Assert.IsTrue(AddressValidator.IsValidAddress("0x92B143F46C3F8B4242bA85F800579cdF73882e98"));
           
            //Arweave format
            Assert.IsTrue(AddressValidator.IsValidAddress("OT9qTE2467gcozb2g8R6D6N3nQS94ENcaAIJfUzHCww"));
            Assert.IsTrue(AddressValidator.IsValidAddress("2gM9n9QO6JG1_bZhCWr3fuEKJtzRgx1xvYUB92nVFAs"));
            Assert.IsTrue(AddressValidator.IsValidAddress("-a4T7XLMDGTcu8_preKXdUT6__4sJkMhYLEJZkXUYd0"));
            Assert.IsTrue(AddressValidator.IsValidAddress("rik3eCayInKVNzSMdoxeSEfpxNd5U7tx1H8NAveg4o8"));
        }

        [TestMethod]
        public async Task ValidateInvalidAddress()
        {
            Assert.IsFalse(AddressValidator.IsValidAddress(""));
            Assert.IsFalse(AddressValidator.IsValidAddress("0xa"));
            Assert.IsFalse(AddressValidator.IsValidAddress("abc"));
            Assert.IsFalse(AddressValidator.IsValidAddress("abc"));
            Assert.IsFalse(AddressValidator.IsValidAddress("0xd40800cc8b4f853eaea90b2b14b1ddda5511755b"));
            Assert.IsFalse(AddressValidator.IsValidAddress("0xd40800cc8b4f853eaea90d2b14b1ddda5511755b"));

            //Too short
            Assert.IsFalse(AddressValidator.IsValidAddress("ik3eCayInKVNzSMdoxeSEfpxNd5U7tx1H8NAveg4o8"));

            //Too long
            Assert.IsFalse(AddressValidator.IsValidAddress("Arik3eCayInKVNzSMdoxeSEfpxNd5U7tx1H8NAveg4o8"));

            //Invalid Eth
            Assert.IsFalse(AddressValidator.IsValidAddress("0x82B143F46C3F8B4242bA85F800579cdF73882e98"));
            Assert.IsFalse(AddressValidator.IsValidAddress("0x92b143F46C3F8B4242bA85F800579cdF73882e98"));


        }

    }
}