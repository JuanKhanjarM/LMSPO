namespace LMSPO.BlazorServerApp.WebApiConnection
{
    public class WebExecuter : IWebExecuter
    {
        private const string _clientApiName = "balszorclient";
        private readonly IHttpClientFactory _httpClientFactory;

        public WebExecuter(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T?> InvokeGetTAsync<T>(string relativeUrl)
        {
            var httpClient = _httpClientFactory.CreateClient(_clientApiName);
            return await httpClient.GetFromJsonAsync<T>(relativeUrl);
        }
    }
}
