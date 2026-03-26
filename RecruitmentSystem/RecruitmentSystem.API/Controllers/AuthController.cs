using Microsoft.AspNetCore.Mvc;
using RecruitmentSystem.Application.DTOs.Auth;
using RecruitmentSystem.Application.Interfaces;

namespace RecruitmentSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        if (response == null) return Unauthorized("Invalid email or password");
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (!result) return BadRequest("Email already exists");
        return Ok("Registration successful");
    }
}
