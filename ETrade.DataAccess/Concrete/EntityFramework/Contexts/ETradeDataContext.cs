using ETrade.Core.Entities.Concrete;
using ETrade.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETrade.DataAccess.Concrete.EntityFramework.Contexts
{
    public class ETradeDataContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-ARHEPGH;Database=ETradeTrakya;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
        public DbSet<Entities.Concrete.Address> Addresses { get; set; }
        public DbSet<Entities.Concrete.Category> Categories { get; set; }
        public DbSet<Entities.Concrete.Order> Orders { get; set; }
        public DbSet<Entities.Concrete.OrderDetail> OrderDetails { get; set; }
        public DbSet<Entities.Concrete.PaymentType> PaymentTypes { get; set; }
        public DbSet<Entities.Concrete.Product> Products { get; set; }
        public DbSet<Entities.Concrete.UserAddress> UserAddresses { get; set; }
        
        public DbSet<Core.Entities.Concrete.User> Users { get; set; }
        public DbSet<Core.Entities.Concrete.OperationClaim> OperationClaims { get; set; }
        public DbSet<Core.Entities.Concrete.RoleOperationClaim> RoleOperationClaims { get; set; }
        public DbSet<Core.Entities.Concrete.Role> Roles { get; set; }
    }
}
