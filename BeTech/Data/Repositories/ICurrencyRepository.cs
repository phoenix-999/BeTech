using BeTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.Data.Repositories
{
    public interface ICurrencyRepository
    {
        IQueryable<Currency> Currencies { get; }
        Currency GetBaseCurrency();
        bool NeedUpdateCurrency();
        Task UpdateRatesFromUNB(CurrencyData[] currencyData);
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


        public bool NeedUpdateCurrency()
        {
            var baseCurrencyUpdateTime = GetBaseCurrency().UpdateTime;
            if (DateTime.Now.Date > baseCurrencyUpdateTime.Date)
                return true;
            return false;
        }

        public async Task UpdateRatesFromUNB(CurrencyData[] currencyData)
        {
            var existsCurrencies = _context.Currencies;

            var baseCurrency = existsCurrencies.Where(c => c.IsBaseCurrencyType).Single();
            baseCurrency.Rate = 1;

            var baseValue = Convert.ToDecimal(currencyData.Where(c => c.cc == baseCurrency.Code).Select(c => c.rate).Single(), CultureInfo.InvariantCulture);


            foreach (var currency in existsCurrencies)
            {
                var updateCurrencyData = currencyData.Where(c => c.cc == currency.Code).FirstOrDefault();
                if (updateCurrencyData == null) continue;
                currency.Rate = Convert.ToDecimal(updateCurrencyData.rate, CultureInfo.InvariantCulture) / baseValue;
                currency.UpdateTime = DateTime.Now;
            }

            var uah = existsCurrencies.Where(c => c.Code == "UAH").SingleOrDefault();
            if (uah != null)
                uah.Rate = 1 / baseValue;

            await _context.SaveChangesAsync();
        }
    }

}
