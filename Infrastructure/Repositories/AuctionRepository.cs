using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    internal sealed class AuctionRepository(ApplicationDbContext _context) : IAuctionRepository
    {

        public async Task<IEnumerable<Auction>> GetAllAsync()
        {
            return await _context.Auctions.ToListAsync();
        }

        public async Task<Auction> GetByIdAsync(Guid id)
        {
            return await _context.Auctions.FindAsync(id);
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
