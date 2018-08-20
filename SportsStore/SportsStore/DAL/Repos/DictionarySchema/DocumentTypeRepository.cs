using SportsStore.DAL.Contexts;
using SportsStore.Models.DictionaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.DictionarySchema
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private DictionaryDbContext _context;

        public DocumentTypeRepository(DictionaryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DocumentType> DocumentTypes => _context.DocumentTypes;
        public void DeleteDocumentType(int documentTypeId)
        {
            var documentType = _context.DocumentTypes.FirstOrDefault(dt => dt.DocumentTypeId == documentTypeId);

            if (documentType != null)
                _context.DocumentTypes.Remove(documentType);
        }
        public DocumentType GetDocumentType(int documentTypeId)
        {
            return DocumentTypes.FirstOrDefault(dt => dt.DocumentTypeId == documentTypeId);
        }

        public int SaveDocumentType(DocumentType documentType)
        {
            if (documentType.DocumentTypeId == 0)
                _context.Add(documentType);
            else
            {
                var dbEntry = _context.DocumentTypes.FirstOrDefault(dt => dt.DocumentTypeId == documentType.DocumentTypeId);

                if (dbEntry != null)
                {
                    dbEntry.Code = documentType.Code;
                    dbEntry.Symbol = documentType.Symbol;
                    dbEntry.Name = documentType.Name;
                }
            }

            _context.SaveChanges();

            return documentType.DocumentTypeId;
        }

    }
}
