using SportsStore.Models.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Patterns.Observers
{
    public abstract class BaseObserver
    {
        public virtual void PropertyChanged(object sender, PropertyChangedEventArgs e)
        { }
    }
}
