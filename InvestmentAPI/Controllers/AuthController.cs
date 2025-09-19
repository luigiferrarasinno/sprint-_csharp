using Microsoft.AspNetCore.Mvc;
using InvestmentAPI.Models;
using InvestmentAPI.Services;

namespace InvestmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse
                {
                    Success = false,
                    Message = "Dados inválidos"
                });
            }

            var response = await _authService.LoginAsync(request);

            if (!response.Success)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }

        // POST: api/Auth/validate-token
        [HttpPost("validate-token")]
        public async Task<ActionResult<object>> ValidateToken([FromBody] string token)
        {
            var result = await _authService.ValidateTokenAsync(token);
            
            // Se o resultado contém success = false, retorna Unauthorized
            var resultDict = result as dynamic;
            if (resultDict?.success == false)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        // GET: api/Auth/test-users
        [HttpGet("test-users")]
        public async Task<ActionResult<object>> GetTestUsers()
        {
            var result = await _authService.GetTestUsersAsync();
            return Ok(result);
        }
    }
}
