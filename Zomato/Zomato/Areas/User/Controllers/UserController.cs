using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Data;
using System.Text.Json;
using Zomato.Areas.Admin.Models;
using Zomato.Areas.User.Models;
using Zomato.BAL;
using Zomato.DAL.Cart;
using Zomato.DAL.FoodCategory;
using Zomato.DAL.Menu;
using Zomato.DAL.Restaurant;
using Zomato.DAL.User;

namespace Zomato.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class UserController : Controller
    {
        #region Common Dal objects
        Restaurant_DAL dalRestaurant = new Restaurant_DAL();
        FoodCategory_DAL dalFoodCategory = new FoodCategory_DAL();
        Menu_DAL dalMenu = new Menu_DAL();
        User_DAL dalUser = new User_DAL();
        Cart_DAL dalCart = new Cart_DAL();
        #endregion

        #region UserLogin 
        public IActionResult SEC_UserLogin()
        {
            return View();
        }

        #endregion

        #region UserRegister
        public IActionResult SEC_UserRegister()
        {
            return View();
        }
        #endregion

        #region Login
        [HttpPost]
        public IActionResult Login(LoginUserModel user)
        {
            if (ModelState.IsValid)
            {
                
                DataTable dt = dalUser.Api_User_SelectByUserNamePassword(user.UserName, user.Password);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Console.WriteLine(dr);
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        HttpContext.Session.SetString("Address", dr["Address"].ToString());
                        break;
                    }

                    bool isSuccess = dalUser.Api_UserProfile_UpdateLoginDate(CV.UserID());
                    if (isSuccess)
                    {
                        Console.WriteLine("Success");
                    }

                    if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("IsAdmin") == "True")
                    {
                        return RedirectToAction("Index", "Home" , new {Areas = ""});
                    }
                    else if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("IsAdmin") == "False")
                    {
                        return RedirectToAction("UserIndex", "User1" ,new {Areas = ""});
                    }
                }
               
            }

            // If ModelState is not valid, return the view with validation messages
            return View("SEC_UserLogin", user);
        }

        #endregion

        #region Logout

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SEC_UserLogin");
        }

        #endregion

        #region Register

        public IActionResult Register(UserModel user)
        {
            
            bool IsSuccess = dalUser.dbo_PR_SEC_User_Register(user);
            if (IsSuccess)
            {
                return RedirectToAction("SEC_UserLogin");
            }
            else
            {
                return RedirectToAction("SEC_UserRegister");
            }
        }

        #endregion

        #region UserProfile
        public IActionResult UserProfile(int UserID)
        {

            DataTable dt = dalUser.Api_User_SelectByPK(UserID);
            UserModel user = new UserModel();

           

            foreach (DataRow dr in dt.Rows)
            {
                user.UserID = Convert.ToInt32(dr["UserID"]);
                user.UserName = dr["UserName"].ToString();
                user.Password = dr["Password"].ToString();
                user.FirstName = dr["FirstName"].ToString();
                user.LastName = dr["LastName"].ToString();
                user.PhoneNo = dr["PhoneNo"].ToString();
                user.Address = dr["Address"].ToString();
                user.Email = dr["Email"].ToString();
                user.LastLoginDate = Convert.ToDateTime(dr["LastLoginDate"]);
                user.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }
            return View(user);
        }
        #endregion

        #region Index

        public IActionResult Index(int CityID,string CityName)
        {
            DataTable restaurantList = dalRestaurant.Api_Restaurant_SelectByCityID(CityID);
            DataTable foodCategoryList = dalFoodCategory.dbo_Api_FoodCategory_SelectAll();

            int Count = dalCart.Api_Cart_Count();

            HttpContext.Session.SetString("CartCount", Count.ToString());

            HttpContext.Session.SetString("CityID", CityID.ToString());
            HttpContext.Session.SetString("CityName", CityName);

            var viewModel = new MyViewModel
            {
                RestaurantList = restaurantList,
                FoodCategoryList = foodCategoryList,
            };

            return View(viewModel);
        }

        #endregion

        #region restaurant by food category
        public IActionResult SelectByFoodCategory(int CategoryID, int CityID)
        {
            DataTable restaurantList = dalRestaurant.Api_Restaurant_SelectByFoodCategory(CategoryID,CityID);
            DataTable foodCategoryList = dalFoodCategory.dbo_Api_FoodCategory_SelectAll();

            var viewModel = new MyViewModel();

            viewModel.RestaurantList = restaurantList;
            viewModel.FoodCategoryList = foodCategoryList;

            return View("Index",viewModel);
        }

        #endregion

        #region MenuPage

        public IActionResult MenuPage(int RestaurantID,string Name)
        {
            if(CV.UserID() == 0)
            {
                return View("SEC_UserLogin");
            }

            HttpContext.Session.SetString("RestaurantName", Name);
            HttpContext.Session.SetString("RestaurantID",RestaurantID.ToString());

            DataTable menuList = dalMenu.Api_Menu_SelectByRestaurantID(RestaurantID);

            DataTable cartList = dalCart.Api_Cart_SelectByUserID(CV.UserID());

            DataTable restaurants = dalRestaurant.dbo_Api_Restaurants_SelectByPK(RestaurantID);

            var viewModel = new MenuPageModel();

            viewModel.MenuList = menuList;
            viewModel.CartList = cartList;
            viewModel.Restaurants = restaurants;

            List<int> menuIds = new List<int>();

            foreach (DataRow row in cartList.Rows)
            {

                menuIds.Add((int)row["MenuID"]);

            }
            ViewBag.menuIds = menuIds;


            return View(viewModel);
        }
        #endregion

        #region UserUpdate
        public IActionResult UserUpdate(UserModel user)
        {
            if (Convert.ToBoolean(dalUser.Api_UserProfile_Update(user)))
            {
               return RedirectToAction("UserProfile",new {UserID = CV.UserID()});
            }

           return RedirectToAction("UserProfile", new {UserID = CV.UserID()});
        }
        #endregion

        #region ProfilePage
        public IActionResult ProfilePage()
        {
            DataTable dt = dalUser.Api_User_SelectByPK(CV.UserID());
            UserModel user = new UserModel();

            Console.WriteLine(CV.UserID());

            foreach (DataRow dr in dt.Rows)
            {
                user.UserID = Convert.ToInt32(dr["UserID"]);
                user.UserName = dr["UserName"].ToString();
                user.Password = dr["Password"].ToString();
                user.FirstName = dr["FirstName"].ToString();
                user.LastName = dr["LastName"].ToString();
                user.PhoneNo = dr["PhoneNo"].ToString();
                user.Address = dr["Address"].ToString();
                user.Email = dr["Email"].ToString();
                user.LastLoginDate = Convert.ToDateTime(dr["LastLoginDate"]);
                user.IsActive = Convert.ToBoolean(dr["IsActive"]);
            }
            return View(user);
        }
        #endregion

        #region EditProfilePage
        public IActionResult EditProfilePage(UserModel user)
        {
            if (Convert.ToBoolean(dalUser.Api_UserProfile_Update(user)))
            {
                return RedirectToAction("ProfilePage",user);
            }

            return RedirectToAction("ProfilePage",user);
        }

        #endregion
    }
}
