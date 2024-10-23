using Application.Services;
using Application.Validations;
using Domain.DTOs;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public class ServicesDIConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IValidator<LoginDTO>, LoginValidator>();
            services.AddScoped<IValidator<RegisterDTO>, RegisterValidator>();
            services.AddScoped<IValidator<AuctionCreateDTO>, AuctionValidator>();
            services.AddScoped<IValidator<BidCreateDTO>, BidValidator>();
        }
    }
}
