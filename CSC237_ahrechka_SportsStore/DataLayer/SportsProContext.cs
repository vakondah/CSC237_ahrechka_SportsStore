using CSC237_ahrechka_SportsStore.DataLayer.Configurations;
using CSC237_ahrechka_SportsStore.DataLayer.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.Models
{
    public class SportsProContext : IdentityDbContext<User>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SeedProduct());
            modelBuilder.ApplyConfiguration(new SeedTechnician());
            modelBuilder.ApplyConfiguration(new SeedCountry());
            modelBuilder.ApplyConfiguration(new SeedCustomer());
            modelBuilder.ApplyConfiguration(new SeedIncident());
            modelBuilder.ApplyConfiguration(new SeedRegistration());

            // many-to-many relationship for registrations table
            modelBuilder.ApplyConfiguration(new RegistrationConfig());

        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager =
                serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string username = "admin";
            string password = "P@ssw0rd";
            string roleName = "Admin";

            // if role does not exists, create it
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // if username does not exists, create it and add it to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                User user = new User { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
