using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.DictionaryModels
{
    [Table("DocumentTypes", Schema = "Dictionary")]
    public class DocumentType
    {
        public int DocumentTypeId { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
    }
}
