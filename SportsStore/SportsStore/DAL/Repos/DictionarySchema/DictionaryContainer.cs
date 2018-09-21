using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.DictionarySchema
{
    public class DictionaryContainer : IDictionaryContainer
    {
        private IDocumentTypeRepository _documentTypeRepository;
        private IVatRateRepository _vatRateRepository;

        public IDocumentTypeRepository DocumentTypeRepository { get { return _documentTypeRepository; } }
        public IVatRateRepository VatRateRepository { get { return _vatRateRepository; } }

        public DictionaryContainer(IDocumentTypeRepository documentTypeRepo, IVatRateRepository vatRateRepo)
        {
            _documentTypeRepository = documentTypeRepo;
            _vatRateRepository = vatRateRepo;
        }
    }
}
