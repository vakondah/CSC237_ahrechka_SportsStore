using CSC237_ahrechka_SportsStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC237_ahrechka_SportsStore.DataLayer.Configurations
{
    public class RegistrationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            /*
             * many-to-many relationship for Registration table
             */
            // Composite primary key
            
                builder.HasKey(r => new { r.CustomerID, r.ProductID });

            // one-to-many relationship beyween Customer and Registration:
                builder.HasOne(r => r.Customer)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.CustomerID);

            // one-to-many relationship beyween Product and Registration:
                builder.HasOne(r => r.Product)
                .WithMany(c => c.Registrations)
                .HasForeignKey(r => r.ProductID);
        }
    }
}
