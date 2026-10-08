using CarServiceManager.Models;

namespace CarServiceManager.Services
{
    public class AuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterAsync(RegisterRequest request)
        {
            return await _httpClient.PostAsJsonAsync(
                "/api/Registration",
                request
            );
        }
    }
}
