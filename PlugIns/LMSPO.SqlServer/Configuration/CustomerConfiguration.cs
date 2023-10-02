using LMSPO.CoreBusiness.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSPO.SqlServer.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Configure the entity properties and keys
            builder.HasKey(c => c.CustomerId);

            // Define the one-to-many relationship between Customer and PurchasedProduct
            builder.HasMany(c => c.PurchasedProducts)
                   .WithOne(pp => pp.Customer)
                   .HasForeignKey(pp => pp.CustomerId);

            // Define the one-to-many relationship between Customer and Group
            builder.HasMany(c => c.Groups)
                   .WithOne(g => g.Customer)
                   .HasForeignKey(g => g.CustomerId);

            // Seed data (you can add more customers as needed)
            builder.HasData(
                new Customer { CustomerId = 1, CustomerName = "Vejle Kommune" },
                new Customer { CustomerId = 2, CustomerName = "Herning Kommune" }
            // Add more customer records here
            );
        }
    }
}
