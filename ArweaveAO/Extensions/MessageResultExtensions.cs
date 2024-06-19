using ArweaveAO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArweaveAO.Extensions
{
    public static class MessageResultExtensions
    {
        public static string? GetFirstTagValue(this MessageResult messageResult, string tag)
        {
            return messageResult.Messages.FirstOrDefault()?.Tags.Where(x => x.Name == tag).Select(x => x.Value).FirstOrDefault();
        }
    }
}
