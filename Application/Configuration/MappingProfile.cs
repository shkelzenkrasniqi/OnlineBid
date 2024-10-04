using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionReadDTO>();
            CreateMap<AuctionCreateDTO, Auction>();
            CreateMap<AuctionUpdateDTO, Auction>();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
