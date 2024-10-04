﻿using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuctionService
    {
        Task<IEnumerable<AuctionReadDTO>> GetAllAuctionsAsync();
        Task<AuctionReadDTO> GetAuctionByIdAsync(Guid id);
        Task<AuctionReadDTO> CreateAuctionAsync(AuctionCreateDTO AuctionCreateDTO);
        Task<bool> UpdateAuctionAsync(Guid id, AuctionUpdateDTO AuctionUpdateDTO);
        Task<bool> DeleteAuctionAsync(Guid id);
    }
}