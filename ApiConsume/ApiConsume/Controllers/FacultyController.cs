using ApiConsume.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiConsume.Controllers
{
    public class FacultyController : Controller
    {
        Uri baseAddress = new Uri("https://630c3edc53a833c534263096.mockapi.io/Faculties");
        private readonly HttpClient _client;

        public FacultyController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            List<Faculty> faculty = new List<Faculty>();
            HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                faculty = JsonConvert.DeserializeObject<List<Faculty>>(data);
            }

            return View(faculty);
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Faculty> faculty = new List<Faculty>();
            HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                faculty = JsonConvert.DeserializeObject<List<Faculty>>(data);
            }

            return View(faculty);
        }

        public async Task<IActionResult> Save(Faculty faculty)
        {
            try
            {
                var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("FacultyName", faculty.FacultyName),
                        new KeyValuePair<string, string>("FacultyDepartment", faculty.FacultyDepartment),
                        new KeyValuePair<string, string>("FacultyImage", faculty.FacultyImage),
                        new KeyValuePair<string, string>("Salary", faculty.Salary.ToString())
                    };

                var content = new FormUrlEncodedContent(formData);


                if (faculty.id == 0)
                {
                    HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Employee Inserted";
                    }
                    return RedirectToAction("Index");

                }
                else
                {
                    HttpResponseMessage response = await _client.PutAsync($"{_client.BaseAddress}/{faculty.id}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Employee Inserted";
                    }
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddEditAsync(int id = 0)
        {
            Faculty faculty = new Faculty();
            HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                faculty = JsonConvert.DeserializeObject<Faculty>(data);

            }
            return View("AddEdit", faculty);


        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_client.BaseAddress}/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Employee Deleted";
            }
            return RedirectToAction("Index");
        }
    }
}
