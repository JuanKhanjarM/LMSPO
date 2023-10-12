using LMSPO.BlazorServerApp.ViewModels;
using System.Text.Json;

namespace LMSPO.BlazorServerApp.WebApiConnection.GroupProducts
{
    public class GroupProductsWS : IGroupProductsWS
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _ApiName = "GroupsCleint";
        private readonly ILogger<GroupProductsWS> _logger;

        public GroupProductsWS(IHttpClientFactory httpClientFactory, ILogger<GroupProductsWS> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<GroupDto?> GetGroupDetailsAsync(string relativeUrl)
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

                        GroupDto? customer = await JsonSerializer.DeserializeAsync<GroupDto>(responseStream, options);
                        if (customer != null)
                        {
                           // LogCustomerGroups(customer.Groups);
                            return customer;
                        }
                        return new GroupDto();
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

                    return new GroupDto();
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
