using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeRental.Domain.Entities.User;

namespace BikeRental.Domain.Entities.Bikes
{
    public class ReservationDBTable
    {
        [Key]
        public int ReservationId { get; set; }
        public UserDBTable UserId { get; set; }
        public int BikeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}