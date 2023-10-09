using LMSPO.BlazorServerApp.ViewModels;
using System.Text;
using System.Text.Json;

namespace LMSPO.BlazorServerApp.WebApiConnection.PurchasedProducts
{
    public class PurchasedProdutsWS : IPurchasedProdutsWS
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _ApiName = "PurchasedProducts";
        private readonly ILogger<PurchasedProdutsWS> _logger;

        public PurchasedProdutsWS(IHttpClientFactory httpClientFactory, ILogger<PurchasedProdutsWS> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<IEnumerable<PurchasedProductDto>> GetPurchasedProductAsync(string relativeUrl)
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
                        var purchasedProduct = await JsonSerializer.DeserializeAsync<IEnumerable<PurchasedProductDto>>(responseStream);
                        return purchasedProduct;
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

        public async Task<PurchasedProductDto> CreatePurchasedProductAsync(string relativeUrl, PurchasedProductDto purchasedProduct)
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
                        PurchasedProductDto createdPurchaseProduct = await JsonSerializer.DeserializeAsync<PurchasedProductDto>(responseStream);
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

        public async Task<bool> UpdatePurchasedProductAsync(string relativeUrl, PurchasedProductDto updatedProduct)
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
