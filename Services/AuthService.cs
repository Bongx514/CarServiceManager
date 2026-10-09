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

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "/api/Auth",
                request
            );

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return loginResponse ?? new LoginResponse { Message = "Login failed", Token = string.Empty };
            }
            else
            {
                return new LoginResponse { Message = "Login failed", Token = string.Empty };
            }
        }
    }
}
