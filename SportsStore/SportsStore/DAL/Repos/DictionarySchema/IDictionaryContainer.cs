using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.DictionarySchema
{
    public interface IDictionaryContainer
    {
        IDocumentTypeRepository DocumentTypeRepository { get; }
        IVatRateRepository VatRateRepository { get; }
    }
}
