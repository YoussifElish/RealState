using RealState.Entities;
using RealState.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealState.Abstactions;
using RealState.Contracts.Auth;
using RealState.Contracts.Authentication;
using RealState.Persistence;
using RealState.Helpers;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using RealState.Authentication;
using System.Security.Cryptography;
using Mapster;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using RealState.Abstactions.Consts;

namespace RealState.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,ApplicationDbContext context,IEmailSender emailSender,IJwtProvider jwtProvider,ILogger<AuthService> logger) : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly int _refreshtokenexpirydate = 1;

    public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Failure<AuthResponse>(AuthErrors.InavlidPassword);

        if (user.IsDisabled)
            return Result.Failure<AuthResponse>(AuthErrors.DisabledUser);

        var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

        if (result.Succeeded)
        {
            var (userRoles, userPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);

            var (token, ExpiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshtokenexpirydate);

            user.RefreshTokens.Add(new RefreshTokens
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration,
            });

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, ExpiresIn, refreshToken, refreshTokenExpiration);
            return Result.Success<AuthResponse>(response);

        }
        var error = result.IsNotAllowed ? AuthErrors.EmailNotConfirmed : result.IsLockedOut ? AuthErrors.LockedUser : AuthErrors.InavlidPassword;

        return Result.Failure<AuthResponse>(error);
    }

    public async Task<Result> RegisterAsync(Contracts.Auth.RegisterRequest request, CancellationToken cancellationToken)
    {
        var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (emailIsExist)
            return Result.Failure(AuthErrors.DuplicatedEmail);
        var user = request.Adapt<ApplicationUser>();
        user.EmailConfirmed = true;
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
           


            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));


    }


    public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.ValidateToken(token);

        if (userId is null)
            return Result.Failure(AuthErrors.InvalidToken);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure(AuthErrors.InavlidUser);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

        if (userRefreshToken is null)
            return Result.Failure(AuthErrors.InavlidUser);

        userRefreshToken.RevokedOn = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        return Result.Success();
    }




    public async Task<Result> SentResetPasswordCodeAsync(string email)
    {
        if (await _userManager.FindByEmailAsync(email) is not { } user)
            return Result.Success();

        if (!user.EmailConfirmed)
            return Result.Failure(AuthErrors.EmailNotConfirmed);

        var random = new Random();
        var code = random.Next(100000, 999999).ToString();

        user.ResetPasswordCode = code;
        user.ResetPasswordCodeExpiration = DateTime.UtcNow.AddMinutes(10); 

        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Reset Code: {Code}", code);

        await SendResetPasswordEmail(user, code);

        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(Contracts.Authentication.ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null || !user.EmailConfirmed)
            return Result.Failure(AuthErrors.InvalidCode);

        if (user.ResetPasswordCode != request.Code || user.ResetPasswordCodeExpiration < DateTime.UtcNow)
            return Result.Failure(AuthErrors.InvalidCode);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (result.Succeeded)
        {
            user.ResetPasswordCode = null;
            user.ResetPasswordCodeExpiration = null;
            await _userManager.UpdateAsync(user);
            return Result.Success();
        }

        var error = result.Errors.First();
        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status401Unauthorized));
    }




    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken = default)
    {
        var UserId = _jwtProvider.ValidateToken(Token);
        if (UserId is null)
            return null;


        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null)
            return null;



        var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);
        if (UserRefreshToken is null)
            return null;


        UserRefreshToken.RevokedOn = DateTime.UtcNow;
        var (userRoles, userPermissions) = await GetUserRolesAndPermissions(user, cancellationToken);
        var (newToken, ExpiresIn) = _jwtProvider.GenerateToken(user, userRoles, userPermissions);
        var newRefreshtoken = GenerateRefreshToken();
        var RefreshtokenExpiration = DateTime.UtcNow.AddDays(_refreshtokenexpirydate);
        user.RefreshTokens.Add(new RefreshTokens
        {
            Token = newRefreshtoken,
            ExpiresOn = RefreshtokenExpiration
        });
        await _userManager.UpdateAsync(user);
        var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, ExpiresIn, newRefreshtoken, RefreshtokenExpiration);

        return Result.Success(response);
    }

    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
    private async Task SendResetPasswordEmail(ApplicationUser user, string code)
    {
       
        var emailBody = EmailBodyBuider.GenerateEmailBody("ForgetPassword", new Dictionary<string, string>
                {
                    {"{{Product_Name}}",user.FirstName},
                    {"{{name}}",user.FirstName},
                    {"{{action_url}}",$"{code}"}
                });
        BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅House Hub: Reset Password", emailBody));

        await Task.CompletedTask;

    }

    private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndPermissions(ApplicationUser user, CancellationToken cancellationToken)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        //var userPermissions = await _context.Roles.Join(_context.RoleClaims, role => role.Id, claim => claim.RoleId, (role, claim) => new { role, claim }).Where(x => userRoles.Contains(x.role.Name!)).Select(x => x.claim.ClaimValue!).Distinct().ToListAsync(cancellationToken);

        var userPermissions = await (from r in _context.Roles
                                     join p in _context.RoleClaims
                                     on r.Id equals p.RoleId
                                     where userRoles.Contains(r.Name!) && r.IsDeleted == false
                                     select p.ClaimValue!
            ).Distinct().ToListAsync(cancellationToken);

        return (userRoles, userPermissions);

    }
}
