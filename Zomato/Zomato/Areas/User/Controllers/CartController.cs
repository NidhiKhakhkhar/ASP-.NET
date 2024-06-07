using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Drawing.Printing;
using Zomato.Areas.Admin.Models;
using Zomato.Areas.User.Models;
using Zomato.BAL;
using Zomato.DAL.Cart;
using Zomato.DAL.Order;

namespace Zomato.Areas.User.Controllers
{
    [Area("User")]
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        #region Common Dal objects
        Cart_DAL dalCart = new Cart_DAL();
        #endregion

        #region CartList
        public IActionResult CartList(int UserID)
        {
            DataTable dt = dalCart.Api_Cart_SelectByUserID(UserID);
            int Count = dalCart.Api_Cart_Count();

            HttpContext.Session.SetString("CartCount", Count.ToString());
            return View(dt);


        }
        #endregion

        #region CartInsert
        public IActionResult CartInsert(int UserID, int MenuID)
        {
            DataTable dt = dalCart.Api_Cart_SelectByUserIDAndMenuID(UserID, MenuID);

            if (dt.Rows.Count == 0)
            {
                if (dalCart.Api_Cart_Insert(UserID, MenuID))
                {
                    int Count = dalCart.Api_Cart_Count();

                    HttpContext.Session.SetString("CartCount", Count.ToString());
                    return RedirectToAction("MenuPage", "User", new { RestaurantID = CV.RestaurantID(), Name = CV.RestaurantName() });
                }

            }
            return RedirectToAction("MenuPage", "User", new { RestaurantID = CV.RestaurantID(), Name = CV.RestaurantName() });
        }

        #endregion

        #region IncrementQuantity
        public IActionResult IncrementQuantity(int Quantity, int MenuID, string returnUrl)
        {
            if (dalCart.Api_Cart_IncrementQuantity(Quantity, MenuID))
            {
                if (returnUrl == "/Cart/CartList")
                {
                    return RedirectToAction("CartList", "Cart", new { Areas = "User", UserID = CV.UserID() });
                }
                else
                {
                    return RedirectToAction("MenuPage", "User", new { Areas = "User", RestaurantID = CV.RestaurantID(), Name = CV.RestaurantName() });
                }

            }
            return RedirectToAction("MenuPage", "User", new { RestaurantID = CV.RestaurantID(), Name = CV.RestaurantName() });

        }
        #endregion

        #region DecrementQuantity
        public IActionResult DecrementQuantity(int Quantity, int MenuID, string returnUrl)
        {
            if (Quantity == 1)
            {
                if (dalCart.Api_Cart_Delete(MenuID))
                {
                    int Count = dalCart.Api_Cart_Count();

                    HttpContext.Session.SetString("CartCount", Count.ToString());
                    if (returnUrl == "/Cart/CartList")
                    {
                        return RedirectToAction("CartList", "Cart", new { Areas = "User", UserID = CV.UserID() });
                    }
                    else
                    {
                        return RedirectToAction("MenuPage", "User", new { RestaurantID = CV.RestaurantID(), Name = CV.RestaurantName() });
                    }


                }
            }


            else
            {
                if (dalCart.Api_Cart_DecrementQuantity(Quantity, MenuID))
                {
                    if (returnUrl == "/Cart/CartList")
                    {
                        return RedirectToAction("CartList", "Cart", new { Areas = "User", UserID = CV.UserID() });
                    }
                    else
                    {
                        return RedirectToAction("MenuPage", "User", new { RestaurantID = CV.RestaurantID(), Name = CV.RestaurantName() });
                    }
                }

            }

            return RedirectToAction("MenuPage", "User", new { RestaurantID = CV.RestaurantID(), Name = CV.RestaurantName() });
        }

        #endregion

        #region PlaceOrder
        public IActionResult PlaceOrder()
        {
            Order_DAL dalOrder = new Order_DAL();
            DataTable dt = dalCart.Api_Cart_SelectByUserID(CV.UserID());

            List<CartModel> list = new List<CartModel>();
            foreach (DataRow dr in dt.Rows)
            {
                CartModel cart = new CartModel();
                cart.CartID = Convert.ToInt32(dr["CartID"]);
                cart.MenuID = Convert.ToInt32(dr["MenuID"]);
                cart.RestaurantID = Convert.ToInt32(dr["RestaurantID"]);
                cart.UserID = Convert.ToInt32(CV.UserID());
                cart.Ouantity = Convert.ToInt32(dr["Quantity"]);
                cart.Price = Convert.ToDecimal(dr["Price"]);
                cart.SubTotal = Convert.ToDecimal(cart.Price * cart.Ouantity);
                list.Add(cart);
            }

            foreach (CartModel cartModel in list)
            {
                bool isOrder;
                DataTable table = dalOrder.Api_Order_SelectByUserIDAndRestaurantID(cartModel.RestaurantID);
                if (table.Rows.Count == 0)
                {
                     isOrder = dalOrder.Api_Order_Insert(cartModel);
                }
                else
                {
                     isOrder = dalOrder.Api_Order_UpdateTotalAmount(cartModel);
                }

                if(isOrder)
                {
                    int id = dalOrder.Api_Order_GetOrderID(cartModel.RestaurantID);
                    bool orderItem = dalOrder.Api_OrderItem_Insert(cartModel, id);
                    bool isOrderDone = dalCart.Api_Cart_Update_IsOrderDone(cartModel.CartID);
                    if (cartModel == list[list.Count - 1])
                    {
                        bool ordered = dalOrder.Api_Order_UpdateAvailableField(id);

                        if (ordered)
                        {
                            return RedirectToAction("OrderList","Order",new {Areas = User});
                        }
                    }
                }
              
            }
  
             return RedirectToAction("CartList", "Cart", new { Areas = "User", UserID = CV.UserID() });

        }
        #endregion
    }
}
