
namespace LMSPO.BlazorServerApp.ViewModels
{
    public class PurchasedProductDto
    {
        public int PurchasedProductId { get; set; }
        public string ProductName { get; set; }
        public int PurchasedQty { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal TotalCost { get; set; }
        public int CustomerId { get; set; }
        public int PPInputQty { get; set; }
        public CustomerDto? Customer { get; set; }
    }
}
