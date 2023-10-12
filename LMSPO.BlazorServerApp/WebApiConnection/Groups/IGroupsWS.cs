using LMSPO.BlazorServerApp.ViewModels;

namespace LMSPO.BlazorServerApp.WebApiConnection.GroupProducts
{
    public interface IGroupsWS
    {
        Task<GroupVM?> GetGroupDetailsAsync(string relativeUrl);
    }
}
