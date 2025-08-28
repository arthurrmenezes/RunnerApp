using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;

namespace RunnerApp.Infrastructure.Data.Repositories.Interfaces;

public interface ITrainingRepository
{
    public Task CreateTrainingAsync(Training training);
}
