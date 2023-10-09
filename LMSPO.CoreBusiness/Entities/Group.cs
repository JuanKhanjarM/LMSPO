using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSPO.CoreBusiness.Entities
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Group name is required")]
        public string GroupName { get; set; }

        public string EAN { get; private set; } 

        public int CustomerId { get; set; }

        // Navigation property should be nullable
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<GroupProduct> GroupProducts { get; set; } = new List<GroupProduct>();
        public Group()
        {
                
        }

        // Constructor to initialize the EAN when creating a new group
        private Group(string groupName ,int customerId )
        {
            GroupName = groupName;
            CustomerId = customerId;
            GenerateEAN(); // Generate EAN upon creation
        }
        // Factory method to create a new group with auto-generated EAN
        public static Group CreateNewGroup(string groupName, int customerId)
        {
            return new Group(groupName, customerId);
        }
        // Method to calculate the total price for all group products within the group
        public decimal CalculateTotalPrice()
        {
            return GroupProducts.Sum(gp => gp.CalculateSubtotal());
        }

        // Private method to generate the EAN
        private void GenerateEAN()
        {
            // Define the characters for capital letters (A-Z)
            string capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Create a random number generator
            Random random = new Random();

            // Generate four random digits
            string randomDigits = new string(Enumerable.Range(0, 4).Select(_ => random.Next(10).ToString()[0]).ToArray());

            // Randomly select two capital letters
            string randomLetters = new string(Enumerable.Range(0, 2).Select(_ => capitalLetters[random.Next(capitalLetters.Length)]).ToArray());

            // Combine the letters and digits to create the EAN
            EAN = $"{randomLetters}{randomDigits}{randomLetters}";
        }
    }
}
