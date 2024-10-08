using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }
    }
}
