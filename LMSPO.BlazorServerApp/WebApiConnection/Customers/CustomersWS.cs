using ConsoleTables;
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
                            LogCustomerGroups(customer.Groups);
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


        private void LogCustomerGroups(List<GroupDto> groups)
        {
            if (!groups.Any())
            {
                return;
            }

            // Log Groups
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Groups");

            foreach (var group in groups)
            {
                LogGroup(group);

                // Log Group Products
                if (group.GroupProducts.Any())
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Group Products for Group");

                    foreach (var groupProduct in group.GroupProducts)
                    {
                        LogGroupProduct(groupProduct);
                    }
                }
            }
        }

        private void LogGroup(GroupDto group)
        {
            var table = new ConsoleTable("Group Name", "Group EAN", "Customer Id", "Group Total")
                         .AddRow(group.GroupName, group.EAN, group.CustomerId, group.TotalPrice);

            LogWithColor(table.ToString(), ConsoleColor.DarkGreen);
        }

        private void LogGroupProduct(GroupProductVM groupProduct)
        {
            var table = new ConsoleTable("Product Name", "Cost", "Added Quantity", "Sub Total")
                         .AddRow(groupProduct.ProductName, groupProduct.Cost, groupProduct.AddedQuantity, groupProduct.SubTotal);

            LogWithColor(table.ToMarkDownString(), ConsoleColor.Yellow);
        }

        private void LogWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            _logger.LogInformation(message);
            Console.ResetColor();
        }

    }
}
