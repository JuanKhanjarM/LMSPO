using LMSPO.CoreBusiness.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSPO.SqlServer.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            // Configure the entity properties and keys
            builder.HasKey(g => g.GroupId);

            // Define the many-to-one relationship between Group and Customer
            builder.HasOne(g => g.Customer)
                   .WithMany(c => c.Groups)
                   .HasForeignKey(g => g.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Now, configure the one-to-many relationship between Group and GroupProduct
            builder.HasMany(g => g.GroupProducts) // A Group can have many GroupProducts
                   .WithOne(gp => gp.Group)       // Each GroupProduct belongs to one Group
                   .HasForeignKey(gp => gp.GroupId); // Foreign key in GroupProduct pointing to Group


            // Seed data for Groups (you can add more)
            //builder.HasData(
            //    new Group { GroupId = 1, GroupName = "Group 1", EAN = "ABC123", CustomerId = 1 },
            //    new Group { GroupId = 2, GroupName = "Group 2", EAN = "XYZ456", CustomerId = 2 }
            //// Add more group records here
            //);
        }
    }
}
