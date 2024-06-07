using System.ComponentModel.DataAnnotations;

namespace Zomato.Areas.User.Models
{
    public class UserModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
        public string  PhoneNo { get; set; }



        public string Email { get; set; }

        public DateTime LastLoginDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        
    }

    public class LoginUserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
