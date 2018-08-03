using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Abstract
{
    public interface ISearchable
    {
        Task<IActionResult> List(int currentPage);

        Task<IActionResult> List(BaseListViewModel model);
    }
}
