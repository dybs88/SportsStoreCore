using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ProductModels
{
    [Table("Products", Schema = "Store")]
    public class Product
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Cena jest wymagana")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią cenę")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Wskazanie kategorii jest wymagane")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        public Product()
        {
            ProductImages = new List<ProductImage>();
        }
    }
}
