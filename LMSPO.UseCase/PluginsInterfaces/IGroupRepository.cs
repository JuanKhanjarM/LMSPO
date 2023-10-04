using LMSPO.CoreBusiness.Entities;

namespace LMSPO.UseCase.PluginsInterfaces
{
    public interface IGroupRepository
    {
        Task<Group?> CreateGroupAsync(int customerId, string groupName);
    }
}
