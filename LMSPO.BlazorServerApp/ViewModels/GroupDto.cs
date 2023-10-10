using System.Text.Json.Serialization;

namespace LMSPO.BlazorServerApp.ViewModels
{
    public class GroupDto
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }
        public string EAN { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public List<GroupProductDto> GroupProducts { get; set; } = new List<GroupProductDto>();
    }

}
