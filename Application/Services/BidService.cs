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
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IMapper _mapper;

        public BidService(IBidRepository bidRepository, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _mapper = mapper;
        }

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
    }
}
