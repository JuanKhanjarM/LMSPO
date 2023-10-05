namespace LMSPO.CrossCut.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSpent { get; set; }
        public List<PurchasedProductDto> PurchasedProducts { get; set; } = new List<PurchasedProductDto>();
        public List<GroupDto> Groups { get; set; } = new List<GroupDto>();
    }
}
