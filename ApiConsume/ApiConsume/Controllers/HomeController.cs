using ApiConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.AccessControl;

namespace ApiConsume.Controllers
{
    
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:2800/api");
        private readonly HttpClient _client;
        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]

        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Employee/GetAll").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(data);
                var dataOfObject = jsonObject.data;
                var extractedDataJson = JsonConvert.SerializeObject(dataOfObject,Formatting.Indented);
                employees = JsonConvert.DeserializeObject<List<Employee>>(extractedDataJson);
            }
            return View(employees);
        }

        #region Save

        [HttpPost]

        public async Task<IActionResult> Save(Employee emp)
        {
            try
            {
                MultipartFormDataContent formData = new MultipartFormDataContent();
                formData.Add(new StringContent(emp.EmpName), "EmpName");
                formData.Add(new StringContent(emp.EmpCode), "EmpCode");
                formData.Add(new StringContent(emp.Email), "Email");
                formData.Add(new StringContent(emp.Contact), "Contact");
                formData.Add(new StringContent(emp.Salary.ToString()), "Salary");

                if(emp.EmpID == 0)
                {
                    HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/Employee/Insert", formData);
                    if(response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Employee Inserted";
                        
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpResponseMessage response = await _client.PutAsync($"{_client.BaseAddress}/Employee/Update/{emp.EmpID}", formData);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Employee Updated";
                       
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        [HttpGet]
        #region AddEdit
        public IActionResult AddEdit(int EmpID = 0)
        {
            Employee employees = new Employee();
            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}/Employee/GetOne/{EmpID}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(data);
                var dataOfObject = jsonObject.data;
                var extractedDataJson = JsonConvert.SerializeObject(dataOfObject, Formatting.Indented);
                employees = JsonConvert.DeserializeObject<Employee>(extractedDataJson);
            }
            return View("AddEdit",employees);

           
        }

        #endregion


        #region Delete

        [HttpGet]
        public IActionResult Delete(int EmpID)
        {
            HttpResponseMessage response = _client.DeleteAsync($"{_client.BaseAddress}/Employee/Delete/{EmpID}").Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Employee Deleted";
            }
            return RedirectToAction("Index");
        }
        #endregion
        public IActionResult Privacy()
        {
            return View();
        }

    }
}