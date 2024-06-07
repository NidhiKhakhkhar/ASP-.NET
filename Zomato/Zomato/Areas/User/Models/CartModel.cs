namespace Zomato.Areas.User.Models
{
    public class CartModel
    {
        public int CartID { get; set; }

        public int UserID { get; set; }

        public int MenuID { get; set; }

        public int RestaurantID { get; set; }

        public decimal Price { get; set; }
        public int Ouantity { get; set; }

        public decimal SubTotal { get; set; }

       
    }
}
