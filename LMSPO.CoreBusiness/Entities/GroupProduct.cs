using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSPO.CoreBusiness.Entities
{
    public class GroupProduct
    {
        [Key]
        public int GroupProductId { get; set; }

        public int GroupId { get; set; }
        public int PurchasedProductId { get; set; }

        [Required(ErrorMessage = "Added quantity must be at least 1")]
        public int AddedQuantity { get; set; }

        // Navigation properties should be nullable
        [ForeignKey(nameof(GroupId))]
        public virtual Group? Group { get; set; }

        [ForeignKey(nameof(PurchasedProductId))]
        public virtual PurchasedProduct? PurchasedProduct { get; set; }

        // Method to calculate the subtotal for the group product
        public decimal CalculateSubtotal()
        {
            if (PurchasedProduct != null)
            {
                return AddedQuantity * PurchasedProduct.ProductPrice;
            }
            return 0;
        }
    }
}
