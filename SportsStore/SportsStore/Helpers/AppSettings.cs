﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Helpers
{
    public class AppSettings : IAppSettings
    {
        public string WebClientAddress { get; set; }
        public string Secret { get; set; }
    }
}
