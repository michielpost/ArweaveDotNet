using System.Text.RegularExpressions;

namespace ArweaveAO
{
    public static class AddressValidator
    {
        public static bool IsValidAddress(string address)
        {
            if (IsEthereumAddress(address))
                return IsValidEthereumAddress(address);
            else
                return IsValidArweaveAddress(address);
        }

        public static bool IsEthereumAddress(string address)
        {
            return address.StartsWith("0x");
        }

        public static bool IsValidArweaveAddress(string address)
        {
            if (address.Length != 43)
                return false;

            string pattern = @"^[a-zA-Z0-9_\-]{0,43}$";

            return Regex.IsMatch(address, pattern);
        }

        public static bool IsValidEthereumAddress(string address)
        {
            var addressUtil = new Nethereum.Util.AddressUtil();

            var validLength = addressUtil.IsValidAddressLength(address);
            if (!validLength)
                return false;

            return addressUtil.IsChecksumAddress(address);
        }
    }
}
