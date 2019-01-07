using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Helpers
{
    public interface IAppSettings
    {
        string WebClientAddress { get; set; }
        string Secret { get; set; }
    }
}
