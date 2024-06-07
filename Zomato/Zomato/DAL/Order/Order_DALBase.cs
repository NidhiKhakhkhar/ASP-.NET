using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;
using Zomato.Areas.User.Models;
using Zomato.BAL;

namespace Zomato.DAL.Order
{
    public class Order_DALBase : DAL_Helper
    {

        SqlDatabase sqlDB = new SqlDatabase(ConnStr);

        #region Api_Order_Insert
        public bool Api_Order_Insert(CartModel cart)
        {

            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_MST_Orders_Insert");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd, "RestaurantID", SqlDbType.Int, cart.RestaurantID);
                sqlDB.AddInParameter(cmd, "Status", SqlDbType.VarChar, "Pending");
                sqlDB.AddInParameter(cmd, "TotalAmount", SqlDbType.Decimal, cart.SubTotal);
                sqlDB.AddInParameter(cmd, "DeliveryAddress", SqlDbType.VarChar, CV.Address());
                sqlDB.AddInParameter(cmd, "PaymentMethod", SqlDbType.VarChar, "Google Pay");

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        #endregion

        #region Api_Order_UpdateTotalAmount
        public bool Api_Order_UpdateTotalAmount(CartModel cart)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_MST_Orders_UpdateTotal");
                sqlDB.AddInParameter(cmd, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd, "RestaurantID", SqlDbType.Int, cart.RestaurantID);
                sqlDB.AddInParameter(cmd, "Total", SqlDbType.Decimal, cart.SubTotal);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Api_Order_SelectByUserIDAndRestaurantID
        public DataTable Api_Order_SelectByUserIDAndRestaurantID(int RestaurantID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_MST_Orders_SelectByUserIDAndRestaurantID");
                sqlDB.AddInParameter(cmd,"UserID",SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd,"RestaurantID",SqlDbType.Int,RestaurantID );

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

        #region Api_Order_UpdateAvailableField
        public bool Api_Order_UpdateAvailableField(int OrderID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Order_UpdateAvailableField");
                sqlDB.AddInParameter(cmd, "OrderID", SqlDbType.Int, OrderID);
                
                

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Api_Order_SelectByUserID
        public DataTable Api_Order_SelectByUserID(int? UserID)
        {

            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_MST_Orders_SelectByUserID");
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

        #region Api_Order_UpdateOrderStatus
        public bool Api_Order_UpdateOrderStatus(int OrderID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Order_UpdateOrderStatus");
                sqlDB.AddInParameter(cmd, "OrderID", SqlDbType.Int, OrderID);

                if (CV.IsAdmin())
                {
                    sqlDB.AddInParameter(cmd, "Status", SqlDbType.VarChar, "Completed");
                }
                else
                {
                    sqlDB.AddInParameter(cmd, "Status", SqlDbType.VarChar, "Cancelled");
                }

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
