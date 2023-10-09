using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.GroupUCs.GroupUCInterfaces
{
    public interface ICreateGroupUC
    {
        Task<Group?> ExcecuteAsync(int customerId, Group group);
    }
}