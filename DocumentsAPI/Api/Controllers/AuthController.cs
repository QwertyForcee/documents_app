using DocumentsAPI.Api.DTO.Auth;
using DocumentsAPI.Core.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpDTO dto)
        {
            var accessToken = await _authService.SignUpAsync(dto.Email, dto.Password, dto.Name);
            return Ok(new AuthResponse(accessToken));
        }


        [HttpPost("sign-in")]
        public async Task<ActionResult<AuthResponse>> SignIn(SignInDTO dto)
        {
            var accessToken = await _authService.SignInAsync(dto.Email, dto.Password);
            return Ok(new AuthResponse(accessToken));
        }
    }
}
