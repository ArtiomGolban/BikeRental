using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using BikeRental.Domain.Entities.Bikes;
using BikeRental.Domain.Entities.User;

namespace BikeRental.BusinessLogic.DataBase
{
    public class BikeContext : DbContext
    {
        public BikeContext() : base("name = BikeRentalProject")
        {

        }

        public virtual DbSet<UserDBTable> Users { get; set; }

        public virtual DbSet<BikeDBTable> Bikes { get; set; }
        
        public virtual DbSet<ReservationDBTable> Reservations { get; set; }
    }
}
