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
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Training?> GetTrainingByIdAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        var training = await _dataContext.Trainings.FirstOrDefaultAsync(t => t.Id.Equals(id));
        return training;
    }
}
