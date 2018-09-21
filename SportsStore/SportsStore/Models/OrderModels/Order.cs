using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Infrastructure.Patterns.Observers;
using SportsStore.Models.Cart;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.DocumentModels;

namespace SportsStore.Models.OrderModels
{
    [Table("SalesOrders", Schema = "Sales")]
    public class Order : Document
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        public bool GiftWrap { get; set; }
        public bool Shipped { get; set; }    
        public decimal NetValue { get; set; }
        public decimal GrossValue { get; set; }
        [ForeignKey("VatRate")]
        public int VatRateId { get; set; }

        public Order()
        {
            Items = new ObservableCollection<CartItem>();
            //Items.CollectionChanged += Items_CollectionChanged;
        }

        public Order(int customerId) :this()
        {
            CustomerId = customerId;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var items = (ObservableCollection<CartItem>)sender;
            NetValue = items.Sum(i => i.Value);
        }
    }
}
