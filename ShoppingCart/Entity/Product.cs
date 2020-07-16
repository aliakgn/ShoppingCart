using System;
using System.Collections.Generic;
using System.Text;

namespace Cart.Entity
{
    public class Product
    {
        public Product(string title, double price, Category category)
        {
            Category = category;
            Title = title;
            Price = price;
        }

        public Category Category { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }
}
