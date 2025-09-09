using RunnerApp.Application.Services.AccountContext.Inputs;
using RunnerApp.Application.Services.AccountContext.Interfaces;
using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Application.Services.AccountContext;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITrainingRepository _trainingRepository;

    public AccountService(IAccountRepository accountRepository, ITrainingRepository trainingRepository)
    {
        _accountRepository = accountRepository;
        _trainingRepository = trainingRepository;
    }

    public async Task<CreateAccountServiceOutput> CreateAccountServiceAsync(CreateAccountServiceInput input, CancellationToken cancellationToken)
    {
        var emailExists = await _accountRepository.IsEmailExistsAsync(input.Email, cancellationToken);
        if (emailExists)
            throw new ArgumentException("An account with this email already exists.", nameof(input.Email));

        var account = Account.Factory(
            firstName: input.FirstName,
            surname: input.Surname,
            email: input.Email);

        await _accountRepository.CreateAccountAsync(account, cancellationToken);

        return CreateAccountServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt);
    }

    public async Task<GetAccountByIdServiceOutput> GetAccountByIdServiceAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountById(id, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {id} not found.");

        return GetAccountByIdServiceOutput.Factory(
            id: account.Id.ToString(),
            firstName: account.FirstName,
            surname: account.Surname,
            email: account.Email,
            createdAt: account.CreatedAt);
    }

    public async Task<GetAllTrainingsByAccountIdServiceOutput> GetAllTrainingsByAccountIdServiceAsync(IdValueObject accountId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountById(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"Account with ID {accountId} not found.");

        var trainings = await _trainingRepository.GetAllTrainingsByAccountIdAsync(accountId, cancellationToken);

        var output = trainings.Select(training => new GetAllTrainingsByAccountIdServiceOutputTrainingOutput(
            id: training.Id.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt)).ToArray();

        var totalTrainingsOutput = output.Length;

        return GetAllTrainingsByAccountIdServiceOutput.Factory(
            totalTrainings: totalTrainingsOutput,
            trainings: output);
    }
}
