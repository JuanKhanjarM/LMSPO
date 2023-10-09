using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.CustomerUC.CustomerUCInterfaces
{
    public interface IGetCustomerWithGroupsAndProductsUC
    {
        Task<Customer?> ExecuteAsync(int customerId);
    }
}