using Microsoft.AspNetCore.Http;
using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Files.Outputs;

namespace RunnerApp.Application.Services.AccountContext.Interfaces;

public interface IAccountService
{
    public Task<GetUserAccountDetailsServiceOutput> GetUserAccountDetailsServiceAsync(
        IdValueObject accountId,
        CancellationToken cancellationToken);

    public Task<UpdateAccountServiceOutput> UpdateAccountServiceAsync(
        IdValueObject accountId,
        UpdateAccountServiceInput input,
        CancellationToken cancellationToken);

    public Task<UploadFileServiceOutput> UploadProfilePictureServiceAsync(
        IdValueObject accountId,
        IFormFile pictureFile,
        CancellationToken cancellationToken);
}
