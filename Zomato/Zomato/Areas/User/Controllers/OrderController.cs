using Microsoft.AspNetCore.Mvc;
using System.Data;
using Zomato.Areas.User.Models;
using Zomato.BAL;
using Zomato.DAL.Cart;
using Zomato.DAL.Order;

namespace Zomato.Areas.User.Controllers
{
    [Area("User")]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        Order_DAL dalOrder = new Order_DAL();
        Cart_DAL dalCart = new Cart_DAL();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderList()
        {
            int Count = dalCart.Api_Cart_Count();

            HttpContext.Session.SetString("CartCount", Count.ToString());

            DataTable orderList = dalOrder.Api_Order_SelectByUserID(CV.UserID());

            DataTable orderItemList = dalOrder.Api_OrderItem_SelectByUserID(CV.UserID());

            var viewModel = new OrderListModel();

            viewModel.OrderList = orderList;
            viewModel.OrderItemList = orderItemList;

            List<int> orderIDs = new List<int>();

            foreach (DataRow row in orderItemList.Rows)
            {

                orderIDs.Add((int)row["OrderID"]);

            }
            ViewBag.orderIDs = orderIDs;


            return View(viewModel);
        }

        public IActionResult OrderItemList(int OrderID)
        {
            DataTable dt = dalOrder.Api_OrderItem_SelectByOrderID(OrderID);
            return View(dt);
        }


        public IActionResult UpdateStatus(int OrderID, int? UserID, string? UserName)
        {
            bool updated = dalOrder.Api_Order_UpdateOrderStatus(OrderID);
            return RedirectToAction("OrderList", new { UserID = UserID, UserName = UserName });
        }
    }
}
