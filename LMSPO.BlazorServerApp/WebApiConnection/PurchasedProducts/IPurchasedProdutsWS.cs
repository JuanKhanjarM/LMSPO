
using LMSPO.BlazorServerApp.ViewModels;

namespace LMSPO.BlazorServerApp.WebApiConnection.PurchasedProducts
{
    public interface IPurchasedProdutsWS
    {
        Task<IEnumerable<PurchasedProductVM>> GetPurchasedProductAsync(string relativeUrl);
        Task<PurchasedProductVM> CreatePurchasedProductAsync(string relativeUrl, PurchasedProductVM purchasedProduct);
        Task<bool> UpdatePurchasedProductAsync(string relativeUrl, PurchasedProductVM updatedProduct);
        Task<bool> DeletePurchasedProductAsync(string relativeUrl);
    }
}