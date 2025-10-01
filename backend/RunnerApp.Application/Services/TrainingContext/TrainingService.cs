using RunnerApp.Application.Services.AccountContext.Outputs;
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
        var account = await _accountRepository.GetAccountByIdAsync(input.AccountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"No account with ID {input.AccountId} was found.");

        var training = Training.Factory(
            location: input.Location,
            distance: input.Distance,
            duration: input.Duration,
            date: input.Date,
            accountId: input.AccountId);

        await _trainingRepository.CreateTrainingAsync(training, cancellationToken);

        return CreateTrainingServiceOutput.Factory(
            trainingId: training.Id.ToString(),
            accountId: account.Id.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt);
    }

    public async Task<GetTrainingByIdServiceOutput> GetTrainingByIdServiceAsync(
        IdValueObject trainingId,
        IdValueObject accountId,
        CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(trainingId, cancellationToken);
        if (training is null)
            throw new KeyNotFoundException($"No training with ID {trainingId} was found.");

        if (training.AccountId.ToString() != accountId.ToString())
            throw new UnauthorizedAccessException("The training does not belong to the specified account.");

        return GetTrainingByIdServiceOutput.Factory(
            trainingId: training.Id.ToString(),
            accountId: accountId.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt);
    }

    public async Task<UpdateTrainingByIdServiceOutput> UpdateTrainingByIdServiceAsync(
        IdValueObject trainingId,
        IdValueObject accountId,
        UpdateTrainingByIdServiceInput input, 
        CancellationToken cancellationToken)
    {
        if (input.Location is null &&
            input.Distance is null &&
            input.Duration is null &&
            input.Date is null)
            throw new ArgumentException("At least one field must be provided for the update.");

        var training = await _trainingRepository.GetTrainingByIdAsync(trainingId, cancellationToken);
        if (training is null)
            throw new KeyNotFoundException($"No training with ID {trainingId} was found.");

        if (training.AccountId.ToString() != accountId.ToString())
            throw new UnauthorizedAccessException("You cannot update a training that does not belong to your account.");

        training.UpdateTrainingDetails(
            location: input.Location,
            distance: input.Distance,
            duration: input.Duration,
            date: input.Date);

        await _trainingRepository.UpdateTrainingAsync(training, cancellationToken);

        var account = await _accountRepository.GetAccountByIdAsync(accountId, cancellationToken);
        if (account is null)
            throw new KeyNotFoundException($"No account with ID {accountId} was found.");

        return UpdateTrainingByIdServiceOutput.Factory(
            id: training.Id.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt,
            account: new UpdateTrainingByIdServiceOutputAccountOutput(
                id: accountId.ToString(),
                firstName: account.FirstName.ToString(),
                surname: account.Surname.ToString(),
                email: account.Email.ToString(),
                createdAt: account.CreatedAt));
    }

    public async Task DeleteTrainingByIdServiceAsync(
        IdValueObject trainingId,
        IdValueObject accountId,
        CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(trainingId, cancellationToken);
        if (training is null)
            throw new KeyNotFoundException($"No training with ID {trainingId} was found.");

        if (training.AccountId.ToString() != accountId.ToString())
            throw new UnauthorizedAccessException("You cannot delete a training that does not belong to your account.");

        await _trainingRepository.DeleteTrainingAsync(training, cancellationToken);
    }

	public async Task<GetAllTrainingsByAccountIdServiceOutput> GetAllTrainingsByAccountIdServiceAsync(
        IdValueObject accountId,
        IdValueObject callerAccountId,
        CancellationToken cancellationToken)
	{
        if (accountId.ToString() != callerAccountId.ToString())
            throw new UnauthorizedAccessException("You are not allowed to view trainings of other accounts.");

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
