using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsStore.Controllers.Base;
using SportsStore.DAL.Repos.DictionarySchema;
using SportsStore.Models.DictionaryModels;

namespace SportsStore.Controllers
{
    public class SalesParametersController : BaseController
    {
        IDictionaryContainer _dictionaryContainer; 

        public SalesParametersController(IServiceProvider provider, IConfiguration config, IDictionaryContainer dictContainer)
            : base(provider, config)
        {
            _dictionaryContainer = dictContainer;
        }

        public IActionResult Index()
        {
            SalesParametersIndexViewModel model = new SalesParametersIndexViewModel
            {
                VatRates = _dictionaryContainer.VatRateRepository.VatRates.ToList()
            };

            return View(model);
        }

        #region VatRates

        public IActionResult SaveVatRate(VatRate vatRate)
        {
            if(ModelState.IsValid && vatRate != null)
            {
                var result = _dictionaryContainer.VatRateRepository.SaveVatRate(vatRate);
                return Json(result.Message);
            }
            else
            {
                return Json("Nie udało się zapisać stawki VAT");
            }
        }

        public IActionResult DeleteVatRate(int vatRateId)
        {
            if(ModelState.IsValid)
            {
                var result = _dictionaryContainer.VatRateRepository.DeleteVatRate(vatRateId);
                return Json(result.Message);
            }
            else
            {
                return Json("Nie udało się usunąć stawki VAT");
            }
        }

        #endregion
    }
}