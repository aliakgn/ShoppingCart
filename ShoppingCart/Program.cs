using Cart.Entity;
using System;

namespace Shoppingcart
{
    class Program
    {
        static void Main(string[] args)
        {

            //Categories
            Category category1 = new Category("food");
            Category category2 = new Category("Shoes");

            //Products
            Product product1 = new Product("apple", 4.5, category1);
            Product product2 = new Product("banana", 13.2, category1);
            Product product3 = new Product("tomato", 2.5, category1);
            Product product4 = new Product("potato", 2.4, category1);

            Product product5 = new Product("adidasf50", 300, category2);
            Product product6 = new Product("adidasf60", 500, category2);
            Product product7 = new Product("adidasf70", 472, category2);
            Product product8 = new Product("adidasf80", 685, category2);

            //Campaings
            Campaing campaing1 = new Campaing(category1, 20, 3, DiscountType.Rate);
            Campaing campaing2 = new Campaing(category1, 50, 5, DiscountType.Rate);
            Campaing campaing3 = new Campaing(category1, 5, 5, DiscountType.Amount);


            Campaing campaing4 = new Campaing(category2, 20, 2, DiscountType.Rate);
            Campaing campaing5 = new Campaing(category2, 40, 4, DiscountType.Rate);
            Campaing campaing6 = new Campaing(category2, 10, 5, DiscountType.Amount);


            //Coupon
            Coupon coupon = new Coupon(100, 5, DiscountType.Amount);


            ShoppingCart cart = new ShoppingCart();
            cart.addItem(product1, 1);
            //cart.addItem(product4, 1);
            cart.addItem(product6, 1);
            // cart.addItem(product7, 1);
            //cart.addItem(product3, 2);
            //cart.addItem(product2, 3);
            //cart.addItem(product5, 4);
            //cart.addItem(product8, 2);
            cart.applyDiscounts(campaing1, campaing3, campaing5);
            cart.applyCoupon(coupon);
            cart.print();
            Console.ReadLine();


        }
    }
}
