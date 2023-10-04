using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.PluginsInterfaces
{
    public interface IPurchasedProductRepository
    {
        Task<IEnumerable<PurchasedProduct>?> GetPurchasedProductsByCustomerIdAsync(int customerId);
    }
}
