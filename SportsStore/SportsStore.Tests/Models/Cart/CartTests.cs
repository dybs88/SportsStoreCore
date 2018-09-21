using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Models;
using SportsStore.Models.Cart;
using SportsStore.Models.ProductModels;
using SportsStore.Tests.Base;
using Xunit;

namespace SportsStore.Tests
{
    public class CartTests
    {
        private Cart target;
        private IEnumerable<Product> products;

        public CartTests()
        {
            products = Repositories.ProductRepository.Products;

            target = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Product = products.First(p => p.ProductID == 1),
                        Quantity = 1
                    },
                    new CartItem
                    {
                        Product = products.First(p => p.ProductID == 2),
                        Quantity = 2
                    }
                }
            };
        }

        [Fact]
        public void AddNewItemTest()
        {
            //arange

            //act
            target.AddItem(products.First(p => p.ProductID == 3), 1);
            target.AddItem(products.First(p => p.ProductID == 4), 2);

            //assert
            Assert.Equal(4, target.Items.Count);
            Assert.Equal(6, target.Items.Sum(i => i.Quantity));
        }

        [Fact]
        public void RemoveItemTest()
        {
            //arange

            //act
            target.RemoveItem(target.Items.First(i => i.Product.ProductID == 1).Product, 1);
            target.RemoveItem(target.Items.First(i => i.Product.ProductID == 2).Product, 1);
            target.RemoveItem(products.First(p => p.ProductID == 8), 3);

            //assert
            Assert.Equal(1, target.Items.Count);
            Assert.Equal(1, target.Items.Sum(i => i.Quantity));
        }

        [Fact]
        public void CalculateCartTotalValue()
        {
            //arange

            //act
            var currentValue = target.CartValue();
            target.AddItem(products.First(p => p.ProductID == 8), 3);
            var newValue = target.CartValue();

            //assert
            Assert.Equal(50, currentValue);
            Assert.NotEqual(currentValue, newValue);
            Assert.Equal(290, newValue);
        }
    }
}
