using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Data.Repositories.Interfaces;

public interface ITrainingRepository
{
    public Task CreateTrainingAsync(Training training, CancellationToken cancellationToken);

    public Task<Training?> GetTrainingByIdAsync(IdValueObject id, CancellationToken cancellationToken);

    public void DeleteTraining(Training training);

    public Task<int> GetTotalTrainingsCountByAccountIdAsync(IdValueObject accountId, CancellationToken cancellationToken);

    public Task<Training[]> GetAllTrainingsByAccountIdAsync(IdValueObject accountId, int pageNumber, int pageSize, CancellationToken cancellationToken);
}
