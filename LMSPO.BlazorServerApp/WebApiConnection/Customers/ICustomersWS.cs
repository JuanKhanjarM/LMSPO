using LMSPO.CrossCut.Dtos;

namespace LMSPO.BlazorServerApp.WebApiConnection.Customers
{
    public interface ICustomersWS
    {
        Task<CustomerDto> GetCustomerAsync(string relativeUrl);
    }
}