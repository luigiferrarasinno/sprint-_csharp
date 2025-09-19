using InvestmentAPI.Models;
using InvestmentAPI.Repositories;

namespace InvestmentAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<Investment>> GetUserInvestmentsAsync(int userId)
        {
            // Validar se o usuário existe
            if (!await _userRepository.ExistsAsync(userId))
                throw new ArgumentException("Usuário não encontrado", nameof(userId));

            return await _userRepository.GetUserInvestmentsAsync(userId);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Validar se email já existe
            if (await _userRepository.EmailExistsAsync(user.Email))
                throw new InvalidOperationException("Email já está em uso");

            // Validar dados obrigatórios
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Nome é obrigatório", nameof(user.Name));

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email é obrigatório", nameof(user.Email));

            return await _userRepository.AddAsync(user);
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            // Verificar se usuário existe
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                return null;

            // Validar se o novo email já está em uso por outro usuário
            if (await _userRepository.EmailExistsAsync(user.Email, id))
                throw new InvalidOperationException("Email já está em uso por outro usuário");

            // Validar dados obrigatórios
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Nome é obrigatório", nameof(user.Name));

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email é obrigatório", nameof(user.Email));

            // Atualizar apenas os campos permitidos
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;

            return await _userRepository.UpdateAsync(existingUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ValidateUserExistsAsync(int id)
        {
            return await _userRepository.ExistsAsync(id);
        }
    }
}
