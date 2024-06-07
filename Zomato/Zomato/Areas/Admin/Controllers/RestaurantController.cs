using Microsoft.AspNetCore.Mvc;
using System.Data;
using Zomato.Areas.Admin.Models;
using Zomato.DAL.Restaurant;
using Zomato.BAL;

namespace Zomato.Areas.Admin.Controllers
{
    [CheckAccess]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class RestaurantController : Controller
    {
        #region Common Dal objects
        Restaurant_DAL dalRestaurant = new Restaurant_DAL();
        #endregion

        #region Select all
        public IActionResult RestaurantList()
        {
            
            DataTable dt = dalRestaurant.dbo_Api_Restaurant_SelectAll();
            return View(dt);
        }
        #endregion

        #region Filter
        public IActionResult RestaurantFilter(string? Name,string? Address,bool? IsActive)
        {
            DataTable dt = dalRestaurant.Api_Rrestaurants_Filter(Name, Address,IsActive);
            return View("RestaurantList",dt);
        }

        #endregion

        #region Save
        public IActionResult Save(Restaurants restaurants)
        {
            
                if (restaurants.RestaurantID == 0)
                {
                    if (Convert.ToBoolean(dalRestaurant.dbo_Api_Restaurant_Insert(restaurants)))
                        TempData["RestaurantInsertMessage"] = "Record inserted";
                }
                else
                {
                    if (Convert.ToBoolean(dalRestaurant.dbo_Api_Restaurants_Update(restaurants)))
                        return RedirectToAction("RestaurantList");
                }
                return RedirectToAction("RestaurantList");
           

        }
        #endregion

        #region AddEdit
        public IActionResult AddEdit(int RestaurantID = 0)
        {
            
           
            DataTable dt = dalRestaurant.dbo_Api_Restaurants_SelectByPK(RestaurantID);

            Restaurants restaurants = new Restaurants();

            foreach (DataRow dr in dt.Rows)
            {
                restaurants.RestaurantID = Convert.ToInt32(dr["RestaurantID"]);
                restaurants.Name = dr["Name"].ToString();
                restaurants.Description = dr["Description"].ToString();
                restaurants.Address = dr["Address"].ToString();
                restaurants.PhoneNo = dr["PhoneNo"].ToString();
                restaurants.Email = dr["Email"].ToString();
                restaurants.OpeningTime = Convert.ToDateTime(dr["OpeningTime"]);
                restaurants.ClosedTime = Convert.ToDateTime(dr["ClosingTime"]);
                restaurants.IsActive = Convert.ToBoolean(dr["IsActive"]);
                restaurants.ImageUrl = dr["ImageUrl"].ToString();
                restaurants.AverageRatig = Convert.ToDecimal(dr["AverageRatig"]);
                restaurants.NumReviews = Convert.ToInt32(dr["NumReviews"]);
                restaurants.CityID = Convert.ToInt32(dr["CityID"]);
                restaurants.StateID = Convert.ToInt32(dr["StateID"]);
                restaurants.CountryID = Convert.ToInt32(dr["CountryID"]);
            }
            if (RestaurantID == 0)
            {
                ViewBag.CountryList = dalRestaurant.CountryDropdown();
                List<StateModel> list1 = new List<StateModel>();
                ViewBag.StateList = list1;
                List<CityModel> list2 = new List<CityModel>();
                ViewBag.CityList = list2;
                restaurants.RestaurantID = RestaurantID;
                return View(restaurants);

            }
            ViewBag.CountryList = dalRestaurant.CountryDropdown();
            ViewBag.StateList = dalRestaurant.StateDropdown(restaurants.CountryID);
            ViewBag.CityList = dalRestaurant.CityDropdown(restaurants.StateID);
            return View(restaurants);

        }
        #endregion

        #region StatesforDropdown
        public dynamic StatesforDropdown(int CountryID)
        {
            List<StateModel> list = dalRestaurant.StateDropdown(CountryID);
            ViewBag.StateList = list;

            return Json(list);
        }
        #endregion

        #region CityForDropdown
        public dynamic CityForDropdown(int StateID)
        {
            List<CityModel> list = dalRestaurant.CityDropdown(StateID);
            ViewBag.CityList = list;
            return Json(list);
        }
        #endregion

        #region Delete

        public IActionResult Delete(int? RestaurantID)
        {
            if (RestaurantID != 0)
            {
                if (Convert.ToBoolean(dalRestaurant.dbo_Api_Restaurants_DeleteByPK(RestaurantID)))
                {
                    return RedirectToAction("RestaurantList");
                }
            }
            return RedirectToAction("RestaurantList");
        }

        #endregion

        #region MultipleDelete
        public ActionResult MultipleDelete()
        {
            string selectedRestaurantIDsString = Request.Form["selectedRestaurantIDs"];

            char[] delimiters = { ',' };

            string[] selectedRestaurantIDs = selectedRestaurantIDsString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            List<int> validRestaurantIDs = new List<int>();
            foreach (string id in selectedRestaurantIDs)
            {
                if (int.TryParse(id, out int restaurantID))
                {
                    validRestaurantIDs.Add(restaurantID);
                }
                else
                {
                    // Handle the invalid string (optional)
                    Console.WriteLine($"Invalid restaurant ID: {id}");
                }
            }

            int[] selectedRestaurantIDsAsIntegers = validRestaurantIDs.ToArray();

            foreach(int id in selectedRestaurantIDsAsIntegers)
            {
                Console.WriteLine(id);
                if (Convert.ToBoolean(dalRestaurant.dbo_Api_Restaurants_DeleteByPK(id)))
                {
                    //return RedirectToAction("RestaurantList");
                    TempData["MultiDelete"] = "Record deleted successfully";
                }
            }
            return RedirectToAction("RestaurantList");


            //if (selectedRestaurantIDs != null && selectedRestaurantIDs.Length > 0)
            //{

            //    Console.WriteLine("Selected Restaurant IDs :" + string.Join(",", selectedRestaurantIDs));

            //    return RedirectToAction("RestaurantList");
            //}



        }
        #endregion


        public IActionResult Index()
        {
            return View();
        }
    }
}
