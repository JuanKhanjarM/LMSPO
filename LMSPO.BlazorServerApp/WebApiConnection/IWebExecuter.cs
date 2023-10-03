namespace LMSPO.BlazorServerApp.WebApiConnection
{
    public interface IWebExecuter
    {
        Task<T?> InvokeGetTAsync<T>(string relativeUrl);
    }
}