using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IBidRepository
    {
        Task<IEnumerable<Bid>> GetAllForAuctionAsync(Guid auctionId);
        Task<IEnumerable<Bid>> GetAllAsync();
        Task<Bid> GetByIdAsync(Guid id);
        Task AddAsync(Bid bid);
        Task UpdateAsync(Bid bid);
        Task DeleteAsync(Guid id);
    }
}
