using SportsStore.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.CustomerModels
{
    [Table("Customers", Schema = "Customer")]
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Podaj swoje imie")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Podaj swoje nazwisko")]
        public string LastName { get; set; }
        [MinLength(9, ErrorMessage = "Niepoprawna długość numeru telefonu")]
        [MaxLength(9, ErrorMessage = "Niepoprawna długość numeru telefonu")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Customer()
        { }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
