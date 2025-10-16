using InvestmentAPI.Models;

namespace InvestmentAPI.Services
{
    public interface IStockQuoteService
    {
        /// <summary>
        /// Consulta a cotação de um símbolo de ação na API Alpha Vantage
        /// </summary>
        /// <param name="symbol">Símbolo da ação (ex: PETR4.SA, VALE3.SA)</param>
        /// <returns>Dados da cotação global</returns>
        Task<GlobalQuoteResponse?> GetGlobalQuoteAsync(string symbol);
    }
}
