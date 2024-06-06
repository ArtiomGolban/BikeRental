using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeRental.Web.Models.Admin
{
    public class BikeEdit
    {   
        public string Name { get; set; }
        public string Type { get; set; }
        public int PricePerDay { get; set; }
        public string PhotoUrl { get; set; }

    }
}