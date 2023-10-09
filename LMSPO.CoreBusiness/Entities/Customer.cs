using System.ComponentModel.DataAnnotations;

namespace LMSPO.CoreBusiness.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        // Navigation properties should use collection interfaces
        public virtual ICollection<PurchasedProduct> PurchasedProducts { get; set; } = new List<PurchasedProduct>();
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

        // Constructor to initialize collections
        public Customer()
        {
            PurchasedProducts = new List<PurchasedProduct>();
            Groups = new List<Group>();
        }

        // Method to calculate the total amount spent by the customer
        public decimal CalculateTotalSpent()
        {
            return PurchasedProducts.Sum(pp => pp.CalculateTotalCost());
        }
        public decimal CalculateTotalCostForGroup()
        {
            return Groups.Sum(pp => pp.CalculateTotalPrice());
        }

        // Method to add a purchased product to the customer's list
        public void AddPurchasedProduct(PurchasedProduct purchasedProduct)
        {
            PurchasedProducts.Add(purchasedProduct);
            purchasedProduct.Customer = this;
        }

        
        // Method to join an existing group
        public void JoinGroup(Group group)
        {
            Groups.Add(group);
        }

        // Method to get the total number of groups for a customer
        public int GetTotalGroups()
        {
            return Groups.Count;
        }
    }

}
