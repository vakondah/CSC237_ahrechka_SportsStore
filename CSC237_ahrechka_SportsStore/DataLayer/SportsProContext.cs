using CSC237_ahrechka_SportsStore.DataLayer.Configurations;
using CSC237_ahrechka_SportsStore.DataLayer.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public class SportsProContext : DbContext
    {
        // constructor:
        public SportsProContext(DbContextOptions<SportsProContext> options)
            : base(options)
        { }

        //Products - name of the table
        public DbSet<Product> Products { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Registration> Registrations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SeedProduct());
            modelBuilder.ApplyConfiguration(new SeedTechnician());
            modelBuilder.ApplyConfiguration(new SeedCountry());
            modelBuilder.ApplyConfiguration(new SeedCustomer());
            modelBuilder.ApplyConfiguration(new SeedIncident());
            modelBuilder.ApplyConfiguration(new SeedRegistration());

            // many-to-many relationship for registrations table
            modelBuilder.ApplyConfiguration(new RegistrationConfig());

        }
    }
}
