using Domain.DTOs;

namespace Application.Services
{
    public interface IBidService
    {
        Task<IEnumerable<BidReadDTO>> GetAllBidsForAuctionAsync(Guid auctionId);
        Task<IEnumerable<BidReadDTO>> GetAllBidsAsync();
        Task<BidReadDTO> GetBidByIdAsync(Guid id);
        Task<BidReadDTO> CreateBidAsync(BidCreateDTO BidCreateDTO);
        Task<bool> DeleteBidAsync(Guid id);
    }
}
