using LMSPO.BlazorServerApp.ViewModels;
using System.Text.Json;

namespace LMSPO.BlazorServerApp.WebApiConnection.GroupProducts
{
    public class GroupsWS : IGroupsWS
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _ApiName = "GroupsCleint";
        private readonly ILogger<GroupsWS> _logger;

        public GroupsWS(IHttpClientFactory httpClientFactory, ILogger<GroupsWS> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<GroupVM?> GetGroupDetailsAsync(string relativeUrl)
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

                        GroupVM? group = await JsonSerializer.DeserializeAsync<GroupVM>(responseStream, options);
                        if (group != null)
                        {
                           // LogCustomerGroups(customer.Groups);
                            return group;
                        }
                        return new GroupVM();
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

                    return new GroupVM();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the Group-s.");
                throw;
            }
        }
    }
}
