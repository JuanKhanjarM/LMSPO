using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.GroupUCs.GroupUCInterfaces
{
    public interface IGetGroupByIdAndCustomerIdUC
    {
        Task<Group?> ExecuteAsync(int customerId,int groupId);
    }
}