using BeTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Data.Repositories
{
    public interface ICurrencyRepository
    {
        IQueryable<Currency> Currencies { get; }
        Currency GetBaseCurrency();
    }


    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;

        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Currency> Currencies => _context.Currencies.AsNoTracking();


        public Currency GetBaseCurrency()
        {
            return _context.Currencies.AsNoTracking().Where(c => c.IsBaseCurrencyType).First();
        }
    }

}
