using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvestmentAPI.Models
{
    /// <summary>
    /// Modelo para armazenar cotação de ações obtidas da API Alpha Vantage
    /// </summary>
    public class StockQuote
    {
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("price")]
        public string? Price { get; set; }

        [JsonPropertyName("volume")]
        public string? Volume { get; set; }

        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; set; }
    }

    /// <summary>
    /// Resposta da API Global Quote do Alpha Vantage
    /// </summary>
    public class GlobalQuoteResponse
    {
        [JsonPropertyName("Global Quote")]
        public GlobalQuoteData? GlobalQuote { get; set; }

        [JsonPropertyName("Note")]
        public string? Note { get; set; }

        [JsonPropertyName("Information")]
        public string? Information { get; set; }

        [JsonPropertyName("Error Message")]
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// Dados da cotação global
    /// </summary>
    public class GlobalQuoteData
    {
        [JsonPropertyName("01. symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("02. open")]
        public string? Open { get; set; }

        [JsonPropertyName("03. high")]
        public string? High { get; set; }

        [JsonPropertyName("04. low")]
        public string? Low { get; set; }

        [JsonPropertyName("05. price")]
        public string? Price { get; set; }

        [JsonPropertyName("06. volume")]
        public string? Volume { get; set; }

        [JsonPropertyName("07. latest trading day")]
        public string? LatestTradingDay { get; set; }

        [JsonPropertyName("08. previous close")]
        public string? PreviousClose { get; set; }

        [JsonPropertyName("09. change")]
        public string? Change { get; set; }

        [JsonPropertyName("10. change percent")]
        public string? ChangePercent { get; set; }
    }

    /// <summary>
    /// Request para consultar cotação
    /// </summary>
    public class StockQuoteRequest
    {
        [Required]
        [StringLength(10)]
        public string Symbol { get; set; } = string.Empty;
    }
}
