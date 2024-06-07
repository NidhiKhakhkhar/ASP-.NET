using Microsoft.AspNetCore.Mvc;
using System.Data;
using Zomato.DAL.User;

namespace Zomato.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class UserController : Controller
    {
        public IActionResult UserList()
        {
            User_DAL dalUser = new User_DAL();
            DataTable dt = dalUser.Api_UserProfile_SelectAll();
            return View(dt);
        }
    }
}
