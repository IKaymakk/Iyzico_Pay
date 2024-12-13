using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class iyzicoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost;database=IyzicoPaymentDb; integrated security=true;TrustServerCertificate=True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
