using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RecruitmentSystem.Application.DTOs.Auth;
using RecruitmentSystem.Application.Interfaces;
using RecruitmentSystem.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace RecruitmentSystem.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var users = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        var user = users.FirstOrDefault();

        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
            return null;

        var roles = await _unitOfWork.Roles.FindAsync(r => r.Id == user.RoleId);
        var role = roles.FirstOrDefault()?.Name ?? "Candidate";

        var token = GenerateJwtToken(user, role);

        return new AuthResponse
        {
            Token = token,
            Username = user.Username,
            Role = role
        };
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        var existingUsers = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        if (existingUsers.Any()) return false;

        var user = _mapper.Map<User>(request);
        user.PasswordHash = HashPassword(request.Password);
        
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        if (request.RoleId == 3) // Assuming 3 is Candidate
        {
            await _unitOfWork.Candidates.AddAsync(new Candidate { UserId = user.Id });
            await _unitOfWork.CompleteAsync();
        }

        return true;
    }

    private string GenerateJwtToken(User user, string role)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    private bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}
