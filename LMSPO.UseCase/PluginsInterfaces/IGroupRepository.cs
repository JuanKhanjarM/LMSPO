using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.PluginsInterfaces
{
    public interface IGroupRepository
    {
        Task<Group?> CreateGroupAsync(int  customerId, Group group);
        Task<IEnumerable<Group>> GetAllGroupsByCustomerIdAsync(int customerId);
        Task<Group?> GetGroupByIdAndCustomerIdAsync(int customerId,int groupId);
        Task<bool> AddGroupProductsToGroupAsync(int groupId, List<GroupProduct> groupProducts);
        Task<bool> DeleteGroupByIdAndCustomerIdAsync(int customerId,int groupId);
        Task<bool> DeleteSelectedGroupProductsAsync(int groupId, List<int> selectedGroupProductIds);
    }
}
