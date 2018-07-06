using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Metadata
{
    [ModelMetadataType(typeof(IdentityRole))]
    public class IdentityRoleMetadata
    {
        [Required(ErrorMessage = "Nazwa roli jest wymagana")]
        public string Name { get; set; }
    }
}
