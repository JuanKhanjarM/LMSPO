

using LMSPO.BlazorServerApp.ViewModels;

namespace LMSPO.BlazorServerApp.WebApiConnection.Customers
{
    public interface ICustomersWS
    {
        Task<CustomerDto> GetCustomerAsync(string relativeUrl);
    }
}