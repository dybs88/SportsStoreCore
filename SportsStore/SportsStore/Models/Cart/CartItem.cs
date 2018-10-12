using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Infrastructure.Patterns.Observers;
using SportsStore.Models.OrderModels;
using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SportsStore.Models.Cart
{
    [Table("Items", Schema = "Sales")]
    public class CartItem : INotifyPropertyChanged
    {
        private Product _product;
        private int _quantity;

        public int CartItemId { get; set; }
        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }
        public decimal NetValue { get; set; }
        public decimal GrossValue { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [BindNever]
        private static BaseObserver EntityObserver => new CartItemObserver();

        public event PropertyChangedEventHandler PropertyChanged = EntityObserver.PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
