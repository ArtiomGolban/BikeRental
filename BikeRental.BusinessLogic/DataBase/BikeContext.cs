using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using BikeRental.Domain.Entities.User;

namespace BikeRental.BusinessLogic.DataBase
{
    public class BikeContext : DbContext
    {
        public BikeContext() : base("name = BikeRental")
        {

        }
        public virtual DbSet<UserDBTable> Users { get; set; }
    }
}
