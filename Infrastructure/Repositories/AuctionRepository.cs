using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class AuctionRepository(ApplicationDbContext _context) : IAuctionRepository
    {
        public async Task<IEnumerable<Auction>> SearchAuctionsAsync(string searchTerm)
        {
            var query = _context.Auctions.Include(c => c.Photos).Include(a => a.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(a => a.Title.Contains(searchTerm) || a.Description.Contains(searchTerm));
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Auction>> GetAllAsync()
        {
            return await _context.Auctions.Include(c => c.Photos).ToListAsync();
        }

        public async Task<Auction> GetByIdAsync(Guid id)
        {
            return await _context.Auctions
                .Include(a => a.Bids)
                .Include(c => c.Photos)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task AddAsync(Auction auction)
        {
            await _context.Auctions.AddAsync(auction);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Auction auction)
        {
            _context.Auctions.Update(auction);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var auction = await GetByIdAsync(id);
            if(auction != null)
            {
                _context.Auctions.Remove(auction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
