using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login jest wymagany")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [UIHint("Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
