using SportsStore.Models.DictionaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.DictionarySchema
{
    public interface IVatRateRepository
    {
        IEnumerable<VatRate> VatRates { get; }

        VatRate GetVatRate(int vatRateId);
        dynamic DeleteVatRate(int vatRateId);
        dynamic SaveVatRate(VatRate vatRate);

    }
}
