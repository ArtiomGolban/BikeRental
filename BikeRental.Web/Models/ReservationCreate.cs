using System;
using System.Collections.Generic;

namespace BikeRental.Web.Models
{
    public class ReservationCreate
    {
        public int BikeId { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

    }
}