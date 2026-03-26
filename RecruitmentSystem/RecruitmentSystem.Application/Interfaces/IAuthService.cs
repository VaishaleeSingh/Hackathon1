using RecruitmentSystem.Application.DTOs.Auth;

namespace RecruitmentSystem.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    Task<bool> RegisterAsync(RegisterRequest request);
}
