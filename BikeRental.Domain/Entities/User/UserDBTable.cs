using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeRental.Domain.Enums;

namespace BikeRental.Domain.Entities.User
{
    public class UserDBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required][StringLength(40)] public string Email { get; set; }

        public UserRole Level { get; set; }

        public decimal Balance { get; set; }
    }
}
