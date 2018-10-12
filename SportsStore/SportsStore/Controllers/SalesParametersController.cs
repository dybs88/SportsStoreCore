using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsStore.Controllers.Base;
using SportsStore.DAL.Repos.DictionarySchema;
using SportsStore.DAL.Repos.Security;
using SportsStore.Domain;
using SportsStore.Infrastructure.Exceptions;
using SportsStore.Models.DictionaryModels;

namespace SportsStore.Controllers
{
    public class SalesParametersController : BaseController
    {
        IDictionaryContainer _dictionaryContainer;
        ISystemParameterRepository _paramRepository;

        public SalesParametersController(IServiceProvider provider, IConfiguration config, IDictionaryContainer dictContainer, ISystemParameterRepository paramRepo)
            : base(provider, config)
        {
            _dictionaryContainer = dictContainer;
            _paramRepository = paramRepo;
        }

        public IActionResult Index()
        {
            SalesParametersIndexViewModel model = new SalesParametersIndexViewModel
            {
                VatRates = _dictionaryContainer.VatRateRepository.VatRates.OrderBy(vr => vr.Symbol).ToList(),
                SelectedPriceType = _paramRepository.GetParameter(SystemParameters.ProductPriceType).Value
            };

            return View(model);
        }

        #region VatRates
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SaveVatRate(VatRate vatRate)
        {
            if(ModelState.IsValid && vatRate != null)
            {
                var result = _dictionaryContainer.VatRateRepository.SaveVatRate(vatRate);
                return Json(result);
            }
            else
            {
                return Json("Niepoprawne dane wejściowe. Nie udało się zapisać stawki VAT");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
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

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult SavePriceType(string value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _paramRepository.SaveParameter(SystemParameters.ProductPriceType, value);
                    return Json(new { message = "Zapisano" });
                }
                catch (ParameterNotExistException e)
                {
                    Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    return Json(new { message = e.Message });
                }
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Json(new { message = "Niepoprawny format danych" });
            }

        }
    }
}