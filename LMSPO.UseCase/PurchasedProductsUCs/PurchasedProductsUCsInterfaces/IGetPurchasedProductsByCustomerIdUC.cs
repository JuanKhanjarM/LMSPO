using LMSPO.CoreBusiness.Entities;
using LMSPO.CrossCut.Dtos;

namespace LMSPO.UseCase.PurchasedProductsUCs.PurchasedProductsUCsInterfaces
{
    public interface IGetPurchasedProductsByCustomerIdUC
    {
        Task<IEnumerable<PurchasedProductDto>> ExecuteAsync(int customerId);
    }
}