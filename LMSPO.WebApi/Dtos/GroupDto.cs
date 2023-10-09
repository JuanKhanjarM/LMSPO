namespace LMSPO.WebApi.Dtos
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string EAN { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<GroupProductDto> GroupProductDto { get; set; } = new List<GroupProductDto>();
    }

}
