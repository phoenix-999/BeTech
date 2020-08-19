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
            services.AddTransient<IProductsRepository, ProductRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        }
    }
}
