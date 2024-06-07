using ApiDemo.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace ApiDemo.DAL
{
    public class Employee_DALBase : DAL_Helper
    {
        public List<Employee> Api_Emp_SelectAll()
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("API_MST_Employee_SelectAll");
                List<Employee> list = new List<Employee>();
                using (IDataReader dr = sqlDatabase.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmpID = Convert.ToInt32(dr["EmpID"].ToString());
                        emp.EmpName = dr["EmpName"].ToString();
                        emp.EmpCode = dr["EmpCode"].ToString();
                        emp.Email = dr["Email"].ToString();
                        emp.Contact = dr["Contact"].ToString();
                        emp.Salary = Convert.ToDecimal(dr["Salary"]);
                        list.Add(emp);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Employee Api_Employee_SelectByPK(int EmpID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("API_MST_Employee_SelectByPK");
                sqlDatabase.AddInParameter(cmd, "@EmpID", SqlDbType.Int, EmpID);
                Employee emp = new Employee();
                using(IDataReader dr = sqlDatabase.ExecuteReader(cmd))
                {
                    dr.Read();
                    emp.EmpID = Convert.ToInt32(dr["EmpID"].ToString());
                    emp.EmpName = dr["EmpName"].ToString();
                    emp.EmpCode = dr["EmpCode"].ToString();
                    emp.Email = dr["Email"].ToString();
                    emp.Contact = dr["Contact"].ToString();
                    emp.Salary = Convert.ToDecimal(dr["Salary"]);
                }
                return emp;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public bool Api_Employee_Insert(Employee emp)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("API_MST_Employee_Insert");
                sqlDatabase.AddInParameter(cmd, "@EmpName", SqlDbType.VarChar, emp.EmpName);
                sqlDatabase.AddInParameter(cmd, "@EmpCode", SqlDbType.VarChar, emp.EmpCode);
                sqlDatabase.AddInParameter(cmd, "@Email", SqlDbType.VarChar, emp.Email);
                sqlDatabase.AddInParameter(cmd, "@Contact", SqlDbType.VarChar, emp.Contact);
                sqlDatabase.AddInParameter(cmd, "@Salary", SqlDbType.VarChar, emp.Salary);
                sqlDatabase.AddInParameter(cmd, "@EventDate", SqlDbType.DateTime, emp.EventDate);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(cmd)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Api_Employee_Update(int EmpID, Employee emp)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("API_MST_Employee_Update");
                sqlDatabase.AddInParameter(cmd, "@EmpID", SqlDbType.Int, EmpID);
                sqlDatabase.AddInParameter(cmd, "@EmpName", SqlDbType.VarChar, emp.EmpName);
                sqlDatabase.AddInParameter(cmd, "@EmpCode", SqlDbType.VarChar, emp.EmpCode);
                sqlDatabase.AddInParameter(cmd, "@Email", SqlDbType.VarChar, emp.Email);
                sqlDatabase.AddInParameter(cmd, "@Contact", SqlDbType.VarChar, emp.Contact);
                sqlDatabase.AddInParameter(cmd, "@Salary", SqlDbType.VarChar, emp.Salary);

                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(cmd)))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Api_Employee_Delete(int EmpID)
        {
            try
            {
                SqlDatabase sqlDatabase = new SqlDatabase(ConnStr);
                DbCommand cmd = sqlDatabase.GetStoredProcCommand("API_MST_Employee_Delete");
                sqlDatabase.AddInParameter(cmd, "@EmpID", SqlDbType.Int, EmpID);
                if (Convert.ToBoolean(sqlDatabase.ExecuteNonQuery(cmd)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
