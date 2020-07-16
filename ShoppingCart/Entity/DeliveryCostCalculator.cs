using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cart.Entity
{
    public class DeliveryCostCalculator
    {
        public DeliveryCostCalculator(double fixedCost, double costPerDelivery, double costPerProduct)
        {
            this.fixedCost = fixedCost;
            this.costPerDelivery = costPerDelivery;
            this.costPerProduct = costPerProduct;
        }

        private double fixedCost { get; set; }
        private double costPerDelivery { get; set; }
        private double costPerProduct { get; set; }

        public double calculateForCart(ShoppingCart cart)
        {
            int numberofProduct =
            cart.cartProducts.Select(s => s.Product.Title).Distinct().Count();
            int numberofdelivery =
            cart.cartProducts.Select(s => s.Product.Category.Title).Distinct().Count();
            return (this.costPerDelivery * numberofdelivery) + (this.costPerProduct * numberofProduct) + fixedCost;
        }
    }
}
