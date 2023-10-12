using LMSPO.BlazorServerApp.ViewModels;
using System.Text.Json;

namespace LMSPO.BlazorServerApp.WebApiConnection.Customers
{
    public class CustomersWS : ICustomersWS
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _ApiName = "CustomersCleint";
        private readonly ILogger<CustomersWS> _logger;

        public CustomersWS(IHttpClientFactory httpClientFactory, ILogger<CustomersWS> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<CustomerDto> GetCustomerAsync(string relativeUrl)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(_ApiName);

                // Send an HTTP GET request
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(relativeUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using var responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        CustomerDto? customer = await JsonSerializer.DeserializeAsync<CustomerDto>(responseStream, options);
                        if (customer != null)
                        {
                            return customer;
                        }
                        return new CustomerDto();
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error deserializing JSON response.");
                        return null;
                    }
                }
                else
                {
                    _logger.LogError("Failed to retrieve the Customer with status code: {StatusCode}", httpResponseMessage.StatusCode);

                    return new CustomerDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Customer-s.");
                throw;
            }
        }
    }
}
