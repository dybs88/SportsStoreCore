using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Cart
{
    public class Cart
    {
        public IList<CartItem> Items = new List<CartItem>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartItem item = null;

            if (Items.Any(i => i.Product.ProductID == product.ProductID))
            {
                item = Items.First(i => i.Product.ProductID == product.ProductID);
                item.Quantity += quantity;
            }
            else
            {
                item = new CartItem {Product = product, Quantity = quantity};

                Items.Add(item);

            }
        }

        public virtual void RemoveItem(Product product, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product.ProductID == product.ProductID);

            if (item != null)
            {
                if (item.Quantity > quantity)
                    item.Quantity -= quantity;
                else
                    Items.Remove(item);
            }
        }

        public virtual decimal CartValue() => Items.Sum(i => i.Value);

        public virtual void ClearCart() => Items.Clear();
    }
}
