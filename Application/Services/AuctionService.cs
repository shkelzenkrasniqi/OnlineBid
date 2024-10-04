﻿using AutoMapper;
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
    public class AuctionService(IAuctionRepository auctionRepository, IMapper mapper) : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository = auctionRepository;
        private readonly IMapper _mapper = mapper;

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

        public async Task<AuctionReadDTO> CreateAuctionAsync(AuctionCreateDTO AuctionCreateDTO)
        {
            var auction = _mapper.Map<Auction>(AuctionCreateDTO);
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
    }
}