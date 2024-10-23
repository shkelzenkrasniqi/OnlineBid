using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionReadDTO>()
             .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<AuctionCreateDTO, Auction>();
            CreateMap<AuctionUpdateDTO, Auction>();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<Bid, BidReadDTO>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<BidCreateDTO, Bid>();
            CreateMap<AuctionPhoto, AuctionPhotoDTO>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
        }
    }
}
