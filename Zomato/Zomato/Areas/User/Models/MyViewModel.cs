using System.Data;
using Zomato.Areas.Admin.Models;

namespace Zomato.Areas.User.Models
{
    public class MyViewModel
    {
        public DataTable RestaurantList { get; set; }
        public DataTable FoodCategoryList { get; set; }
    }

    public class MenuPageModel
    {
        public DataTable MenuList { get; set; }

        public DataTable CartList { get; set; }

        public DataTable Restaurants { get; set; }
    }

    public class OrderListModel
    {
        public DataTable OrderList { get; set;}

        public DataTable OrderItemList { get; set; }
    }
}
