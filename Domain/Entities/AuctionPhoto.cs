using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuctionPhoto
    {
        public Guid Id { get; set; }
        public byte[] PhotoData { get; set; }
        public string ContentType { get; set; }
        public Guid AuctionId { get; set; }
        [JsonIgnore]
        public Auction Auction { get; set; }
    }
}
