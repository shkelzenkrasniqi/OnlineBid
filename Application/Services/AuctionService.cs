using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AuctionService(IAuctionRepository _auctionRepository, IMapper _mapper) : IAuctionService
    {
        public async Task<IEnumerable<AuctionReadDTO>> SearchAuctionsAsync(string searchTerm)
        {
            var auctions = await _auctionRepository.SearchAuctionsAsync(searchTerm);
            return _mapper.Map<IEnumerable<AuctionReadDTO>>(auctions);
        }
        public async Task<IEnumerable<AuctionReadDTO>> GetAllAuctionsAsync()
        {
            var auctions = await _auctionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuctionReadDTO>>(auctions);
        }

        public async Task<AuctionReadDTO> GetAuctionByIdAsync(Guid id)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            return _mapper.Map<AuctionReadDTO>(auction);
        }
        public async Task<AuctionReadDTO> CreateAuctionAsync(AuctionCreateDTO AuctionCreateDTO, List<IFormFile> photos)
        {
            var auction = _mapper.Map<Auction>(AuctionCreateDTO);
            await AddPhotosToAuctionAsync(auction, photos);

            await _auctionRepository.AddAsync(auction);
            return _mapper.Map<AuctionReadDTO>(auction);
        }

        public async Task<bool> UpdateAuctionAsync(Guid id, AuctionUpdateDTO AuctionUpdateDTO)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null) return false;

            _mapper.Map(AuctionUpdateDTO, auction);
            await _auctionRepository.UpdateAsync(auction);
            return true;
        }

        public async Task<bool> DeleteAuctionAsync(Guid id)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null) return false;

            await _auctionRepository.DeleteAsync(id);
            return true;
        }
        private async Task AddPhotosToAuctionAsync(Auction auction, List<IFormFile> photos)
        {
            if (photos != null && photos.Count > 0)
            {
                auction.Photos = new List<AuctionPhoto>();

                foreach (var photo in photos)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);
                        auction.Photos.Add(new AuctionPhoto
                        {
                            PhotoData = memoryStream.ToArray(),
                            ContentType = photo.ContentType
                        });
                    }
                }
            }
        }
    }
}
