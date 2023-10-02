using LMSPO.CoreBusiness.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSPO.SqlServer.Configuration
{
    public class GroupProductConfiguration : IEntityTypeConfiguration<GroupProduct>
    {
        public void Configure(EntityTypeBuilder<GroupProduct> builder)
        {
            // Configure the entity properties and keys
            builder.HasKey(gp => gp.GroupProductId);

            // Define the many-to-one relationship between GroupProduct and Group
            builder.HasOne(gp => gp.Group)
                   .WithMany(g => g.GroupProducts)
                   .HasForeignKey(gp => gp.GroupId)
                   .OnDelete(DeleteBehavior.NoAction); // Cascade delete for Groups, but not for PurchasedProducts

            // Define the many-to-one relationship between GroupProduct and PurchasedProduct
            builder.HasOne(gp => gp.PurchasedProduct)
                   .WithMany(pp => pp.GroupProducts)
                   .HasForeignKey(gp => gp.PurchasedProductId)
                   .OnDelete(DeleteBehavior.NoAction); // No cascade delete for PurchasedProducts

            // Seed data for GroupProducts (you can add more)
            //builder.HasData(
            //    new GroupProduct { GroupProductId = 1, GroupId = 1, PurchasedProductId = 1, AddedQuantity = 5 },
            //    new GroupProduct { GroupProductId = 2, GroupId = 2, PurchasedProductId = 2, AddedQuantity = 3 }
            //// Add more group product records here
            //);
        }
    }
}
