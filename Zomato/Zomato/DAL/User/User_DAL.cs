using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Zomato.DAL.User
{
    public class User_DAL : User_DALBase
    {
        SqlDatabase sqlDB = new SqlDatabase(ConnStr);
       
    }
}
