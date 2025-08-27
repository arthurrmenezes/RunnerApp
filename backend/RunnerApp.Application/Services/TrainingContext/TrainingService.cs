using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Interfaces;
using RunnerApp.Application.Services.TrainingContext.Outputs;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;

namespace RunnerApp.Application.Services.TrainingContext;

public class TrainingService : ITrainingService
{
    public Task<CreateTrainingServiceOutput> CreateTrainingServiceAsync(CreateTrainingServiceInput input, CancellationToken cancellationToken)
    {
        var training = Training.Factory(
            location: input.Location,
            distance: input.Distance,
            duration: input.Duration,
            date: input.Date);

        return Task.FromResult(CreateTrainingServiceOutput.Factory(
            id: training.Id,
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt));
    }
}
