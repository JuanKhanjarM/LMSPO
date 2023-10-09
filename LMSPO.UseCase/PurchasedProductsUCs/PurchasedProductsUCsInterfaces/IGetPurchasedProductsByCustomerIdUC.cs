using LMSPO.CoreBusiness.Entities;


namespace LMSPO.UseCase.PurchasedProductsUCs.PurchasedProductsUCsInterfaces
{
    public interface IGetPurchasedProductsByCustomerIdUC
    {
        Task<IEnumerable<PurchasedProduct>> ExecuteAsync(int customerId);
    }
}