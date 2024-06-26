using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportationOrders.Models.Entities
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public ApplicationDBContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=8090;Database=orders_db;Username=admin;Password=qwerty");
        }
    }
}
