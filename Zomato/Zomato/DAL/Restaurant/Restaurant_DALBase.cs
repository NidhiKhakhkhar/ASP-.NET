using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;

namespace Zomato.DAL.Restaurant
{
    public class Restaurant_DALBase : DAL_Helper
    {
        #region Select all

        public DataTable dbo_Api_Restaurant_SelectAll()
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDb.GetStoredProcCommand("API_Restaurants_SelectAll");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDb.ExecuteReader(cmd))
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

        #region Select by pk

        public DataTable dbo_Api_Restaurants_SelectByPK(int RestaurantID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDb.GetStoredProcCommand("API_Restaurants_SelectByPK");
                sqlDb.AddInParameter(cmd, "RestaurantID", SqlDbType.Int, RestaurantID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDb.ExecuteReader(cmd))
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

        #region Insert Restaurant

        public bool dbo_Api_Restaurant_Insert(Restaurants restaurants)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDb.GetStoredProcCommand("API_Restaurants_Insert");
                sqlDb.AddInParameter(cmd, "Name", SqlDbType.VarChar, restaurants.Name);
                sqlDb.AddInParameter(cmd, "Description", SqlDbType.VarChar, restaurants.Description);
                sqlDb.AddInParameter(cmd, "Address", SqlDbType.VarChar, restaurants.Address);
                sqlDb.AddInParameter(cmd, "PhoneNo", SqlDbType.VarChar, restaurants.PhoneNo);
                sqlDb.AddInParameter(cmd, "Email", SqlDbType.VarChar, restaurants.Email);
                sqlDb.AddInParameter(cmd, "OpeningTime", SqlDbType.DateTime, restaurants.OpeningTime);
                sqlDb.AddInParameter(cmd, "ClosingTime", SqlDbType.DateTime, restaurants.ClosedTime);
                sqlDb.AddInParameter(cmd, "AverageRatig", SqlDbType.Decimal, restaurants.AverageRatig);
                sqlDb.AddInParameter(cmd, "NumReviews", SqlDbType.Int, restaurants.NumReviews);
                sqlDb.AddInParameter(cmd, "IsActive", SqlDbType.Bit, restaurants.IsActive);
                sqlDb.AddInParameter(cmd, "ImageUrl", SqlDbType.VarChar, restaurants.ImageUrl);
                sqlDb.AddInParameter(cmd, "CityID", SqlDbType.Int, restaurants.CityID);

                int result = sqlDb.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Update
        public bool dbo_Api_Restaurants_Update(Restaurants restaurants)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDb.GetStoredProcCommand("API_Restaurants_UpdateByPK");
                sqlDb.AddInParameter(cmd, "RestaurantID", SqlDbType.Int, restaurants.RestaurantID);
                sqlDb.AddInParameter(cmd, "Name", SqlDbType.VarChar, restaurants.Name);
                sqlDb.AddInParameter(cmd, "Description", SqlDbType.VarChar, restaurants.Description);
                sqlDb.AddInParameter(cmd, "Address", SqlDbType.VarChar, restaurants.Address);
                sqlDb.AddInParameter(cmd, "PhoneNo", SqlDbType.VarChar, restaurants.PhoneNo);
                sqlDb.AddInParameter(cmd, "Email", SqlDbType.VarChar, restaurants.Email);
                sqlDb.AddInParameter(cmd, "OpeningTime", SqlDbType.DateTime, restaurants.OpeningTime);
                sqlDb.AddInParameter(cmd, "ClosingTime", SqlDbType.DateTime, restaurants.ClosedTime);
                sqlDb.AddInParameter(cmd, "AverageRatig", SqlDbType.Decimal, restaurants.AverageRatig);
                sqlDb.AddInParameter(cmd, "NumReviews", SqlDbType.Int, restaurants.NumReviews);
                sqlDb.AddInParameter(cmd, "IsActive", SqlDbType.Bit, restaurants.IsActive);
                sqlDb.AddInParameter(cmd, "ImageUrl", SqlDbType.VarChar, restaurants.ImageUrl);
                sqlDb.AddInParameter(cmd, "CityID", SqlDbType.Int, restaurants.CityID);

                int result = sqlDb.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region Delete

        public bool? dbo_Api_Restaurants_DeleteByPK(int? RestaurantID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDb.GetStoredProcCommand("API_Restaurants_DeleteByPK");
                sqlDb.AddInParameter(cmd, "RestaurantID", SqlDbType.Int, RestaurantID);

                int result = sqlDb.ExecuteNonQuery(cmd);
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
