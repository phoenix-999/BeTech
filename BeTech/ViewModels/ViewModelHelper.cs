using BeTech.Data.Repositories;
using BeTech.ViewModels.ProductModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTech.ViewModels
{
    public class ViewModelHelper
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IStockRepository _stockRepository;

        public ViewModelHelper(IProductsRepository productRepository, ICurrencyRepository currencyRepository, IStockRepository stockRepository)
        {
            _productRepository = productRepository;
            _currencyRepository = currencyRepository;
            _stockRepository = stockRepository;
        }


        public NewProductViewModel GetNewProductViewModel(NewProductViewModel model=null)
        {
            if (model == null) model = new NewProductViewModel();
            model.Categories = _productRepository.ProductCategories;
            model.Stocks = _stockRepository.Stocks;
            model.Currencies = _currencyRepository.Currencies;

            return model;
        }

        public UpdateProductViewModel GetUpdateProductViewModel(UpdateProductViewModel model = null)
        {
            if (model == null) model = new UpdateProductViewModel();
            model = GetNewProductViewModel(model) as UpdateProductViewModel;

            return model;
        }
    }
}
