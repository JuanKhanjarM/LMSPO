using System.Text.Json.Serialization;

namespace LMSPO.WebApi.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        [JsonPropertyName("Customer Name")]
        public string CustomerName { get; set; }

        [JsonPropertyName("Total Purchased")]
        public decimal TotalSpent { get; set; }

        [JsonPropertyName("Number Of Groups")]
        public int TotalGroups { get; set; }

        [JsonPropertyName("Total/kr.")]
        public decimal TotalGroupSpent { get; set; }

        [JsonPropertyName("Purchased Products")]
        public List<PurchasedProductDto> PurchasedProducts { get; set; } 

        public List<GroupDto> Groups { get; set; }
    }
}
