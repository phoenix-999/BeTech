using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeTech.Data.Repositories;
using BeTech.Models;
using BeTech.ViewModels.CurrencyModels;
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
        public async Task<IActionResult> UpdateRates(CurrencyData[] currencyData)
        {
            await _currencyRepository.UpdateRatesFromUNBAsync(currencyData);
            return Json(true);
        }


        [HttpGet]
        public IActionResult AddCurrency()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddCurrency(NewCurrencyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _currencyRepository.AddCurrencyAsync(model.CurrencyName, model.Code, model.Rate);

            return RedirectToAction(nameof(Currencies));
        }


        [HttpGet]
        public IActionResult EditCurrency([Required] int currencyId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var currency = _currencyRepository.Currencies.Where(c => c.CurrencyId == currencyId).SingleOrDefault();
            if (currency == default) return NotFound();

            var model = new UpdateCurrencyViewModel(currency);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditCurrency(UpdateCurrencyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var updateResult = await _currencyRepository.UpdateCurrencyAsync(model.CurrencyId, model.CurrencyName, model.Code, model.Rate);
            if (updateResult == null) return NotFound();

            return RedirectToAction(nameof(Currencies));
        }


        [HttpGet]
        public IActionResult DeleteCurrency([Required] int currencyId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var currency = _currencyRepository.Currencies.Where(s => s.CurrencyId == currencyId).SingleOrDefault();
            if (currency == null) return NotFound();

            var model = new DeleteCurrencyViewModel
            {
                CurrencyId = currency.CurrencyId,
                CurrencyName = currency.CurrencyName
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCurrency(DeleteCurrencyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var deletedStock = await _currencyRepository.DeleteCurrencyAsync(model.CurrencyId);
            if (deletedStock == null) return NotFound();

            return RedirectToAction(nameof(Currencies));
        }
    }
}
