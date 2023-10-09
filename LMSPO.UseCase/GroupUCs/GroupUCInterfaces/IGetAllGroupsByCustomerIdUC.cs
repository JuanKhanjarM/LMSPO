using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.GroupUCs.GroupUCInterfaces
{
    public interface IGetAllGroupsByCustomerIdUC
    {
        Task<IEnumerable<Group>> ExcecuteAsync(int customerId);
    }
}