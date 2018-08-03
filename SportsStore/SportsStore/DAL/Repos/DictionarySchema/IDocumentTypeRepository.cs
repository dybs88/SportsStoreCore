using SportsStore.DAL.Contexts;
using SportsStore.Models.DictionaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.DictionarySchema
{
    public interface IDocumentTypeRepository
    {
        IEnumerable<DocumentType> DocumentTypes { get; }
        void DeleteDocumentType(int documentTypeId);
        DocumentType GetDocumentType(int documentTypeId);
        int SaveDocumentType(DocumentType documentType);
    }
}
