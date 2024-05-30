using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using BikeRental.Domain.Entities.User;

namespace BikeRental.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=BikeRental")
        {
        }

        public virtual DbSet<SessionDBTable> Sessions { get; set; }
    }
}
