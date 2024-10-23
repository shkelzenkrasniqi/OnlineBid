
namespace Domain.DTOs
{
    public class AuctionReadDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? CurrentPrice { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public string CategoryName { get; set; }
        public List<AuctionPhotoDTO> Photos { get; set; }
    }
}
