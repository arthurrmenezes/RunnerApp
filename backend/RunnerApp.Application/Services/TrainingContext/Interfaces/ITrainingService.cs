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
        IdValueObject id,
        CancellationToken cancellationToken);

    public Task<UpdateTrainingByIdServiceOutput> UpdateTrainingByIdServiceAsync(
        IdValueObject id,
        UpdateTrainingByIdServiceInput input,
        CancellationToken cancellationToken);

    public Task DeleteTrainingByIdServiceAsync(
        IdValueObject id,
        CancellationToken cancellationToken);

    public Task<GetAllTrainingsByAccountIdServiceOutput> GetAllTrainingsByAccountIdServiceAsync(
        IdValueObject accountId,
        CancellationToken cancellationToken);


}
