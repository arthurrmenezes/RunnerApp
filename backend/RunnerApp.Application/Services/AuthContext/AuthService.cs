using Microsoft.AspNetCore.Identity;
using RunnerApp.Application.Services.AuthContext.Inputs;
using RunnerApp.Application.Services.AuthContext.Interfaces;
using RunnerApp.Application.Services.AuthContext.Outputs;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;
using RunnerApp.Infrastructure.Data.UnitOfWork.Interfaces;
using RunnerApp.Infrastructure.Identity.Entities;
using RunnerApp.Infrastructure.Identity.Services;

namespace RunnerApp.Application.Services.AuthContext;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly TokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRefreshTokenRepository refreshTokenRepository,
        TokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterUserAccountServiceOutput> RegisterServiceAsync(
        RegisterUserAccountServiceInput input, 
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            if (input.Password != input.RePassword)
                throw new ArgumentException("Passwords do not match.");

            var emailExists = await _userManager.FindByEmailAsync(input.Email);
            if (emailExists is not null)
                throw new ArgumentException($"An account with this email already exists.");

            var account = Account.Factory(
                firstName: input.FirstName,
                surname: input.Surname,
                email: input.Email,
                profilePictureUrl: null);

            var user = new ApplicationUser
            {
                UserName = $"{input.FirstName} {input.Surname}",
                Email = input.Email,
                Account = account,
                AccountId = account.Id
            };

            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            var output = RegisterUserAccountServiceOutput.Factory(
                accountId: account.Id.ToString(),
                firstName: account.FirstName.ToString(),
                surname: account.Surname.ToString(),
                email: account.Email.ToString(),
                createdAt: account.CreatedAt);

            return output;
        }, cancellationToken);
    }

    public async Task<LoginUserAccountServiceOutput> LoginServiceAsync(
        LoginUserAccountServiceInput input, 
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is null)
                throw new ArgumentException("Invalid email or password.");

            var result = await _signInManager.PasswordSignInAsync(user, input.Password, false, false);
            if (!result.Succeeded)
                throw new ArgumentException("Invalid email or password.");

            var accessToken = _tokenService.GenerateToken(user);
            var (refreshToken, refreshTokenExpirationDate) = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = RefreshToken.Factory(
                token: refreshToken,
                userId: user.Id,
                user: user,
                expirationDate: refreshTokenExpirationDate);
            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var output = LoginUserAccountServiceOutput.Factory(
                accessToken: accessToken,
                refreshToken: refreshToken);

            return output;
        }, cancellationToken);
    }

    public async Task<RefreshTokenServiceOutput> RefreshTokenServiceAsync(
        RefreshTokenServiceInput input,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.ExecuteInTransactionAsync(async () =>
        {
            var currentRefreshToken = await _refreshTokenRepository.GetRefreshTokenByJwtTokenAsync(input.RefreshToken, cancellationToken);
            if (currentRefreshToken is null || currentRefreshToken.IsExpired() || currentRefreshToken.IsRevoked())
                throw new ArgumentException("Invalid refresh token");

            var user = currentRefreshToken.User;
            if (user is null)
                throw new ArgumentException("Invalid refresh token user.");

            _refreshTokenRepository.RevokeRefreshToken(currentRefreshToken);

            var newAccessToken = _tokenService.GenerateToken(user);
            var (newRefreshToken, newExpirationDate) = _tokenService.GenerateRefreshToken();

            var newRefreshTokenEntity = RefreshToken.Factory(
                token: newRefreshToken,
                userId: user.Id,
                user: user,
                expirationDate: newExpirationDate);

            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);

            var output = RefreshTokenServiceOutput.Factory(
                accessToken: newAccessToken,
                refreshToken: newRefreshToken);

            return output;
        }, cancellationToken);
    }

    public async Task LogoutServiceAsync(
        string userId,
        CancellationToken cancellationToken)
    {
        var userGuid = Guid.Parse(userId);

        await _refreshTokenRepository.RevokeAllTokensByUserIdAsync(userGuid, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IdentityResult> ChangePasswordAsync(
        ChangePasswordInput input, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(input.UserId);
        if (user is null)
            return IdentityResult.Failed(new IdentityError
            {
                Code = "UserNotFound",
                Description = "User not found."
            });

        var result = await _userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);

        return result;
    }
}
