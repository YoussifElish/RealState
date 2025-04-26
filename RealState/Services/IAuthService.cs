using RealState.Abstactions;
using RealState.Contracts.Auth;
using RealState.Contracts.Authentication;
using System.Diagnostics.Contracts;

namespace RealState.Services;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken = default);
    Task<Result> RegisterAsync(Contracts.Auth.RegisterRequest request, CancellationToken cancellationToken);
    Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
    Task<Result> SentResetPasswordCodeAsync(string email);
    Task<Result> ResetPasswordAsync(Contracts.Authentication.ResetPasswordRequest request);
}
