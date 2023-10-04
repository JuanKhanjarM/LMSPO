using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

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
        //public async Task<T?> InvokeGetTAsync<T>(string relativeUrl)
        //{
        //    HttpClient httpClient = _httpClientFactory.CreateClient(_clientApiName);
        //    return await httpClient.GetFromJsonAsync<T>(relativeUrl);
        //}
        public async Task<T?> InvokeGetTAsync<T>(string relativeUrl)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(_clientApiName);
                var response = await httpClient.GetAsync(relativeUrl);

                if (!response.IsSuccessStatusCode)
                {
                    // Handle non-success status codes
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Handle 404 Not Found
                        return default; // You can return a default value or null for this case
                    }
                    else
                    {
                        // Handle other error status codes
                        throw new HttpRequestException($"Error: {response.StatusCode}");
                    }
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request-related exceptions
                // Log or rethrow as needed
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log or rethrow as needed
                throw;
            }
        }

        public async Task<TResponse?> InvokePostAsync<TRequest, TResponse>(string uri, TRequest data)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient(_clientApiName);
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseContent);
        }

        public async Task<TResponse?> InvokePutAsync<TRequest, TResponse>(string relativeUrl, TRequest request)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(_clientApiName);

                // Serialize the request object to JSON
                var jsonRequest = JsonSerializer.Serialize(request);

                // Create a StringContent with JSON content type
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Send the PUT request
                var response = await httpClient.PutAsync(relativeUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    // Handle non-success status codes
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Handle 404 Not Found
                        return default; // You can return a default value or null for this case
                    }
                    else
                    {
                        // Handle other error status codes
                        throw new HttpRequestException($"Error: {response.StatusCode}");
                    }
                }

                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request-related exceptions
                // Log or rethrow as needed
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log or rethrow as needed
                throw;
            }
        }

        public async Task<bool> InvokeDeleteAsync(string relativeUrl)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient(_clientApiName);

                // Send the DELETE request
                var response = await httpClient.DeleteAsync(relativeUrl);

                if (!response.IsSuccessStatusCode)
                {
                    // Handle non-success status codes
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        // Handle 404 Not Found
                        return false; // You can return false or throw an exception for this case
                    }
                    else
                    {
                        // Handle other error status codes
                        throw new HttpRequestException($"Error: {response.StatusCode}");
                    }
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request-related exceptions
                // Log or rethrow as needed
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log or rethrow as needed
                throw;
            }
        }

    }
}
