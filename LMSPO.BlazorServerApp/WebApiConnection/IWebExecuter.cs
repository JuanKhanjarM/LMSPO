namespace LMSPO.BlazorServerApp.WebApiConnection
{
    public interface IWebExecuter
    {
        Task<bool> InvokeDeleteAsync(string relativeUrl);
        Task<T?> InvokeGetTAsync<T>(string relativeUrl);
        Task<T?> InvokePostAsync<T>(string uri, T data);
        Task<TResponse?> InvokePutAsync<TRequest, TResponse>(string relativeUrl, TRequest request);
    }
}