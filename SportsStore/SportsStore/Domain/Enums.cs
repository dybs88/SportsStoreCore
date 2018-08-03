using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Domain
{
    public enum PageMode
    {
        Edit,
        View
    }

    public enum DocumentTypes
    {
        SalesOrder,
        Receipt,
        SalesInvoice
    }
}
