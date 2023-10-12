using System.Text.Json.Serialization;

namespace LMSPO.BlazorServerApp.ViewModels
{
    public class GroupProductVM
    {
        //public int GroupProductId { get; set; }
        public int GroupId { get; set; }
        public int PurchasedProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int AddedQuantity { get; set; }
        public int InputProductQuantity { get; set; }

        [JsonPropertyName("Sub total/kr.")]
        public decimal SubTotal { get; set; }
    }
}
