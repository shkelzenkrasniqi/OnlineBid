using System.Text.Json.Serialization;

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
