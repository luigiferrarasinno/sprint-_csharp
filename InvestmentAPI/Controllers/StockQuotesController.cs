using Microsoft.AspNetCore.Mvc;
using InvestmentAPI.Models;
using InvestmentAPI.Services;

namespace InvestmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockQuotesController : ControllerBase
    {
        private readonly IStockQuoteService _stockQuoteService;

        public StockQuotesController(IStockQuoteService stockQuoteService)
        {
            _stockQuoteService = stockQuoteService;
        }

        /// <summary>
        /// Obtém a cotação global de um símbolo de ação
        /// </summary>
        /// <param name="symbol">Símbolo da ação (ex: PETR4.SA, VALE3.SA)</param>
        /// <returns>Dados da cotação global</returns>
        /// <remarks>
        /// Este endpoint consulta a API pública do Alpha Vantage para obter informações de cotação de ações.
        /// 
        /// Exemplo de requisição:
        /// GET /api/stockquotes/quote?symbol=PETR4.SA
        /// 
        /// Resposta de sucesso (200 OK):
        /// {
        ///   "Global Quote": {
        ///     "01. symbol": "PETR4.SA",
        ///     "02. price": "25.45",
        ///     "03. volume": "1000000",
        ///     "04. timestamp": "2025-10-16 16:30:00",
        ///     "05. price change": "+0.45",
        ///     "06. price change percent": "+1.80%",
        ///     "07. bid price": "25.43",
        ///     "08. ask price": "25.47",
        ///     "09. bid size": "500000",
        ///     "10. ask size": "500000",
        ///     "11. trade date": "2025-10-16"
        ///   }
        /// }
        /// </remarks>
        [HttpGet("quote")]
        public async Task<ActionResult<GlobalQuoteResponse>> GetStockQuote([FromQuery] string symbol)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(symbol))
                {
                    return BadRequest(new
                    {
                        message = "Símbolo não pode ser vazio",
                        example = "PETR4.SA"
                    });
                }

                var quote = await _stockQuoteService.GetGlobalQuoteAsync(symbol);

                if (quote == null)
                {
                    return NotFound(new { message = "Cotação não encontrada" });
                }

                // Verificar se houve erro na resposta da API
                if (!string.IsNullOrEmpty(quote.ErrorMessage) || !string.IsNullOrEmpty(quote.Information))
                {
                    return StatusCode(503, new
                    {
                        message = "Erro ao consultar API do Alpha Vantage",
                        error = quote.ErrorMessage ?? quote.Information,
                        note = quote.Note
                    });
                }

                return Ok(quote);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(503, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao processar requisição", error = ex.Message });
            }
        }

    }
}
