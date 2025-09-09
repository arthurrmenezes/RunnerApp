using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Interfaces;
using RunnerApp.Application.Services.TrainingContext.Outputs;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Application.Services.TrainingContext;

public class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _trainingRepository;
    private readonly IAccountRepository _accountRepository;

    public TrainingService(ITrainingRepository trainingRepository, IAccountRepository accountRepository)
    {
        _trainingRepository = trainingRepository;
        _accountRepository = accountRepository;
    }

    public async Task<CreateTrainingServiceOutput> CreateTrainingServiceAsync(
        CreateTrainingServiceInput input, 
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountById(input.AccountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException("No account with ID {id} was found.");

        var training = Training.Factory(
            location: input.Location,
            distance: input.Distance,
            duration: input.Duration,
            date: input.Date,
            accountId: input.AccountId);

        await _trainingRepository.CreateTrainingAsync(training, cancellationToken);

        return CreateTrainingServiceOutput.Factory(
            id: training.Id.ToString(),
            account: new CreateTrainingServiceOutputAccountOutput(
                id: training.Account.Id.ToString(),
                firstName: training.Account.FirstName,
                surname: training.Account.Surname,
                email: training.Account.Email,
                createdAt: training.Account.CreatedAt),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt);
    }

    public async Task<GetTrainingByIdServiceOutput> GetTrainingByIdServiceAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(id, cancellationToken);
        if (training is null)
            throw new KeyNotFoundException($"No training with ID {id} was found.");

        var account = await _accountRepository.GetAccountById(training.AccountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"No account with ID {training.AccountId} was found.");

        return GetTrainingByIdServiceOutput.Factory(
            id: training.Id.ToString(),
            account: new GetTrainingByIdServiceOutputAccountOutput(
                id: account.Id.ToString(),
                firstName: account.FirstName.ToString(),
                surname: account.Surname.ToString(),
                email: account.Email.ToString(),
                createdAt: account.CreatedAt),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt);
    }

    public async Task<UpdateTrainingByIdServiceOutput> UpdateTrainingByIdServiceAsync(
        IdValueObject id, 
        UpdateTrainingByIdServiceInput input, 
        CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(id, cancellationToken);
        if (training is null)
            throw new KeyNotFoundException($"No training with ID {id} was found.");

        var account = await _accountRepository.GetAccountById(training.AccountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"No account with ID {training.AccountId} was found.");

        training.UpdateTrainingDetails(
            location: input.Location,
            distance: input.Distance,
            duration: input.Duration,
            date: input.Date);

        await _trainingRepository.UpdateTrainingAsync(training, cancellationToken);

        return UpdateTrainingByIdServiceOutput.Factory(
            id: training.Id.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt,
            account: new UpdateTrainingByIdServiceOutputAccountOutput(
                id: account.Id.ToString(),
                firstName: account.FirstName.ToString(),
                surname: account.Surname.ToString(),
                email: account.Email.ToString(),
                createdAt: account.CreatedAt));
    }

    public async Task DeleteTrainingByIdServiceAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(id, cancellationToken);

        if (training is null)
            throw new KeyNotFoundException($"No training with ID {id} was found.");

        await _trainingRepository.DeleteTrainingAsync(training, cancellationToken);
    }
}
