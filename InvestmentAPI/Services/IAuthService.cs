using InvestmentAPI.Models;

namespace InvestmentAPI.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<object> ValidateTokenAsync(string token);
        Task<object> GetTestUsersAsync();
    }
}
