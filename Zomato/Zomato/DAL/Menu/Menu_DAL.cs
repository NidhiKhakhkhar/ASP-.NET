using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;

namespace Zomato.DAL.Menu
{
    public class Menu_DAL : Menu_DALBase
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnStr);

        #region Select by RestaurantID FoodCategoryID
        public DataTable Api_Menu_SelectByRC(int RestaurantID, int CategoryID)
        {
            try
            {
                
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Menus_SelectByRC");
                sqlDB.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, RestaurantID);
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
        #endregion

        #region select by CategoryID

        public DataTable Api_Menu_SelectByCategoryID(int CategoryID)
        {
            try
            {
                
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Menus_SelectByCategoryID");
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
        #endregion


        public DataTable Api_MenuFilter(string? ItemName,int? CategoryID,int? RestaurantID,bool? IsActive)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Menu_Filter");
                sqlDB.AddInParameter(cmd, "@ItemName", SqlDbType.VarChar, ItemName);
                sqlDB.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, CategoryID);
                sqlDB.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, RestaurantID);
                sqlDB.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);
                


                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
