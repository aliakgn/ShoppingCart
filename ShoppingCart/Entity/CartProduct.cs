using System;
using System.Collections.Generic;
using System.Text;

namespace Cart.Entity
{
    public class CartProduct
    {
        public CartProduct(Product product, int count)
        {
            Product = product;
            Count = count;
        }

        public Product Product { get; set; }
        public int Count { get; set; }

    }
}
