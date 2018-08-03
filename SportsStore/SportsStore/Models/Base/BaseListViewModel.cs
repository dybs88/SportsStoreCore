using SportsStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Base
{
    public class BaseListViewModel
    {
        public string SearchData { get; set; }

        public PageHelper PageModel { get; set; }
    }

}
