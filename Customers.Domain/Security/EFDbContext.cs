using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Customers.Domain.TableDef;

namespace Customers.Domain.Security
{
    public class EFDbContext : DbContext
    {
        public DbSet<UserInformation> UserInformations { get; set; }

        public DbSet<Demand> Demands { get; set; }
    }
}
