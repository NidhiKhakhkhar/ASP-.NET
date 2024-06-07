using ApiDemo.DAL;
using ApiDemo.Model;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace ApiDemo.BAL
{
    public class Employee_BALBase 
    {
        Employee_DALBase dalEmployee = new Employee_DALBase();
        public List<Employee> Api_Emp_SelectAll()
        {
            try
            {
                
                List<Employee> list = dalEmployee.Api_Emp_SelectAll();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Employee Api_Emp_SelectByPK(int EmpID)
        {
            try
            {
                Employee emp = dalEmployee.Api_Employee_SelectByPK(EmpID);
                return emp;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Api_Employee_Insert(Employee emp)
        {
            try
            {
                if (dalEmployee.Api_Employee_Insert(emp))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Api_Employee_Update(int EmpID,Employee emp)
        {
            try
            {
                if (dalEmployee.Api_Employee_Update(EmpID, emp))
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
                if(dalEmployee.Api_Employee_Delete(EmpID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
