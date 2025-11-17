using Microsoft.AspNetCore.Http;
using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;
using RunnerApp.Infrastructure.Data.UnitOfWork.Interfaces;
using RunnerApp.Infrastructure.Files.Interfaces;

namespace RunnerApp.Application.Services.AccountContext;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;

    public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IFileService fileService)
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }

    public async Task<GetUserAccountDetailsServiceOutput> GetUserAccountDetailsServiceAsync(
        IdValueObject accountId,
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} not found.");

        var output = GetUserAccountDetailsServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            profilePictureUrl: account.ProfilePictureUrl,
            createdAt: account.CreatedAt);

        return output;
    }

    public async Task<UpdateAccountServiceOutput> UpdateAccountServiceAsync(
        IdValueObject accountId,
        UpdateAccountServiceInput input, 
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} was not found.");

        account.UpdateAccountDetails(
            firstName: input.FirstName,
            surname: input.Surname,
            email: input.Email,
            profilePictureUrl: input.ProfilePictureUrl);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var output = UpdateAccountServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            profilePictureUrl: account.ProfilePictureUrl,
            createdAt: account.CreatedAt);

        return output;
    }
    
    public async Task<UploadProfilePictureServiceOutput> UploadProfilePictureServiceAsync(
        IdValueObject accountId, 
        IFormFile pictureFile, 
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} was not found.");

        if (!string.IsNullOrEmpty(account.ProfilePictureUrl))
            _fileService.DeleteFile(account.ProfilePictureUrl);

        var response = await _fileService.UploadFileServiceAsync(pictureFile, cancellationToken);

        account.SetProfilePicture(response.FileUrl);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UploadProfilePictureServiceOutput.Factory(
            fileName: response.FileName,
            fileUrl: response.FileUrl,
            fileSize: response.FileSize);
    }

    public async Task<GetProfilePictureByAccountIdServiceOutput> GetProfilePictureByAccountIdServiceAsync(
        IdValueObject accountId, 
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} not found.");

        if (account.ProfilePictureUrl is null)
            throw new KeyNotFoundException($"Account with ID {accountId} does not have a profile picture.");

        var file = await _fileService.GetFileByPathAsync(account.ProfilePictureUrl, cancellationToken);
        if (file is null)
            throw new FileNotFoundException($"File was not found.");

        var fileType = Path.GetExtension(account.ProfilePictureUrl);

        var fileTypeOutput = fileType switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream"
        };
        var output = GetProfilePictureByAccountIdServiceOutput.Factory(
            profilePicture: file,
            fileType: fileTypeOutput);

        return output;
    }
}
