using InvestmentAPI.Models;
using InvestmentAPI.Repositories;

namespace InvestmentAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Validar dados de entrada
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Email é obrigatório"
                };
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Senha é obrigatória"
                };
            }

            // Buscar usuário pelo email
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Email ou senha inválidos"
                };
            }

            // Simulação de verificação de senha (na prática, seria hash comparado)
            // Para demonstração, aceita qualquer senha para usuários existentes
            
            // Gerar um "token" simulado
            var token = Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes($"{user.Id}:{user.Email}:{DateTime.UtcNow}")
            );

            return new LoginResponse
            {
                Success = true,
                Message = "Login realizado com sucesso",
                User = user,
                Token = token
            };
        }

        public async Task<object> ValidateTokenAsync(string token)
        {
            try
            {
                // Decodificar o token simulado
                var decoded = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var parts = decoded.Split(':');
                
                if (parts.Length != 3)
                {
                    return new { success = false, message = "Token inválido" };
                }

                var userId = int.Parse(parts[0]);
                var user = await _userRepository.GetByIdAsync(userId);

                if (user == null)
                {
                    return new { success = false, message = "Usuário não encontrado" };
                }

                return new
                {
                    success = true,
                    message = "Token válido",
                    user = new { user.Id, user.Name, user.Email }
                };
            }
            catch
            {
                return new { success = false, message = "Token inválido" };
            }
        }

        public async Task<object> GetTestUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            
            return new
            {
                message = "Usuários disponíveis para teste de login (use qualquer senha)",
                users = users.Select(u => new { u.Id, u.Name, u.Email }).ToList()
            };
        }
    }
}
