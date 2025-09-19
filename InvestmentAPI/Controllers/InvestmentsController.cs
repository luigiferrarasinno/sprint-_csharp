using Microsoft.AspNetCore.Mvc;
using InvestmentAPI.Models;
using InvestmentAPI.Services;

namespace InvestmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentService _investmentService;

        public InvestmentsController(IInvestmentService investmentService)
        {
            _investmentService = investmentService;
        }

        // GET: api/Investments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investment>>> GetInvestments()
        {
            var investments = await _investmentService.GetAllInvestmentsAsync();
            return Ok(investments);
        }

        // GET: api/Investments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investment>> GetInvestment(int id)
        {
            var investment = await _investmentService.GetInvestmentByIdAsync(id);

            if (investment == null)
            {
                return NotFound(new { message = "Investimento não encontrado" });
            }

            return Ok(investment);
        }

        // GET: api/Investments/by-type/{type}
        [HttpGet("by-type/{type}")]
        public async Task<ActionResult<IEnumerable<Investment>>> GetInvestmentsByType(string type)
        {
            try
            {
                var investments = await _investmentService.GetInvestmentsByTypeAsync(type);
                return Ok(investments);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Investments/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<Investment>>> GetInvestmentsByUser(int userId)
        {
            try
            {
                var investments = await _investmentService.GetInvestmentsByUserIdAsync(userId);
                return Ok(investments);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/Investments
        [HttpPost]
        public async Task<ActionResult<Investment>> CreateInvestment(CreateInvestmentRequest request)
        {
            try
            {
                var investment = new Investment
                {
                    Name = request.Name,
                    Type = request.Type,
                    Amount = request.Amount,
                    ExpectedReturn = request.ExpectedReturn,
                    Description = request.Description,
                    UserId = request.UserId
                };

                var createdInvestment = await _investmentService.CreateInvestmentAsync(investment);
                return CreatedAtAction(nameof(GetInvestment), new { id = createdInvestment!.Id }, createdInvestment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/Investments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvestment(int id, UpdateInvestmentRequest request)
        {
            try
            {
                var investment = new Investment
                {
                    Id = id,
                    Name = request.Name,
                    Type = request.Type,
                    Amount = request.Amount,
                    ExpectedReturn = request.ExpectedReturn,
                    Description = request.Description,
                    UserId = request.UserId
                };

                var updatedInvestment = await _investmentService.UpdateInvestmentAsync(id, investment);
                if (updatedInvestment == null)
                {
                    return NotFound(new { message = "Investimento não encontrado" });
                }

                return Ok(updatedInvestment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Investments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestment(int id)
        {
            var deleted = await _investmentService.DeleteInvestmentAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Investimento não encontrado" });
            }

            return Ok(new { message = "Investimento deletado com sucesso" });
        }

        // GET: api/Investments/summary
        [HttpGet("summary")]
        public async Task<ActionResult<object>> GetInvestmentsSummary()
        {
            var summary = await _investmentService.GetInvestmentsSummaryAsync();
            return Ok(summary);
        }
    }
}
