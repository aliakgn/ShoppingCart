using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cart.Entity
{
    public class Coupon
    {
        public Coupon(double minAmountOfCart, double discountRationOrPrice, DiscountType discountType)
        {
            DiscountRationOrPrice = discountRationOrPrice;
            MinAmountOfCart = minAmountOfCart;
            DiscountType = discountType;
        }

        public double DiscountRationOrPrice { get; set; }
        public double MinAmountOfCart { get; set; }
        public DiscountType DiscountType { get; set; }

        public double calculateDiscountForCart(ShoppingCart cart)
        {
            var totalPriceOfCart = cart.getTotalAmountWithoutDiscount();
            totalPriceOfCart = totalPriceOfCart - cart.getCampaignDiscount();
            if (totalPriceOfCart < this.MinAmountOfCart)
            {
                Console.WriteLine("Kupon için sepet tutarı yetersiz.");
                return 0;
            }
            if (this.DiscountType == DiscountType.Amount)
            {
                return this.DiscountRationOrPrice;
            }
            else if (this.DiscountType == DiscountType.Rate)
            {
                return totalPriceOfCart * this.DiscountRationOrPrice / 100;
            }
            return 0;
        }
    }
}
