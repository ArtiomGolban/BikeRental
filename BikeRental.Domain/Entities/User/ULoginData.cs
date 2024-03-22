using System.ComponentModel.DataAnnotations;

namespace BikeRental.Domain.Entities.User
{
    public class ULoginData
{
    public bool Status { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
}
