using InvestmentAPI.Models;

namespace InvestmentAPI.Services
{
    public interface IInvestmentService
    {
        Task<IEnumerable<Investment>> GetAllInvestmentsAsync();
        Task<Investment?> GetInvestmentByIdAsync(int id);
        Task<IEnumerable<Investment>> GetInvestmentsByTypeAsync(string type);
        Task<IEnumerable<Investment>> GetInvestmentsByUserIdAsync(int userId);
        Task<Investment?> CreateInvestmentAsync(Investment investment);
        Task<Investment?> UpdateInvestmentAsync(int id, Investment investment);
        Task<bool> DeleteInvestmentAsync(int id);
        Task<object> GetInvestmentsSummaryAsync();
    }
}
