using SportsStore.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Patterns.Observers
{
    public class OrderObserver : BaseObserver
    {
        private Order _obj;
        public override void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.PropertyChanged(sender, e);
            _obj = (Order)sender;

            if (e.PropertyName == "Items")
                ItemsPropertyChanged();
        }

        private void ItemsPropertyChanged()
        {
            _obj.NetValue = _obj.Items.Sum(i => i.Value);
        }
    }
}
