using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class RepositoriesDIConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
