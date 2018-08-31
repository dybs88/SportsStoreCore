using SportsStore.Models.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Patterns.Observers
{
    internal class CartItemObserver : BaseObserver
    {
        private CartItem _obj;

        public override void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.PropertyChanged(sender, e);
            _obj = (CartItem)sender;
            if (e.PropertyName == "Product")
                ProductPropertyChanged();
            if (e.PropertyName == "Quantity")
                QuantityPropertyChanged();
        }

        private void ProductPropertyChanged()
        {
            _obj.Value = _obj.Product.Price * _obj.Quantity;
        }

        private void QuantityPropertyChanged()
        {
            decimal price = 0;
            if (_obj.Product != null)
                price = _obj.Product.Price;

            _obj.Value = price * _obj.Quantity;
        }
    }
}
