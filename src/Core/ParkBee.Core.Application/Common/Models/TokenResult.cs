using System;

namespace ParkBee.Core.Application.Common.Models
{
    public class TokenResult
    {
        public string Token { get; set; }

        public DateTime? Expiration { get; set; }
    }
}
