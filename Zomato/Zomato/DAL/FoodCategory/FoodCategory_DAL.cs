using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;

namespace Zomato.DAL.FoodCategory
{
    public class FoodCategory_DAL : FoodCategory_DALBase
    {
        public DataTable Api_RestaurantFoodCategory(int? RestaurantID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                if (RestaurantID != 0)
                {
                    DbCommand cmd = sqlDB.GetStoredProcCommand("API_RestautantFoodCategories_SelectByRestaurantID");
                    sqlDB.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, RestaurantID);
                    DataTable dt = new DataTable();
                    using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }

                else
                {
                    DbCommand cmd = sqlDB.GetStoredProcCommand("API_FoodCategories_SelectAll");
                    DataTable dt = new DataTable();
                    using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Api_RestaurantFoodCategory_Insert(int RestaurantID, int CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_RestaurantFoodCategories_Insert");
                sqlDB.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, RestaurantID);
                sqlDB.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, CategoryID);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<FoodCategoryDropdownModel> Api_FoodCategoryDropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_FoodCategories_DropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }
                List<FoodCategoryDropdownModel> list = new List<FoodCategoryDropdownModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    FoodCategoryDropdownModel model = new FoodCategoryDropdownModel();
                    model.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    model.CategoryName = dr["CategoryName"].ToString();
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
