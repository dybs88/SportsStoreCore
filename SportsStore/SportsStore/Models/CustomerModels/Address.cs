using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models.CustomerModels
{
    [Table("Addresses", Schema = "Customer")]
    public class Address
    {
        [BindNever]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Podaj ulicę")]
        [MaxLength(75, ErrorMessage = "Maksymalna długość - 75 znaków")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Podaj nr budynku")]
        [MaxLength(6, ErrorMessage = "Maksymalna długość - 6 znaków")]
        public string BuildingNumber { get; set; }
        [MaxLength(6, ErrorMessage = "Maksymalna długość - 6 znaków")]
        public string ApartmentNumber { get; set; }
        [Required(ErrorMessage = "Podaj nazwę miasta")]
        [MaxLength(75, ErrorMessage = "Maksymalna długość - 75 znaków")]
        public string City { get; set; }
        [Required(ErrorMessage = "Podaj województwo")]
        [MaxLength(75, ErrorMessage = "Maksymalna długość - 75 znaków")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Podaj kod pocztowy")]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Należy podać poprawny kod pocztowy XX-XXX")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Podaj nazwę kraju")]
        [MaxLength(75, ErrorMessage = "Maksymalna długość - 75 znaków")]
        public string Country { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
