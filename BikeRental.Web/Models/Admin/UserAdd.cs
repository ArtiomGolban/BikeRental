﻿using BikeRental.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeRental.Web.Models.Admin
{
    public class UserAdd
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}