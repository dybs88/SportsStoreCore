using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SportsStore.Helpers;

namespace SportsStore.ApiControllers.Base
{
    public abstract class BaseApiController : Controller
    {
        protected AppSettings _appSettings;

        public BaseApiController(IOptions<AppSettings> settings)
        {
            _appSettings = settings.Value;
        }
    }
}