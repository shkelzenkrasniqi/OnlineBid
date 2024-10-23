using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<Auction>> SearchAuctionsAsync(string searchTerm);
        Task<IEnumerable<Auction>> GetAllAsync();
        Task<Auction> GetByIdAsync(Guid id);
        Task AddAsync(Auction auction);
        Task UpdateAsync(Auction auction);
        Task DeleteAsync(Guid id);
    }
}
