using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cart.Entity
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.cartProducts = new List<CartProduct>();
            this.cartCampaingAdded = false;
            this.cartCouponAdded = false;
        }

        #region properties
        public List<CartProduct> cartProducts { get; set; }
        private Campaing cartCampaing { get; set; }
        private bool cartCampaingAdded { get; set; }
        private Coupon cartCoupon { get; set; }
        private bool cartCouponAdded { get; set; }
        private double cartProductsAmount { get; set; }
        private double deliveryAmount { get; set; }
        #endregion

        #region Functions
        /// <summary>
        /// Sepete yeni ürün ekleyen metottur.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="count"></param>
        public void addItem(Product product, int count)
        {
            this.cartProducts.Add(new CartProduct(product, count));
        }
        /// <summary>
        /// En fazla indirim uygulanan kampyayayı sepete uygular.
        /// </summary>
        /// <param name="discount"></param>
        /// <param name="discount1"></param>
        /// <param name="discount2"></param>
        public void applyDiscounts(Campaing discount, Campaing discount1, Campaing discount2)
        {
            this.cartCampaing =
            getCampaingMaxDiscountAmount(getCampaingMaxDiscountAmount(discount, discount1), discount2);
            this.cartCampaingAdded = true;
        }
        /// <summary>
        /// Kuponu sepete uygular
        /// </summary>
        /// <param name="coupon"></param>
        public void applyCoupon(Coupon coupon)
        {
          
            if (coupon.calculateDiscountForCart(this) < getTotalAmountAfterDiscount())
            {
                this.cartCoupon = coupon;
                this.cartCouponAdded = true;
            }
        }
        /// <summary>
        /// Sepet'i yazdırı.
        /// </summary>
        public void print()
        {

            foreach (var category in cartProducts.Select(s => s.Product.Category).Distinct().OrderBy(o => o.Title))
            {
                Console.WriteLine(category.Title);
                foreach (var product in cartProducts.Where(w => w.Product.Category.Title == category.Title).OrderBy(o => o.Product.Title))
                {
                    Console.WriteLine("Ürün :" + product.Product.Title + "   Birim Fiyat:" + product.Product.Price.ToString() + " Adet: " + product.Count + "  Toplam: " + product.Product.Price * product.Count);
                }
            }
            Console.WriteLine("Sepet Toplam İndirimsiz:" + getTotalAmountWithoutDiscount().ToString());
            Console.WriteLine("Sepet Toplam İndirimli:" + getTotalAmountAfterDiscount().ToString());
            Console.WriteLine("Toplam İndirim :" + (getCouponDiscount() + getCampaignDiscount()).ToString());
            Console.WriteLine("Kargo Bedeli:" + getDeliveryCost());
            Console.WriteLine("Genel Toplam :" + (getTotalAmountAfterDiscount() + getDeliveryCost()).ToString());

        }
        /// <summary>
        /// İndirimsiz sepet tutarını verir.
        /// </summary>
        /// <returns></returns>
        public double getTotalAmountWithoutDiscount()
        {
            double totalPrice = 0;
            foreach (var item in this.cartProducts)
            {
                totalPrice = totalPrice + (item.Product.Price * item.Count);
            }
            return totalPrice;
        }
        /// <summary>
        /// Kampanya ve kupon indirimleri uygulanmış sepet tutarını döner
        /// </summary>
        /// <returns></returns>
        private double getTotalAmountAfterDiscount()
        {
            double totalPriceAfterDiscount = 0;
            if (this.cartCampaingAdded)
            {
                totalPriceAfterDiscount = getTotalAmountWithoutDiscount() - getCampaignDiscount();
            }
            if (this.cartCouponAdded)
            {
                totalPriceAfterDiscount = totalPriceAfterDiscount - getCouponDiscount();
            }
            return totalPriceAfterDiscount;
        }
        /// <summary>
        /// Sepete Uygulanmış Kupon indirim tutarını döner.
        /// </summary>
        /// <returns></returns>
        public double getCouponDiscount()
        {
            if (this.cartCouponAdded && this.cartCoupon != null)
            {
                return this.cartCoupon.calculateDiscountForCart(this);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Sepete Uygulanmış Kampanya idirim tutarını döner.
        /// </summary>
        /// <returns></returns>
        public double getCampaignDiscount()
        {
            if (this.cartCampaingAdded && this.cartCampaing != null)
            {
                return this.cartCampaing.calculateDiscountForCart(this);
            }
            else
            {
                throw new Exception("Kampanya eklenmeden bu işlem yapılamaz.");
            }
        }
        /// <summary>
        /// Kargo tutarını döner
        /// </summary>
        /// <returns></returns>
        private double getDeliveryCost()
        {
            DeliveryCostCalculator deliveryCostCalculator = new DeliveryCostCalculator(2.99, 2, 3);
            return deliveryCostCalculator.calculateForCart(this);
        }
        /// <summary>
        /// 2 kampanyadan maximum indirim tutarı olanı döner.
        /// </summary>
        /// <param name="campaing"></param>
        /// <param name="campaing1"></param>
        /// <returns></returns>
        private Campaing getCampaingMaxDiscountAmount(Campaing campaing, Campaing campaing1)
        {
            return campaing.calculateDiscountForCart(this) > campaing1.calculateDiscountForCart(this) ? campaing : campaing1;
        }


        #endregion


    }
}
