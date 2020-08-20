using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeTech.Data.Repositories;
using BeTech.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeTech.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }


        public IActionResult Currencies()
        {
            return View(_currencyRepository.Currencies);
        }


        public IActionResult NeedUpdateCurrency()
        {
            return Json(_currencyRepository.NeedUpdateCurrency());
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCurrencies(CurrencyData[] currencyData)
        {
            await _currencyRepository.UpdateRatesFromUNB(currencyData);
            return Json(true);
        }
    }
}
