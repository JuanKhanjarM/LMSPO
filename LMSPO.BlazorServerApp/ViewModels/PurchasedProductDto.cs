
namespace LMSPO.BlazorServerApp.ViewModels
{
    public class PurchasedProductVM
    {
        public int PurchasedProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int PurchasedQty { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalCost { get; set; }
        public int CustomerId { get; set; }
        public int PPInputQty { get; set; }
    }
}
