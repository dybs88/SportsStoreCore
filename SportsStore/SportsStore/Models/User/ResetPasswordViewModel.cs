using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.User
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Podaj hasło")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Niepoprawne hasło. Minimalna długość hasła to 8 znaków. Hasło powinno zawierać jedną dużą literę, jedną cyfrę i jeden znak specjalny")]
        public string Password { get; set; }
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        [Required(ErrorMessage = "Podaj hasło")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Niepoprawne hasło. Minimalna długość hasła to 8 znaków. Hasło powinno zawierać jedną dużą literę, jedną cyfrę i jeden znak specjalny")]
        public string RepeatedPassword { get; set; }
    }
}
