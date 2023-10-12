namespace LMSPO.BlazorServerApp.ViewModels
{
    public class GroupProductDto
    {
        public int GroupProductId { get; set; }
        public int GroupId { get; set; }
        public int PurchasedProductId { get; set; }
        public int AddedQuantity { get; set; }
        public int InputProductQuantity { get; set; }
        public GroupDto? Group { get; set; } // Nullable Group navigation property
        public PurchasedProductVM? PurchasedProduct { get; set; } // Nullable PurchasedProduct navigation property
    }
}
