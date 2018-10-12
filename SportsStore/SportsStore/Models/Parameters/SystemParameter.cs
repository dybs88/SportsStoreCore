using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Parameters
{
    [Table("Parameters", Schema = "Security")]
    public class SystemParameter
    {
        public int SystemParameterId { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
        public ParameterType ParameterType { get; set; }
    }
}
