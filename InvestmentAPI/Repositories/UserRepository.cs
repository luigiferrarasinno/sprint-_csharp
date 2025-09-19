using Microsoft.EntityFrameworkCore;
using InvestmentAPI.Data;
using InvestmentAPI.Models;

namespace InvestmentAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InvestmentDbContext _context;

        public UserRepository(InvestmentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Investments)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Investments)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Investments)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<Investment>> GetUserInvestmentsAsync(int userId)
        {
            return await _context.Investments
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            // Gerar ID manualmente - buscar o próximo ID disponível
            var lastId = await _context.Users
                .OrderByDescending(u => u.Id)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();
            
            user.Id = lastId + 1;
            user.CreatedAt = DateTime.UtcNow;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user != null;
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludeUserId = null)
        {
            var query = _context.Users.Where(u => u.Email == email);
            
            if (excludeUserId.HasValue)
                query = query.Where(u => u.Id != excludeUserId.Value);
                
            var user = await query.FirstOrDefaultAsync();
            return user != null;
        }
    }
}
