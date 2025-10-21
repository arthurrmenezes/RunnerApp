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

    public async Task<Training?> GetTrainingByIdAsync(IdValueObject trainingId, CancellationToken cancellationToken)
    {
        return await _dataContext.Trainings.FirstOrDefaultAsync(t => t.Id.Equals(trainingId));
    }

    public void DeleteTraining(Training training)
    {
        _dataContext.Trainings.Remove(training);
    }

    public async Task<int> GetTotalTrainingsCountByAccountIdAsync(IdValueObject accountId, CancellationToken cancellationToken)
    {
        return await _dataContext.Trainings.CountAsync(t => t.AccountId.Equals(accountId), cancellationToken);
    }

    public async Task<Training[]> GetAllTrainingsByAccountIdAsync(IdValueObject accountId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var trainingsToSkip = (pageNumber - 1) * pageSize;

        return await _dataContext.Trainings
            .Where(t => t.AccountId.Equals(accountId))
            .OrderByDescending(t => t.Date)
            .Skip(trainingsToSkip)
            .Take(pageSize)
            .ToArrayAsync(cancellationToken);
    }
}
