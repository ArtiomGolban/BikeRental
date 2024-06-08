using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRental.Domain.Entities.Bikes
{
    public class BikeDBTable
    {
        [Key]
        public int BikeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int PricePerDay { get; set; }
        public string PhotoUrl { get; set; }

    }
}