using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;
using Zomato.DAL.Menu;

namespace Zomato.DAL.FoodCategory
{
    public class FoodCategory_DALBase : DAL_Helper
    {
        public DataTable dbo_Api_FoodCategory_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_FoodCategories_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable Api_FoodCategory_SelectByPK(int CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_FoodCategories_SelectByPK");
                sqlDB.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, CategoryID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Api_FoodCategory_Insert(FoodCategoryModel food)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_FoodCategories_Insert");
                sqlDB.AddInParameter(cmd, "@CategoryName", SqlDbType.VarChar, food.CategoryName);
                sqlDB.AddInParameter(cmd, "@ImageUrl", SqlDbType.VarChar, food.ImageUrl);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Api_FoodCategory_Update(FoodCategoryModel food)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_FoodCategories_UpdateByPK");
                sqlDB.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, food.CategoryID);
                sqlDB.AddInParameter(cmd, "@CategoryName", SqlDbType.VarChar, food.CategoryName);
                sqlDB.AddInParameter(cmd, "@ImageUrl", SqlDbType.VarChar, food.ImageUrl);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Api_FoodCategory_Delete(int RestaurantID, int CategoryID)
        {
            try
            {
                Menu_DAL dalMenu = new Menu_DAL();
                IDModel id = dalMenu.Api_GetRestaurantFoodCategoryID(RestaurantID, CategoryID);

                if (id.RestaurantFoodCategoryID != 0)
                {

                    SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                    DbCommand cmd = sqlDB.GetStoredProcCommand("API_RestaurantFoodCategories_DeleteByPK");
                    sqlDB.AddInParameter(cmd, "@RestaurantFoodCategoryID", SqlDbType.Int, id.RestaurantFoodCategoryID);

                    int result = sqlDB.ExecuteNonQuery(cmd);
                    return result == -1 ? false : true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
