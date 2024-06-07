using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.Admin.Models;

namespace Zomato.DAL.Restaurant
{
    public class Restaurant_DAL : Restaurant_DALBase
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnStr);
        #region Restaurant filter
        public DataTable Api_Rrestaurants_Filter(string? Name, string? Address, bool? IsActive)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_Restaurants_Filter");
                sqlDB.AddInParameter(cmd, "@Name", SqlDbType.VarChar, Name);
                sqlDB.AddInParameter(cmd, "@Address", SqlDbType.VarChar, Address);
                sqlDB.AddInParameter(cmd, "@IsActive", SqlDbType.Bit, IsActive);

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

        #region City Cropdown
        public List<CityModel> CityDropdown(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_CityDropdown");
                sqlDB.AddInParameter(cmd, "StateID", SqlDbType.Int, StateID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }

                List<CityModel> list = new List<CityModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CityModel city = new CityModel();
                    city.CityID = Convert.ToInt32(dr["CityID"]);
                    city.CityName = dr["CityName"].ToString();
                    list.Add(city);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


        #region State Dropdown
        public List<StateModel> StateDropdown(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnStr);
                if (CountryID == 0)
                {

                }

                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_State_StateByCountryID");
                sqlDB.AddInParameter(cmd, "CountryID", SqlDbType.Int, CountryID);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }

                List<StateModel> list = new List<StateModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    StateModel state = new StateModel();
                    state.StateID = Convert.ToInt32(dr["StateID"]);
                    state.StateName = dr["StateName"].ToString();
                    list.Add(state);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Country Dropdown

        public List<CountryModel> CountryDropdown()
        {
            try
            {

                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_CountryDropdown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(cmd))
                {
                    dt.Load(dr);
                }

                List<CountryModel> list = new List<CountryModel>();
                foreach (DataRow dr in dt.Rows)
                {
                    CountryModel country = new CountryModel();
                    country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    country.CountryName = dr["CountryName"].ToString();
                    list.Add(country);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region City wise restaurant count
        public DataTable Api_CityWiseRestaurantCount()
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_CityWiseRestaurantCount");

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

        public DataTable Api_Restaurant_SelectByCityID(int CityID)
        {

            try
            {
                SqlDatabase sqlDb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDb.GetStoredProcCommand("Api_Restaurant_SelectByCityID");
                sqlDb.AddInParameter(cmd, "@CityID", SqlDbType.Int, CityID);

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

        public DataTable Api_Restaurant_SelectByFoodCategory(int CategoryID,int CityID)
        {
            try
            {
                SqlDatabase sqlDb = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDb.GetStoredProcCommand("Api_Restaurant_SelectByFoodCategory");
                sqlDb.AddInParameter(cmd, "@CategoryID", SqlDbType.Int, CategoryID);
                sqlDb.AddInParameter(cmd, "@CityID", SqlDbType.Int, CityID);

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
    }
}
