﻿using System;
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

    public enum DocumentKind
    {
        SalesOrder,
        Receipt,
        SalesInvoice
    }

    public enum ParameterType
    {
        Sales
    }

    public enum PriceType
    {
        Net,
        Gross
    }
}
