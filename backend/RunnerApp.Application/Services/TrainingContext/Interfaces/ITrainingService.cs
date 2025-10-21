using RunnerApp.Application.Services.AccountContext.Outputs;
using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Outputs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.TrainingContext.Interfaces;

public interface ITrainingService
{
    public Task<CreateTrainingServiceOutput> CreateTrainingServiceAsync(
        CreateTrainingServiceInput input,
        CancellationToken cancellationToken);

    public Task<GetTrainingByIdServiceOutput> GetTrainingByIdServiceAsync(
        IdValueObject trainingId,
        IdValueObject accountId,
        CancellationToken cancellationToken);

    public Task<UpdateTrainingByIdServiceOutput> UpdateTrainingByIdServiceAsync(
        IdValueObject trainingId,
        IdValueObject accountId,
        UpdateTrainingByIdServiceInput input,
        CancellationToken cancellationToken);

    public Task DeleteTrainingByIdServiceAsync(
        IdValueObject trainingId,
        IdValueObject accountId,
        CancellationToken cancellationToken);

    public Task<GetAllTrainingsForCurrentUserServiceOutput> GetAllTrainingsForCurrentUserServiceAsync(
        IdValueObject accountId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken);
}
