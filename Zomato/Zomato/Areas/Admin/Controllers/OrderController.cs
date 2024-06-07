using Microsoft.AspNetCore.Mvc;
using System.Data;
using Zomato.BAL;
using Zomato.DAL.Cart;
using Zomato.DAL.Order;

namespace Zomato.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        Order_DAL dalOrder = new Order_DAL();
        Cart_DAL dalCart = new Cart_DAL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserOrderList(int? UserID, string? UserName)
        {

            HttpContext.Session.SetString("UserOrder", UserName);
            DataTable orderList = dalOrder.Api_Order_SelectByUserID(UserID);
            return View("OrderList", orderList);

        }

        public IActionResult UserOrderItemList(int OrderID)
        {
            DataTable dt = dalOrder.Api_OrderItem_SelectByOrderID(OrderID);
            return View("OrderItemList", dt);
        }


        public IActionResult UpdateStatus(int OrderID, int? UserID, string? UserName)
        {
            bool updated = dalOrder.Api_Order_UpdateOrderStatus(OrderID);
            return RedirectToAction("OrderList", new { UserID = UserID, UserName = UserName });
        }
    }
}
