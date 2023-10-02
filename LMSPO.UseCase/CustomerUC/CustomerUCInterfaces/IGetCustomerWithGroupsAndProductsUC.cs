using LMSPO.CrossCut.Dtos;

namespace LMSPO.UseCase.CustomerUC.CustomerUCInterfaces
{
    public interface IGetCustomerWithGroupsAndProductsUC
    {
        Task<CustomerDto?> ExecuteAsync(int customerId);
    }
}