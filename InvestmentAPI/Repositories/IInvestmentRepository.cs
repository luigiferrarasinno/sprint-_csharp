using InvestmentAPI.Models;

namespace InvestmentAPI.Repositories
{
    public interface IInvestmentRepository
    {
        Task<IEnumerable<Investment>> GetAllAsync();
        Task<Investment?> GetByIdAsync(int id);
        Task<IEnumerable<Investment>> GetByTypeAsync(string type);
        Task<IEnumerable<Investment>> GetByUserIdAsync(int userId);
        Task<Investment> AddAsync(Investment investment);
        Task<Investment> UpdateAsync(Investment investment);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<object> GetSummaryAsync();
    }
}
