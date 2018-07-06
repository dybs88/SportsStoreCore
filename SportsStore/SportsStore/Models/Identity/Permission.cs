using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Identity
{
    [Table("Permissions", Schema = "Security")]
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
