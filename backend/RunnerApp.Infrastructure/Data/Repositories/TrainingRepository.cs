using Microsoft.EntityFrameworkCore;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Infrastructure.Data.Repositories;

public class TrainingRepository : ITrainingRepository
{
    private readonly DataContext _dataContext;

    public TrainingRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateTrainingAsync(Training training, CancellationToken cancellationToken)
    {
        await _dataContext.Trainings.AddAsync(training, cancellationToken);
    }

    public async Task<Training?> GetTrainingByIdAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        return await _dataContext.Trainings.FirstOrDefaultAsync(t => t.Id.Equals(id));
    }

    public void DeleteTraining(Training training, CancellationToken cancellationToken)
    {
        _dataContext.Trainings.Remove(training);
    }

    public async Task<Training[]> GetAllTrainingsByAccountIdAsync(IdValueObject accountId, CancellationToken cancellationToken)
    {
        return await _dataContext.Trainings
            .Where(t => t.AccountId.Equals(accountId))
            .ToArrayAsync(cancellationToken);
    }
}
