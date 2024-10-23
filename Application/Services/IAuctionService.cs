using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuctionService
    {
        Task<IEnumerable<AuctionReadDTO>> SearchAuctionsAsync(string searchTerm);
        Task<IEnumerable<AuctionReadDTO>> GetAllAuctionsAsync();
        Task<AuctionReadDTO> GetAuctionByIdAsync(Guid id);
        Task<AuctionReadDTO> CreateAuctionAsync(AuctionCreateDTO AuctionCreateDTO, List<IFormFile> photos);
        Task<bool> UpdateAuctionAsync(Guid id, AuctionUpdateDTO AuctionUpdateDTO);
        Task<bool> DeleteAuctionAsync(Guid id);
    }
}
