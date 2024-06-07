using System.ComponentModel.DataAnnotations;

namespace Zomato.Areas.Admin.Models
{
    public class FoodCategoryModel
    {

        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string? RestaurantID { get; set; }

        public IFormFile File { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }

    public class FoodCategoryDropdownModel
    {
        public int CategoryID { get; set;}

        public string? CategoryName { get; set;}
    }
}
