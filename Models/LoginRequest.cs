namespace CarServiceManager.Models
{
    public class LoginRequest
    {
        public string userEmail { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
