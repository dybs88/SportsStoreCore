using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Models
{
    public class CreateUserViewModel
    {
        public IdentityUser User { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Niepoprawne hasło. Hasło powinno zawierać jedną dużą literę, jedną cyfrę i jeden znak specjalny")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Powtórz hasło")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Niepoprawne hasło. Hasło powinno zawierać jedną dużą literę, jedną cyfrę i jeden znak specjalny")]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        public string RepeatedPassword { get; set; }
        public IEnumerable<IdentityRole> AvaibleRoles { get; set; }
        public IEnumerable<string> AddedRolesIds { get; set; }
        public string ReturnUrl { get; set; }
    }
}
