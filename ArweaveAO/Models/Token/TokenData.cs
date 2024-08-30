using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArweaveAO.Models.Token
{
    public class TokenData
    {
        public string? TokenId { get; set; }
        public string? Name { get; set; }
        public string? Ticker { get; set; }
        public string? Logo { get; set; }
        public int? Denomination { get; set; }

        public TokenType TokenType { get; set; }
        public bool Transferable { get; set; } = true;

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(TokenId) && (!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Ticker));
        }

        public bool IsValidToken()
        {
            return IsValid() && TokenType == TokenType.Token;
        }
    }

    public enum TokenType
    {
        Token,
        AtomicAsset
    }
}
