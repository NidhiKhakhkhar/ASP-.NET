using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using Zomato.Areas.User.Models;
using Zomato.BAL;

namespace Zomato.DAL.User
{
    public class User_DALBase : DAL_Helper
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnStr);

        #region Select by username password
        public DataTable Api_User_SelectByUserNamePassword(string UserName, string Password)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_UserProfile_SelectByUsernamePassword");
                sqlDB.AddInParameter(cmd, "@UserName", SqlDbType.VarChar, UserName);
                sqlDB.AddInParameter(cmd, "@Password", SqlDbType.VarChar, Password);

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

        #endregion

        #region Method: dbo_PR_SEC_User_Register
        public bool dbo_PR_SEC_User_Register(UserModel user)
        {
            try
            {
                
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("API_UserProfile_SelectUserName");
                sqlDB.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, user.UserName);
                DataTable dataTable = new DataTable();
                using (IDataReader dataReader = sqlDB.ExecuteReader(dbCMD))
                {
                    dataTable.Load(dataReader);
                }
                if (dataTable.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    DbCommand dbCMD1 = sqlDB.GetStoredProcCommand("API_UserPrpfile_Insert");
                    sqlDB.AddInParameter(dbCMD1, "@UserName", SqlDbType.VarChar, user.UserName);
                    sqlDB.AddInParameter(dbCMD1, "@Password", SqlDbType.VarChar, user.Password);
                    sqlDB.AddInParameter(dbCMD1, "@FirstName", SqlDbType.VarChar, user.FirstName);
                    sqlDB.AddInParameter(dbCMD1, "@LastName", SqlDbType.VarChar, user.LastName);
                    sqlDB.AddInParameter(dbCMD1, "@Address", SqlDbType.VarChar, user.Address);
                    sqlDB.AddInParameter(dbCMD1, "@Email", SqlDbType.VarChar, user.Email);
                    sqlDB.AddInParameter(dbCMD1, "@PhoneNo", SqlDbType.VarChar, user.PhoneNo);
                    sqlDB.AddInParameter(dbCMD1, "@IsActive", SqlDbType.Bit, true);
                    if (CV.IsAdmin())
                    {
                        sqlDB.AddInParameter(dbCMD1, "@IsAdmin", SqlDbType.Bit, user.IsAdmin);
                    }
                    else
                    {
                        sqlDB.AddInParameter(dbCMD1, "@IsAdmin", SqlDbType.Bit, false);
                    }


                  

                    if (Convert.ToBoolean(sqlDB.ExecuteNonQuery(dbCMD1)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region select by pk
        public DataTable Api_User_SelectByPK(int? UserID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_UserProfile_SelectByPK");
                sqlDB.AddInParameter(cmd, "@UserID", SqlDbType.Int, UserID);
                DataTable dt = new DataTable();
                using(IDataReader dr = sqlDB.ExecuteReader(cmd))
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


        #region update login date
        public bool Api_UserProfile_UpdateLoginDate(int? UserID)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("Api_UserProfile_UpdateLoginDate");
                sqlDB.AddInParameter(cmd, "@UserID", SqlDbType.Int, UserID);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


        #region Api_UserProfile_SelectAll

        public DataTable Api_UserProfile_SelectAll()
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_UserProfile_SelectAll");

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

        public bool Api_UserProfile_Update(UserModel user)
        {
            try
            {
                DbCommand cmd = sqlDB.GetStoredProcCommand("API_UserProfile_UpdateByPK");
                sqlDB.AddInParameter(cmd,"UserID",SqlDbType.Int, user.UserID);
                sqlDB.AddInParameter(cmd, "Email", SqlDbType.VarChar, user.Email);
                sqlDB.AddInParameter(cmd, "FirstName", SqlDbType.VarChar, user.FirstName);
                sqlDB.AddInParameter(cmd, "LastName", SqlDbType.VarChar, user.LastName);
                sqlDB.AddInParameter(cmd, "Address", SqlDbType.VarChar, user.Address);
                sqlDB.AddInParameter(cmd, "PhoneNo", SqlDbType.VarChar, user.PhoneNo);

                int result = sqlDB.ExecuteNonQuery(cmd);
                return result == -1 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
