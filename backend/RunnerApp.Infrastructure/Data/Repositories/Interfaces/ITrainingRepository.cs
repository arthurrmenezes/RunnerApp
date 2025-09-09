using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Data.Repositories.Interfaces;

public interface ITrainingRepository
{
    public Task CreateTrainingAsync(Training training, CancellationToken cancellationToken);

    public Task<Training?> GetTrainingByIdAsync(IdValueObject id, CancellationToken cancellationToken);

    public Task UpdateTrainingAsync(Training training, CancellationToken cancellationToken);

    public Task DeleteTrainingAsync(Training training, CancellationToken cancellationToken);

    public Task<Training[]> GetAllTrainingsByAccountIdAsync(IdValueObject accountId, CancellationToken cancellationToken);
}
