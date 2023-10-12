using System.Text.Json.Serialization;

namespace LMSPO.WebApi.Dtos
{
    public class GroupDto
    {
        public int GroupId { get; set; }

        //[JsonPropertyName("Navnpå afdeling")]
        public string GroupName { get; set; }
        [JsonPropertyName("EAN")]
        public string EAN { get; set; }

        public int CustomerId { get; set; }

        [JsonPropertyName("Total/kr.")]
        public decimal TotalPrice { get; set; }

       // public string FirstGroupProductName { get; set; }
        [JsonPropertyName("Group's Producs")]
        public List<GroupProductDto> GroupProducts { get; set; } = new List<GroupProductDto>();
    }

}
