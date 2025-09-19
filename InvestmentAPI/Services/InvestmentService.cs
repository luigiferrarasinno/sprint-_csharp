using InvestmentAPI.Models;
using InvestmentAPI.Repositories;

namespace InvestmentAPI.Services
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IUserRepository _userRepository;

        public InvestmentService(IInvestmentRepository investmentRepository, IUserRepository userRepository)
        {
            _investmentRepository = investmentRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Investment>> GetAllInvestmentsAsync()
        {
            return await _investmentRepository.GetAllAsync();
        }

        public async Task<Investment?> GetInvestmentByIdAsync(int id)
        {
            return await _investmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByTypeAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Tipo é obrigatório", nameof(type));

            return await _investmentRepository.GetByTypeAsync(type);
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByUserIdAsync(int userId)
        {
            // Validar se o usuário existe
            if (!await _userRepository.ExistsAsync(userId))
                throw new ArgumentException("Usuário não encontrado", nameof(userId));

            return await _investmentRepository.GetByUserIdAsync(userId);
        }

        public async Task<Investment?> CreateInvestmentAsync(Investment investment)
        {
            // Validar se o usuário existe
            if (!await _userRepository.ExistsAsync(investment.UserId))
                throw new ArgumentException("Usuário não encontrado");

            // Validar dados obrigatórios
            if (string.IsNullOrWhiteSpace(investment.Name))
                throw new ArgumentException("Nome é obrigatório", nameof(investment.Name));

            if (string.IsNullOrWhiteSpace(investment.Type))
                throw new ArgumentException("Tipo é obrigatório", nameof(investment.Type));

            if (investment.Amount <= 0)
                throw new ArgumentException("Valor deve ser maior que zero", nameof(investment.Amount));

            // Validar retorno esperado se fornecido
            if (investment.ExpectedReturn.HasValue && investment.ExpectedReturn < 0)
                throw new ArgumentException("Retorno esperado não pode ser negativo", nameof(investment.ExpectedReturn));

            var createdInvestment = await _investmentRepository.AddAsync(investment);
            
            // Incluir o usuário na resposta
            var user = await _userRepository.GetByIdAsync(investment.UserId);
            createdInvestment.User = user!;

            return createdInvestment;
        }

        public async Task<Investment?> UpdateInvestmentAsync(int id, Investment investment)
        {
            // Verificar se investimento existe
            var existingInvestment = await _investmentRepository.GetByIdAsync(id);
            if (existingInvestment == null)
                return null;

            // Verificar se o usuário existe (se foi alterado)
            if (existingInvestment.UserId != investment.UserId)
            {
                if (!await _userRepository.ExistsAsync(investment.UserId))
                    throw new ArgumentException("Usuário não encontrado");
            }

            // Validar dados obrigatórios
            if (string.IsNullOrWhiteSpace(investment.Name))
                throw new ArgumentException("Nome é obrigatório", nameof(investment.Name));

            if (string.IsNullOrWhiteSpace(investment.Type))
                throw new ArgumentException("Tipo é obrigatório", nameof(investment.Type));

            if (investment.Amount <= 0)
                throw new ArgumentException("Valor deve ser maior que zero", nameof(investment.Amount));

            // Validar retorno esperado se fornecido
            if (investment.ExpectedReturn.HasValue && investment.ExpectedReturn < 0)
                throw new ArgumentException("Retorno esperado não pode ser negativo", nameof(investment.ExpectedReturn));

            // Atualizar apenas os campos permitidos
            existingInvestment.Name = investment.Name;
            existingInvestment.Type = investment.Type;
            existingInvestment.Amount = investment.Amount;
            existingInvestment.ExpectedReturn = investment.ExpectedReturn;
            existingInvestment.Description = investment.Description;
            existingInvestment.UserId = investment.UserId;

            return await _investmentRepository.UpdateAsync(existingInvestment);
        }

        public async Task<bool> DeleteInvestmentAsync(int id)
        {
            return await _investmentRepository.DeleteAsync(id);
        }

        public async Task<object> GetInvestmentsSummaryAsync()
        {
            return await _investmentRepository.GetSummaryAsync();
        }
    }
}
