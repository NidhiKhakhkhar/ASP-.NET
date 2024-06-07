using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;
using Zomato.BAL;

namespace Zomato.DAL.Cart
{
    public class Cart_DALBase : DAL_Helper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnStr);

        #region Api_Cart_SelectByUserID
        public DataTable Api_Cart_SelectByUserID(int? UserID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Cart_SelectByUserID");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, UserID);

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

        #region Api_Cart_Insert

        public bool Api_Cart_Insert(int UserID, int MenuID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Cart_Insert");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(cmd, "MenuID", SqlDbType.Int, MenuID);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Api_Cart_SelectByUserIDAndMenuID
        public DataTable Api_Cart_SelectByUserIDAndMenuID(int UserID, int MenuID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Cart_SelectByUserIDAndMenuID");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, UserID);
                sqlDB.AddInParameter(cmd, "MenuID", SqlDbType.Int, MenuID);

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


        #region Api_Cart_Count
        public int Api_Cart_Count()
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Cart_UserCartCount");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    int cartCount = Convert.ToInt32(dt.Rows[0]["CartCount"]);
                    return cartCount;
                }
                else
                {
                    return 0;
                }
            }
            catch { return 0; }
        }

        #endregion

        #region Api_Cart_IncrementQuantity
        public bool Api_Cart_IncrementQuantity(int Quantity, int MenuID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Cart_IncrementQuantity");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd, "MenuID", SqlDbType.Int, MenuID);
                sqlDB.AddInParameter(cmd, "Quantity", SqlDbType.Int, Quantity);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex) { return false; }

        }
        #endregion

        #region Api_Cart_DecrementQuantity
        public bool Api_Cart_DecrementQuantity(int Quantity, int MenuID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Cart_DecrementQuantity");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd, "MenuID", SqlDbType.Int, MenuID);
                sqlDB.AddInParameter(cmd, "Quantity", SqlDbType.Int, Quantity);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex) { return false; }

        }
        #endregion

        #region Api_Cart_Delete
        public bool Api_Cart_Delete(int MenuID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_Cart_Delete");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd, "MenuID", SqlDbType.Int, MenuID);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch(Exception ex) { return false; }
        }
        #endregion

        public DataTable Api_Cart_SelectByUserIDAndRestaurantID(int RestaurantID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Cart_SelectByUserIDAndRestaurantID");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd, "RestaurantID", SqlDbType.Int, RestaurantID);

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


        public bool Api_Cart_Update_IsOrderDone(int CartID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Cart_Update_IsOrderDone");
                sqlDB.AddInParameter(cmd, "CartID", SqlDbType.Int, CartID);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
