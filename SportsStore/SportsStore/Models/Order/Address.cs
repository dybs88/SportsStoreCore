using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models.Order
{
    public class Address
    {
        [BindNever]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Podaj ulicę")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Podaj nr budynku")]
        public string BuildingNumber { get; set; }

        public string ApartmentNumber { get; set; }
        [Required(ErrorMessage = "Podaj nazwę miasta")]
        public string City { get; set; }
        [Required(ErrorMessage = "Podaj województwo")]
        public string Region { get; set; }
        [Required(ErrorMessage = "Podaj kod pocztowy")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Podaj nazwę kraju")]
        public string Country { get; set; }
    }
}
