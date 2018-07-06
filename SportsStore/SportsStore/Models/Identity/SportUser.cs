using Microsoft.AspNetCore.Identity;
using SportsStore.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Identity
{
    [Table("Users", Schema = "Security")]
    public class SportUser : IdentityUser
    {
        [Required(ErrorMessage = "Podaj swoje imię")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Podaj swoje nazwisko")]
        public string LastName { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreationDate { get; set; }

        public SportUser() : base()
        {
            CreationDate = DateTime.Now;
        }

        public SportUser(string userName) : base (userName)
        {
            CreationDate = DateTime.Now;
        }
    }
}
