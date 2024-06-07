using Microsoft.AspNetCore.Mvc;
using System.Data;
using Zomato.Areas.Admin.Models;
using Zomato.DAL.FoodCategory;
using Zomato.DAL.Menu;
using Zomato.DAL.Restaurant;
using Zomato.BAL;

namespace Zomato.Areas.Admin.Controllers
{
    [CheckAccess]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MenuController : Controller
    {
        #region Common Dal objects
        Menu_DAL dalMenu = new Menu_DAL();
        #endregion 

        #region MenuList
        public IActionResult MenuList(int RestaurantID, int CategoryID)
        {
            FoodCategory_DAL dalFoodCategory = new FoodCategory_DAL();
            ViewBag.FoodCategory = dalFoodCategory.Api_FoodCategoryDropDown();

            if (RestaurantID != 0)
            {
               
                HttpContext.Session.SetString("CategoryID",CategoryID.ToString());
               
                DataTable dt = dalMenu.Api_Menu_SelectByRC(RestaurantID, CategoryID);
                return View(dt);
            }
            else if(CategoryID != 0)
            {
                HttpContext.Session.SetString("CategoryID", CategoryID.ToString());
                DataTable dt = dalMenu.Api_Menu_SelectByCategoryID(CategoryID);
                return View(dt);
            }
            else
            {
                HttpContext.Session.SetString("RestaurantID", RestaurantID.ToString());
                HttpContext.Session.SetString("CategoryID", CategoryID.ToString());
                
                DataTable dt = dalMenu.Api_Menus_SelectAll();
                
                return View(dt);
            }
            
        }
        #endregion

        #region MenuFilter
        public IActionResult MenuFilter(string? ItemName, int? CategoryID,int? RestaurantID,bool? IsActive )
        {
            FoodCategory_DAL dalFoodCategory = new FoodCategory_DAL();
            ViewBag.FoodCategory = dalFoodCategory.Api_FoodCategoryDropDown();
            DataTable dt = dalMenu.Api_MenuFilter(ItemName, CategoryID,RestaurantID, IsActive);


            return View("MenuList", dt);
        }
        #endregion

        #region Save
        public IActionResult Save(int RestaurantID,int CategoryID,Menus menu) 
        {
            
            
                IDModel model = dalMenu.Api_GetRestaurantFoodCategoryID(RestaurantID, CategoryID);
                if (menu.MenuID == 0)
                {
                    if (Convert.ToBoolean(dalMenu.Api_Menus_Insert(model.RestaurantFoodCategoryID, menu)))
                        TempData["InsertItem"] = "Item Inserted";
                }
                else
                {
                    if (Convert.ToBoolean(dalMenu.Api_Menus_Update(model.RestaurantFoodCategoryID, menu)))
                        return RedirectToAction("MenuList", new { RestaurantID = RestaurantID, CategoryID = CategoryID });

                }
                return RedirectToAction("MenuList", new { RestaurantID = RestaurantID, CategoryID = CategoryID });
           
        }
        #endregion

        #region AddEdit
        public IActionResult AddEdit(int MenuID = 0)
        {
            DataTable dt = dalMenu.Api_Menus_SelectByPK(MenuID);
            Menus menu = new Menus();

            foreach(DataRow dr in dt.Rows)
            {
                menu.MenuID = Convert.ToInt32(dr["MenuID"]);
                menu.ItemName = dr["ItemName"].ToString();
                menu.Description = dr["Description"].ToString();
                menu.Price = Convert.ToDecimal(dr["Price"]);
                menu.ImageUrl = dr["ImageUrl"].ToString();
                menu.Rating = Convert.ToDecimal(dr["Rating"]);
                menu.IsActive = Convert.ToBoolean(dr["IsActive"]);

            }
            return View(menu);


        }
        #endregion

        #region Delete
        public IActionResult Delete(int MenuID,int RestaurantID,int CategoryID)
        {
            if(MenuID != 0)
            {
                if (dalMenu.Api_Menus_Delete(MenuID))
                {
                    return RedirectToAction("MenuList", new { RestaurantID = RestaurantID, CategoryID = CategoryID });
                }
            }
            return RedirectToAction("MenuList", new { RestaurantID = RestaurantID, CategoryID = CategoryID });
        }

        #endregion
    }
}
