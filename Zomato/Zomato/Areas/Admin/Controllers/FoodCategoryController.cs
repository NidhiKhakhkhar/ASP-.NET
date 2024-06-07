using Microsoft.AspNetCore.Mvc;
using System.Data;
using Zomato.Areas.Admin.Models;
using Zomato.DAL.FoodCategory;
using Zomato.BAL;

namespace Zomato.Areas.Admin.Controllers
{
    [CheckAccess]
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class FoodCategoryController : Controller
    {
        #region Common Dal objects
        FoodCategory_DAL dalFoodCategory = new FoodCategory_DAL();
        #endregion

        #region FoodCategoryList
        public IActionResult FoodCategoryList(int RestaurantID = 0)
        {
            HttpContext.Session.SetString("RestaurantID", RestaurantID.ToString());

            DataTable dt = dalFoodCategory.Api_RestaurantFoodCategory(RestaurantID);
            return View(dt);
        }

        #endregion

        #region Save
        public IActionResult Save(FoodCategoryModel food)
        {
            if(food.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, food.File.FileName);

                food.ImageUrl = "~" + FilePath.Replace("wwwroot\\","/") + "/" + food.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    food.File.CopyTo(stream);
                }
            }

            if (food.CategoryID == 0)
            {
                if (Convert.ToBoolean(dalFoodCategory.Api_FoodCategory_Insert(food)))
                    TempData["FoodInsertedMessage"] = "Record Inserted";
            }
            else
            {
                if (Convert.ToBoolean(dalFoodCategory.Api_FoodCategory_Update(food)))
                    return RedirectToAction("FoodCategoryList");
            }

            return RedirectToAction("FoodCategoryList");


        }
        #endregion

        #region SaveRestaurantWiseFoodCategory
        public IActionResult SaveRestaurantWiseFoodCategory(int CategoryID, int RestaurantID)
        {
            if (CategoryID != 0)
            {
                if (Convert.ToBoolean(dalFoodCategory.Api_RestaurantFoodCategory_Insert(RestaurantID, CategoryID)))
                    return RedirectToAction("FoodCategoryList", new { RestaurantID = RestaurantID });
            }
            return RedirectToAction("FoodCategoryList", new { RestaurantID = RestaurantID });
        }
        #endregion

        #region AddEdit
        public IActionResult AddEdit(int CategoryID = 0)
        {
            ViewBag.FoodCategory = dalFoodCategory.Api_FoodCategoryDropDown();
            DataTable dt = dalFoodCategory.Api_FoodCategory_SelectByPK(CategoryID);
            FoodCategoryModel food = new FoodCategoryModel();

            foreach (DataRow dr in dt.Rows)
            {
                food.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                food.CategoryName = dr["CategoryName"].ToString();
                food.ImageUrl = dr["ImageUrl"].ToString();

            }
            return View(food);

        }
        #endregion

        #region Delete
        public IActionResult Delete(int RestaurantID, int CategoryID)
        {
            if (Convert.ToBoolean(dalFoodCategory.Api_FoodCategory_Delete(RestaurantID, CategoryID)))
            {
                return RedirectToAction("FoodCategoryList", new { RestaurantID = RestaurantID });
            }
            return RedirectToAction("FoodCategoryList", new { RestaurantID = RestaurantID });
        }

        #endregion

    }


}
