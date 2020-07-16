using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cart.Entity
{
    public class Campaing
    {
        public Campaing(Category category, double discountRationOrPrice, int countOfProduct, DiscountType discountType)
        {
            Category = category;
            DiscountRationOrPrice = discountRationOrPrice;
            CountOfProduct = countOfProduct;
            DiscountType = discountType;
        }
        public Category Category { get; set; }
        public double DiscountRationOrPrice { get; set; }
        public int CountOfProduct { get; set; }
        public DiscountType DiscountType { get; set; }

        /// <summary>
        /// Kampaya indirim tutarını hesaplar.
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public double calculateDiscountForCart(ShoppingCart cart)
        {
            var countOfProduct = cart.cartProducts.Where(cat => cat.Product.Category.Title == this.Category.Title).Sum(s => s.Count);
            if (this.DiscountType == DiscountType.Amount)
            {
                return countOfProduct > 1 ? this.DiscountRationOrPrice : 0;
            }
            else if (this.DiscountType == DiscountType.Rate)
            {
                double totalAmount = 0;
                foreach (var item in cart.cartProducts.Where(cat => cat.Product.Category.Title == this.Category.Title))
                {
                    totalAmount = totalAmount + item.Product.Price * (double)item.Count;
                }

                return countOfProduct > this.CountOfProduct ? totalAmount * this.DiscountRationOrPrice / 100 : 0;
            }
            return 0;
        }


    }

    /// <summary>
    /// İndim tipini belirleyen enum'dur. Oran ya da tutar'a göre hesaplama yapılmasını belirler.
    /// </summary>
    public enum DiscountType
    {
        Rate = 0,
        Amount = 1
    }
}
