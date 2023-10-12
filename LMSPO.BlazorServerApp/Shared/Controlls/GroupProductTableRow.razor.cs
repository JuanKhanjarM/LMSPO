using LMSPO.BlazorServerApp.ViewModels;
using Microsoft.AspNetCore.Components;

namespace LMSPO.BlazorServerApp.Shared.Controlls
{
    public partial class GroupProductTableRow
    {
        [Parameter]
        public GroupProductVM GroupProduct { get; set; } = new GroupProductVM();

        [Parameter]
        public int PurchasedProductAvailability { get; set; }

        [Parameter]
        public EventCallback<GroupProductVM> RemoveProduct { get; set; }

        private bool IsInvalidQuantity()
        {
            return (PurchasedProductAvailability - GroupProduct.InputProductQuantity) == 0 || (GroupProduct.AddedQuantity * -1 == GroupProduct.InputProductQuantity);
        }

        private async Task RemoveProductClicked()
        {
            await RemoveProduct.InvokeAsync(GroupProduct);
        }
        [Parameter]
        public EventCallback PurchasedProductAvailabilityQuantityChanged { get; set; }
    }
}
