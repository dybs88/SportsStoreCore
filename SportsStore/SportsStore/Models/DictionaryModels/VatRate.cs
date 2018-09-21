using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.DictionaryModels
{
    [Table("VatRates", Schema = "Dictionary")]
    public class VatRate
    {
        public int VatRateId { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public decimal Value { get; set; }
        public bool IsDefault { get; set; }

        public string FullName { get { return $"{Symbol} {Value}%"; } }
    }
}
