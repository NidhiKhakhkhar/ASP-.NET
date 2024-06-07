using Microsoft.AspNetCore.Mvc;
using System.Data;
using Zomato.DAL.Restaurant;
using Zomato.BAL;

namespace Zomato.Controllers
{
    
    public class User1Controller : Controller
    {
        Restaurant_DAL dalRestaurant = new Restaurant_DAL();

        
        public IActionResult UserIndex()
        {
            DataTable dt = dalRestaurant.Api_CityWiseRestaurantCount();
            return View(dt);
        }
       
    }
}
