using System.Text.Json.Serialization;

namespace LMSPO.WebApi.Dtos
{
    public class GroupDto
    {
        public int GroupId { get; set; }

        [JsonPropertyName("Navn på afdeling")]
        public string GroupName { get; set; }
        [JsonPropertyName("EAN")]
        public string EAN { get; set; }

        public int CustomerId { get; set; }

        [JsonPropertyName("SubTotal/kr.")]
        public decimal TotalPrice { get; set; }

        [JsonPropertyName("Group's Producs")]
        public List<GroupProductDto> GroupProductDto { get; set; } = new List<GroupProductDto>();
    }

}
