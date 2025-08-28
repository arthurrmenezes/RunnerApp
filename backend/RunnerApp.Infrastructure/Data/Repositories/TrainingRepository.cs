using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Infrastructure.Data.Repositories;

public class TrainingRepository : ITrainingRepository
{
    private readonly DataContext _dataContext;

    public TrainingRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task CreateTrainingAsync(Training training)
    {
        await _dataContext.Trainings.AddAsync(training);
        await _dataContext.SaveChangesAsync();
    }
}
