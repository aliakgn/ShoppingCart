using System;
using System.Collections.Generic;
using System.Text;

namespace Cart.Entity
{
    public class Category
    {
        public Category(string title)
        {
            Title = title;
        }

        public Category(string title, Category parentCategory)
        {
            Title = title;
            ParentCategory = parentCategory;
        }

        public string Title { get; set; }

        public Category ParentCategory { get; set; }
    }
}
