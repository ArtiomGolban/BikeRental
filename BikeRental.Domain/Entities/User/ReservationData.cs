using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeRental.Domain.Entities.Bikes;

namespace BikeRental.Domain.Entities.User
{
    public class ReservationData
    {
        [Key]
        public int ReservationId { get; set; }
        public BikeDBTable BikeID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int TotalPrice { get; set; }


    }
}