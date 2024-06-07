using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;

namespace Zomato.DAL.Menu
{
    public class Menu_DALBase : DAL_Helper
    {

        #region Selectall
        public DataTable Api_Menus_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Menus_SelectAll");

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

        #region select by pk
        public DataTable Api_Menus_SelectByPK(int MenuID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Menus_SelectByPK");
                sqlDB.AddInParameter(cmd, "@MenuID", SqlDbType.Int, MenuID);

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

        public DataTable Api_Menu_SelectByRestaurantID(int RestaurantID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Menus_SelectByRestaurantID");
                sqlDB.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, RestaurantID);

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

        #region get ID
        public IDModel Api_GetRestaurantFoodCategoryID(int RestaurantID, int CategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_GetRestaurantFoodCategoryID");
                sqlDB.AddInParameter(cmd, "@RestaurantID", SqlDbType.Int, RestaurantID);
                sqlDB.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, CategoryID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }

                IDModel model = new IDModel();

                foreach (DataRow dr in dt.Rows)
                {
                    model.RestaurantFoodCategoryID = Convert.ToInt32(dr["RestaurantFoodCategoryID"]);
                }
                return model;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region insert
        public bool Api_Menus_Insert(int ID, Menus menu)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Menus_Insert");
                sqlDB.AddInParameter(cmd, "@RestaurantFoodCategoryID", SqlDbType.Int, ID);
                sqlDB.AddInParameter(cmd, "@ItemName", SqlDbType.VarChar, menu.ItemName);
                sqlDB.AddInParameter(cmd, "@Description", SqlDbType.VarChar, menu.Description);
                sqlDB.AddInParameter(cmd, "@Price", SqlDbType.Decimal, menu.Price);
                sqlDB.AddInParameter(cmd, "@ImageUrl", SqlDbType.VarChar, menu.ImageUrl);
                sqlDB.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, menu.IsActive);
                sqlDB.AddInParameter(cmd, "@Rating", SqlDbType.Decimal, menu.Rating);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region update
        public bool Api_Menus_Update(int ID, Menus menu)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Menus_UpdateByPK");
                sqlDB.AddInParameter(cmd, "@MenuID", SqlDbType.Int, menu.MenuID);
                sqlDB.AddInParameter(cmd, "@RestaurantFoodCategoryID", SqlDbType.Int, ID);
                sqlDB.AddInParameter(cmd, "@ItemName", SqlDbType.VarChar, menu.ItemName);
                sqlDB.AddInParameter(cmd, "@Description", SqlDbType.VarChar, menu.Description);
                sqlDB.AddInParameter(cmd, "@Price", SqlDbType.Decimal, menu.Price);
                sqlDB.AddInParameter(cmd, "@ImageUrl", SqlDbType.VarChar, menu.ImageUrl);
                sqlDB.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, menu.IsActive);
                sqlDB.AddInParameter(cmd, "@Rating", SqlDbType.Decimal, menu.Rating);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region Delete
        public bool Api_Menus_Delete(int MenuID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Menus_DeleteByPK");
                sqlDB.AddInParameter(cmd, "@MenuID", SqlDbType.Int, MenuID);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
