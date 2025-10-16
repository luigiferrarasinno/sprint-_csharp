using InvestmentAPI.Models;
using System.Text.Json;

namespace InvestmentAPI.Services
{
    public class StockQuoteService : IStockQuoteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "7TDGUCG3EQNHLWXA";
        private const string BaseUrl = "https://www.alphavantage.co/query";

        public StockQuoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Consulta a cotação de um símbolo de ação na API Alpha Vantage
        /// </summary>
        /// <param name="symbol">Símbolo da ação (ex: PETR4.SA, VALE3.SA)</param>
        /// <returns>Dados da cotação global ou null se houver erro</returns>
        public async Task<GlobalQuoteResponse?> GetGlobalQuoteAsync(string symbol)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(symbol))
                {
                    throw new ArgumentException("Símbolo não pode ser vazio", nameof(symbol));
                }

                // Construir URL com parâmetros
                var url = $"{BaseUrl}?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_apiKey}";

                // Fazer requisição GET
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Desserializar resposta
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = JsonSerializer.Deserialize<GlobalQuoteResponse>(content, options);
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException($"Erro ao consultar API do Alpha Vantage: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Erro ao desserializar resposta da API: {ex.Message}", ex);
            }
        }
    }
}
