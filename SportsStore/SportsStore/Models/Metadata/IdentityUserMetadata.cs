using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Models.Metadata
{
    [ModelMetadataType(typeof(IdentityUser))]
    public class IdentityUserMetadata
    {
        [Required(ErrorMessage = "Podaj nazwę użytkownika")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
    }
}
