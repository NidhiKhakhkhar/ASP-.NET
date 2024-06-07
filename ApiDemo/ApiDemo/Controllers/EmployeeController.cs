using ApiDemo.BAL;
using ApiDemo.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        Employee_BALBase balEmployee = new Employee_BALBase();

        [HttpGet]
        public IActionResult GetAll()
        {

            List<Employee> emp = balEmployee.Api_Emp_SelectAll();
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (emp.Count > 0 && emp != null)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", emp);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data not Found");
                response.Add("data", null);
                return NotFound(response);

            }
        }

        [HttpGet("{EmpID}")]
        public IActionResult GetOne(int EmpID)
        {
            Employee emp = balEmployee.Api_Emp_SelectByPK(EmpID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (EmpID != 0)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", emp);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data not Found");
                response.Add("data", null);
                return NotFound(response);

            }

        }

        [HttpPost]
        public IActionResult Insert([FromForm] Employee emp)
        {
            bool IsSuccess = balEmployee.Api_Employee_Insert(emp);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (IsSuccess)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", emp);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data not Found");
                response.Add("data", null);
                return NotFound(response);

            }
        }

        [HttpPut("{EmpID}")]
        public IActionResult Update(int EmpID, [FromForm] Employee emp)
        {
            emp.EmpID = EmpID;
            bool IsSuccess = balEmployee.Api_Employee_Update(EmpID, emp);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (IsSuccess)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", emp);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data not Found");
                response.Add("data", null);
                return NotFound(response);

            }
        }

        [HttpDelete("{EmpID}")]
        public IActionResult Delete(int EmpID)
        {
            bool IsSuccess = balEmployee.Api_Employee_Delete(EmpID);
            Dictionary<string, dynamic> response = new Dictionary<string, dynamic>();
            if (IsSuccess)
            {
                response.Add("status", true);
                response.Add("message", "Data Found");
                response.Add("data", IsSuccess);
                return Ok(response);
            }
            else
            {
                response.Add("status", false);
                response.Add("message", "Data not Found");
                response.Add("data", null);
                return NotFound(response);

            }
        }
       
    }
}
