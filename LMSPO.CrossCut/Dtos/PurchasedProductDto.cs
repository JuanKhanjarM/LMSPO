using LMSPO.CoreBusiness.Entities;

namespace LMSPO.CrossCut.Dtos
{
    public class PurchasedProductDto
    {
        public int PurchasedProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int PurchasedQty { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalCost { get; set; }
        public int TotalAddedQuantity { get; set; } 
        public int GetIndividualAddedQuantityForGroupProduct { get; set; }
        public int PPInputQty { get; set; }
    }
}
