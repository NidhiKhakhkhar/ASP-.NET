using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.User.Models;
using Zomato.BAL;

namespace Zomato.DAL.Order
{
    public class Order_DAL : Order_DALBase
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnStr);


        #region Api_OrderItem_Insert
        public bool Api_OrderItem_Insert(CartModel cart,int OrderID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_OrderItem_Insert");
                sqlDB.AddInParameter(cmd, "OrderID", SqlDbType.Int, OrderID);
                sqlDB.AddInParameter(cmd, "MenuID", SqlDbType.Int, cart.MenuID);
                sqlDB.AddInParameter(cmd, "Quantity", SqlDbType.Int, cart.Ouantity);
                sqlDB.AddInParameter(cmd, "SubTotal", SqlDbType.Decimal, cart.SubTotal);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Api_Order_GetOrderID
        public int Api_Order_GetOrderID(int RestaurantID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_MST_Orders_SelectByUserIDAndRestaurantID");
                sqlDB.AddInParameter(cmd,"UserID",SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(cmd, "RestaurantID", SqlDbType.Int, RestaurantID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }
                int id = 0;

                foreach (DataRow dr in dt.Rows)
                {
                     id = Convert.ToInt32(dr["OrderID"]);
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        #endregion

        #region Api_OrderItem_SelectByOrderID
        public DataTable Api_OrderItem_SelectByOrderID(int OrderID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_OrderItem_SelectByOrderID");
                sqlDB.AddInParameter(cmd, "OrderID", SqlDbType.Int, OrderID);


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

        #region Api_OrderItem_SelectByUserID
        public DataTable Api_OrderItem_SelectByUserID(int? UserID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_OrderItem_SelectByUserID");
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

    }
}
