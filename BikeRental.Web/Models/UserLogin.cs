using System.ComponentModel.DataAnnotations;

namespace BikeRental.Web.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}