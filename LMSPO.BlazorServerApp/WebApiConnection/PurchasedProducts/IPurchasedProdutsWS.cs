using LMSPO.CrossCut.Dtos;

namespace LMSPO.BlazorServerApp.WebApiConnection.PurchasedProducts
{
    public interface IPurchasedProdutsWS
    {
        Task<IEnumerable<PurchasedProductDto>> GetPurchasedProductAsync(string relativeUrl);
        Task<PurchasedProductDto> CreatePurchasedProductAsync(string relativeUrl, PurchasedProductDto purchasedProduct);
        Task<bool> UpdatePurchasedProductAsync(string relativeUrl, PurchasedProductDto updatedProduct);
        Task<bool> DeletePurchasedProductAsync(string relativeUrl);
    }
}