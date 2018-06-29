using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ComponentViewModels
{
    public class QuickMenuViewModel
    {
        public Cart.Cart Cart { get; set; }

        [Required(ErrorMessage = "Login jest wymagany")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [UIHint("Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
        public bool IsUserLogged { get; set; }
        public IdentityUser LoggedUser { get; set; }
    }
}
