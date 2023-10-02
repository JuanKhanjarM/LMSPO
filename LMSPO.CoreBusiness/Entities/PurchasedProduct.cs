using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSPO.CoreBusiness.Entities
{
    public class PurchasedProduct
    {
        [Key]
        public int PurchasedProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than 0")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Purchased quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Purchased quantity must be non-negative")]
        public int PurchasedQty { get; set; }

        public int CustomerId { get; set; }

        // Navigation properties should be nullable
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<GroupProduct> GroupProducts { get; set; } = new List<GroupProduct>();

        // Method to calculate the total cost for the purchased product
        public decimal CalculateTotalCost()
        {
            return ProductPrice * PurchasedQty;
        }
        public int GetTotalAddedQuantity()
        {
            return GroupProducts?.Sum(gp => gp.AddedQuantity) ?? 0;
        }
        public int GetIndividualAddedQuantityForGroupProduct()
        {
            var groupProduct = GroupProducts.FirstOrDefault(gp => gp.GroupProductId == this.PurchasedProductId);

            return groupProduct?.AddedQuantity ?? 0;
        }
    }
}
