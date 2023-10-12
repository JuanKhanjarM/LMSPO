using System.Text.Json.Serialization;

namespace LMSPO.BlazorServerApp.ViewModels
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public decimal TotalSpent { get; set; }

        public int TotalGroups { get; set; }

        public decimal TotalGroupSpent { get; set; }

        public List<PurchasedProductVM> PurchasedProducts { get; set; }=new List<PurchasedProductVM>();

        public List<GroupDto> Groups { get; set; } = new List<GroupDto>();

    }
}
