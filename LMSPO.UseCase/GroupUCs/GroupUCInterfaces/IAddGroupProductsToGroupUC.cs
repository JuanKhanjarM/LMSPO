using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.GroupUCs.GroupUCInterfaces
{
    public interface IAddGroupProductsToGroupUC
    {
        Task<bool> ExecuteAsync(int groupId, List<GroupProduct> groupProducts);
    }
}