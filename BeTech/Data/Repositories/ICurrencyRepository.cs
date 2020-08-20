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
        Task UpdateRatesFromUNBAsync(CurrencyData[] currencyData);
        Task UpdateBasePricesAsync();
        Task<Currency> AddCurrencyAsync(string name, string code, decimal rate);
        Task<Currency> UpdateCurrencyAsync(int currencyId, string name, string code, decimal rate);
        Task<Currency> DeleteCurrencyAsync(int currencyId);
    }


    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext _context;

        public CurrencyRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Currency> Currencies => _context.Currencies.AsNoTracking().Where(c => c.Deleted != true);


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

        public async Task UpdateRatesFromUNBAsync(CurrencyData[] currencyData)
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
            {
                uah.Rate = 1 / baseValue;
                uah.UpdateTime = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            await UpdateBasePricesAsync();
        }

        public async Task UpdateBasePricesAsync()
        {
            var baseCurrency = GetBaseCurrency();
            foreach (var product in _context.Products.Include(p => p.Currency))
            {
                product.PriceInBaseCurrency = product.Price / product.Currency.Rate;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Currency> AddCurrencyAsync(string name, string code, decimal rate)
        {
            var currency = new Currency
            {
                CurrencyName = name,
                Code = code,
                Rate = rate,
                UpdateTime = DateTime.Now
            };

            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();
            return currency;
        }

        public async Task<Currency> UpdateCurrencyAsync(int currencyId, string name, string code, decimal rate)
        {
            var currency = _context.Currencies.Where(c => c.CurrencyId == currencyId).SingleOrDefault();
            if (currency == null) return null;

            currency.CurrencyName = name;
            currency.Code = code;
            currency.Rate = rate;
            currency.UpdateTime = DateTime.Now;

            await _context.SaveChangesAsync();
            await UpdateBasePricesAsync();
            return currency;
        }


        public async Task<Currency> DeleteCurrencyAsync(int currencyId)
        {
            var currency = _context.Currencies.Where(c => c.CurrencyId == currencyId).SingleOrDefault();
            if (currency == null) return null;

            currency.Deleted = true;
            await _context.SaveChangesAsync();
            return currency;
        }
    }

}
