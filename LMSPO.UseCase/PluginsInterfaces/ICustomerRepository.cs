using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.PluginsInterfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerWithGroupsAndProductsAsync(int customerId);
        Task<Customer> GetCustomerById(string CustomerId);
    }
}
