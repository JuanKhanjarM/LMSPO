

using LMSPO.BlazorServerApp.ViewModels;

namespace LMSPO.BlazorServerApp.WebApiConnection.Customers
{
    public interface ICustomersWS
    {
        Task<CustomerVM> GetCustomerAsync(string relativeUrl);
    }
}