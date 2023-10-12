using System.Text.Json.Serialization;

namespace LMSPO.BlazorServerApp.ViewModels
{
    public class GroupDto
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = null!;
        public string EAN { get; set; }

        public int CustomerId { get; set; }

        [JsonPropertyName("Total/kr.")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("Group's Producs")]
        public List<GroupProductVM> GroupProducts { get; set; } = new List<GroupProductVM>();
    }

}
