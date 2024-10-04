using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAuctionRepository
    {

        Task<IEnumerable<Auction>> GetAllAsync();
        Task<Auction> GetByIdAsync(Guid id);
        Task AddAsync(Auction auction);
        Task UpdateAsync(Auction auction);
        Task DeleteAsync(Guid id);
    }
}
