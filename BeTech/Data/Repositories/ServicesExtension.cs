using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Data.Repositories
{
    public static class ServicesExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductsRepository, ProductRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
        }
    }
}
