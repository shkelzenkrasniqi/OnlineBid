
namespace Domain.DTOs
{
    public class BidReadDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }
        public Guid UserId { get; set; }
        public Guid AuctionId { get; set; }
        public string UserName { get; set; }
    }
}
