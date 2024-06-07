using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Areas.Admin.Models
{
    public class Restaurants
    {
        public int RestaurantID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DisplayName("Phone no.")]
        public string PhoneNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime OpeningTime { get; set; }

        [Required]
        public DateTime ClosedTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

        
        public decimal? AverageRatig { get; set; }

        public int? NumReviews { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        [DisplayName("City")]
        public int CityID { get; set; }

        [DisplayName("State")]
        public int StateID { get; set; }

        [DisplayName("Country")]
        public int CountryID { get; set; }
    }
}
