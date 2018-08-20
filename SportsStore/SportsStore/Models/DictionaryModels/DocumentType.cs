using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.DictionaryModels
{
    [Table("DocumentTypes", Schema = "Dictionary")]
    public class DocumentType
    {
        public int DocumentTypeId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DocumentKind DocumentKind { get; set; }
    }
}
