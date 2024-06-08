using System.ComponentModel.DataAnnotations;
using BikeRental.Domain.Enums;

namespace BikeRental.Web.Models.Admin
{
    public class UserEdit
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public UserRole Level { get; set; }

        public decimal Balance { get; set; }
    }
}   