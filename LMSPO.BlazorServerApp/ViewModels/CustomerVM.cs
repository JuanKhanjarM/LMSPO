using System.Text.Json.Serialization;

namespace LMSPO.BlazorServerApp.ViewModels
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }

        [JsonPropertyName("Customer Name")]
        public string CustomerName { get; set; }

        public decimal TotalSpent { get; set; }

        public int TotalGroups { get; set; }

        public decimal TotalGroupSpent { get; set; }

        public List<PurchasedProductVM> PurchasedProducts { get; set; }=new List<PurchasedProductVM>();

        public List<GroupVM> Groups { get; set; } = new List<GroupVM>();

    }
}
