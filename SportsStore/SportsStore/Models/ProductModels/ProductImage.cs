using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ProductModels
{
    [Table("ProductImages", Schema = "Store")]
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string FileName { get; set; }
        public bool IsMain { get; set; }
    }
}
