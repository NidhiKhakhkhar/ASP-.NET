using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Areas.Admin.Models
{
    public class Menus
    {
        public int MenuID { get; set; }

        
        public string Name { get; set; }


        public string CategoryName { get; set; }

        [Required]
        [DisplayName("Item Name")]
        public string ItemName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }


        public string? ImageUrl { get; set; }
          
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public decimal Rating { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }

    public class IDModel
    {
        public int RestaurantFoodCategoryID { get; set; }
    }
}
