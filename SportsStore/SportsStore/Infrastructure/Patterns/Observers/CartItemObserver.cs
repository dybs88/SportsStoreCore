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
            _obj.NetValue = _obj.Product.NetPrice * _obj.Quantity;
            _obj.GrossValue = _obj.GrossValue * _obj.Quantity;
        }

        private void QuantityPropertyChanged()
        {
            if (_obj.Product != null)
            {
                _obj.NetValue = _obj.Product?.NetPrice ?? 0 * _obj.Quantity;
                _obj.GrossValue = _obj.Product?.GrossPrice ?? 0 * _obj.Quantity;
            }
        }
    }
}
