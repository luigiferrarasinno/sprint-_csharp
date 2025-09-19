using Microsoft.EntityFrameworkCore;
using InvestmentAPI.Data;
using InvestmentAPI.Models;

namespace InvestmentAPI.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly InvestmentDbContext _context;

        public InvestmentRepository(InvestmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Investment>> GetAllAsync()
        {
            return await _context.Investments
                .Include(i => i.User)
                .ToListAsync();
        }

        public async Task<Investment?> GetByIdAsync(int id)
        {
            return await _context.Investments
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Investment>> GetByTypeAsync(string type)
        {
            return await _context.Investments
                .Include(i => i.User)
                .Where(i => i.Type.ToLower() == type.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Investment>> GetByUserIdAsync(int userId)
        {
            return await _context.Investments
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<Investment> AddAsync(Investment investment)
        {
            investment.InvestmentDate = DateTime.UtcNow;
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();
            return investment;
        }

        public async Task<Investment> UpdateAsync(Investment investment)
        {
            _context.Investments.Update(investment);
            await _context.SaveChangesAsync();
            return investment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var investment = await _context.Investments.FindAsync(id);
            if (investment == null)
                return false;

            _context.Investments.Remove(investment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Investments.AnyAsync(i => i.Id == id);
        }

        public async Task<object> GetSummaryAsync()
        {
            var summary = await _context.Investments
                .GroupBy(i => i.Type)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count(),
                    TotalAmount = g.Sum(i => i.Amount),
                    AverageReturn = g.Average(i => i.ExpectedReturn ?? 0)
                })
                .ToListAsync();

            var totalAmount = await _context.Investments.SumAsync(i => i.Amount);
            var totalInvestments = await _context.Investments.CountAsync();

            return new
            {
                TotalInvestments = totalInvestments,
                TotalAmount = totalAmount,
                ByType = summary
            };
        }
    }
}
