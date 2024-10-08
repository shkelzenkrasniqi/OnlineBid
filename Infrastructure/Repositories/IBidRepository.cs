using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBidRepository
    {
        Task<IEnumerable<Bid>> GetAllAsync();
        Task<Bid> GetByIdAsync(Guid id);
        Task AddAsync(Bid bid);
        Task UpdateAsync(Bid bid);
        Task DeleteAsync(Guid id);
    }
}
