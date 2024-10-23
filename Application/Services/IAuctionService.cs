using Domain.DTOs;
using Microsoft.AspNetCore.Http;

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
