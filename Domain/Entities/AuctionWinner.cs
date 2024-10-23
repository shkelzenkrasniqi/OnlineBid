
namespace Domain.Entities
{
    public class AuctionWinner
    {
        public int Id { get; set; }
        public decimal WinningBid { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; }
    }
}
