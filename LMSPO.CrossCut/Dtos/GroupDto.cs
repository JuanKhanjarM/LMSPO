namespace LMSPO.CrossCut.Dtos
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string EAN { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public CustomerDto? Customer { get; set; } // Nullable Customer navigation property
        public ICollection<GroupProductDto> GroupProducts { get; set; } = new List<GroupProductDto>(); // Nullable GroupProducts navigation property
    }

}
