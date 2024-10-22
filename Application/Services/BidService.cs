using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BidService(IBidRepository bidRepository, IMapper mapper,IAuctionRepository auctionRepository) : IBidService
    {
        private readonly IBidRepository _bidRepository = bidRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<BidReadDTO>> GetAllBidsAsync()
        {
            var bids = await _bidRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BidReadDTO>>(bids);
        }

        public async Task<BidReadDTO> GetBidByIdAsync(Guid id)
        {
            var bid = await _bidRepository.GetByIdAsync(id);
            return _mapper.Map<BidReadDTO>(bid);
        }

        public async Task<BidReadDTO> CreateBidAsync(BidCreateDTO bidCreateDTO)
        {
            var auction = await auctionRepository.GetByIdAsync(bidCreateDTO.AuctionId);

            if (auction == null)
            {
                throw new Exception("Auction not found.");
            }

            if (!auction.IsActive)
            {
                throw new Exception("Cannot place a bid. The auction is not active.");
            }
            decimal minimumBid = auction.CurrentPrice ?? auction.StartingPrice;

            if (bidCreateDTO.Amount <= minimumBid)
            {
                throw new Exception("Cannot place a bid lower than or equal to the minimum price.");
            }
            auction.CurrentPrice = bidCreateDTO.Amount;
            await auctionRepository.UpdateAsync(auction);

            var bid = _mapper.Map<Bid>(bidCreateDTO);
            await _bidRepository.AddAsync(bid);
            return _mapper.Map<BidReadDTO>(bid);
        }

        public async Task<bool> DeleteBidAsync(Guid id)
        {
            var bid = await _bidRepository.GetByIdAsync(id);
            if (bid == null) return false;

            await _bidRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<BidReadDTO>> GetAllBidsForAuctionAsync(Guid auctionId)
        {
            var bids = await _bidRepository.GetAllForAuctionAsync(auctionId);

            var reversedBids = bids.Reverse();

            var bidReadDTOs = _mapper.Map<IEnumerable<BidReadDTO>>(reversedBids);

            return bidReadDTOs;
        }
    }
}
