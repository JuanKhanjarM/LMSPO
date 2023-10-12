using LMSPO.BlazorServerApp.ViewModels;

namespace LMSPO.BlazorServerApp.WebApiConnection.GroupProducts
{
    public interface IGroupProductsWS
    {
        Task<GroupDto?> GetGroupDetailsAsync(string relativeUrl);
    }
}
