namespace LMSPO.WebApi.Dtos
{
    public class GroupProductDto
    {
        public int GroupProductId { get; set; }
        public int GroupId { get; set; }
        public int PurchasedProductId { get; set; }
        public int AddedQuantity { get; set; }
        public int InputProductQuantity { get; set; }
    }
}
