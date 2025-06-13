using Blazored.LocalStorage;

namespace AdiExamSimulator.Client.Services
{
    public class AuthorizedHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthorizedHttpClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<HttpClient> GetClientAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return _httpClient;
        }
    }
}
