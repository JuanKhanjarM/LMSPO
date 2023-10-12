using ConsoleTables;
using LMSPO.BlazorServerApp.ViewModels;
using System.Text;
using System.Text.Json;

namespace LMSPO.BlazorServerApp.WebApiConnection.PurchasedProducts
{
    public class PurchasedProdutsWS : IPurchasedProdutsWS
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _ApiName = "PurchasedProductsCleint";
        private readonly ILogger<PurchasedProdutsWS> _logger;

        public PurchasedProdutsWS(IHttpClientFactory httpClientFactory, ILogger<PurchasedProdutsWS> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<PurchasedProductVM>> GetPurchasedProductAsync(string relativeUrl)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(_ApiName);

                // Send an HTTP GET request
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(relativeUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    using Stream responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    try
                    {
                        JsonSerializerOptions options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        IEnumerable<PurchasedProductVM>? purchasedProduct = await JsonSerializer.DeserializeAsync<IEnumerable<PurchasedProductVM>>(responseStream, options);
                        if (purchasedProduct != null && purchasedProduct.Any())
                        {
                            foreach (var item in purchasedProduct)
                            {
                                var table = new ConsoleTable("Product Name", "Product Qty", "Product Price", "Product Total")
                                             .AddRow(item.ProductName, item.PurchasedQty, item.ProductPrice, item.TotalCost);

                                // Set the console color (for example, green)
                                Console.ForegroundColor = ConsoleColor.DarkGreen;

                                // Log the table with colors
                                _logger.LogInformation(table.ToMarkDownString());

                                // Reset the console color
                                Console.ResetColor();
                            }

                            return purchasedProduct;
                        }
                        return Enumerable.Empty<PurchasedProductVM>();
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error deserializing JSON response.");
                        return null;
                    }
                }
                else
                {
                    _logger.LogError("Failed to retrieve the purchased product with status code: {StatusCode}", httpResponseMessage.StatusCode);

                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the purchased product.");
                throw;
            }
        }

        public async Task<PurchasedProductVM> CreatePurchasedProductAsync(string relativeUrl, PurchasedProductVM purchasedProduct)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(_ApiName);

                var json = JsonSerializer.Serialize(purchasedProduct);// convert from complix object to a jason (string)
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //A Http response including status code and data
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(relativeUrl, content);

                if (httpResponseMessage.IsSuccessStatusCode == true)// True (200 - 299)
                {
                    using var responseStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    try
                    {
                        PurchasedProductVM createdPurchaseProduct = await JsonSerializer.DeserializeAsync<PurchasedProductVM>(responseStream);
                        return createdPurchaseProduct;
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Error deserializing JSON response.");
                        return null;
                    }
                }
                else
                {
                    _logger.LogError("Product creation failed with status code: {StatusCode}", httpResponseMessage.StatusCode);

                    return null;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating a purchased product.");
                throw;
            }

        }

        public async Task<bool> UpdatePurchasedProductAsync(string relativeUrl, PurchasedProductVM updatedProduct)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(_ApiName);

                var json = JsonSerializer.Serialize(updatedProduct);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send an HTTP PUT request
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(relativeUrl, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Successfully updated
                    return true;
                }
                else
                {
                    _logger.LogError("Failed to update the purchased product with status code: {StatusCode}", httpResponseMessage.StatusCode);

                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the purchased product.");
                throw;
            }
        }


        public async Task<bool> DeletePurchasedProductAsync(string relativeUrl)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(_ApiName);

                // Send an HTTP DELETE request
                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(relativeUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    // Successfully deleted
                    return true;
                }
                else
                {
                    _logger.LogError("Failed to delete the purchased product with status code: {StatusCode}", httpResponseMessage.StatusCode);

                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the purchased product.");
                throw;
            }
        }

    }
}
