using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class BidRepository(ApplicationDbContext _context) : IBidRepository
    {
        public async Task<IEnumerable<Bid>> GetAllAsync()
        {
           return await _context.Bids.ToListAsync();
        }

        public async Task<Bid> GetByIdAsync(Guid id)
        {
            return await _context.Bids.FindAsync(id);
        }
        public async Task AddAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Bid bid)
        {
            _context.Bids.Update(bid);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var bid = await GetByIdAsync(id);
            if(bid != null)
            {
                _context.Bids.Remove(bid);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Bid>> GetAllForAuctionAsync(Guid auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionId == auctionId)
                                      .Include(b => b.User)
                                      .ToListAsync();
        }
    }
}
