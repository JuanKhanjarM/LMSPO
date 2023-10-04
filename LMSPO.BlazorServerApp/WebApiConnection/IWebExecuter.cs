namespace LMSPO.BlazorServerApp.WebApiConnection
{
    public interface IWebExecuter
    {
        Task<bool> InvokeDeleteAsync(string relativeUrl);
        Task<T?> InvokeGetTAsync<T>(string relativeUrl);
        Task<TResponse?> InvokePostAsync<TRequest, TResponse>(string uri, TRequest data);
        Task<TResponse?> InvokePutAsync<TRequest, TResponse>(string relativeUrl, TRequest request);
    }
}