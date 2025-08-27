using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Outputs;

namespace RunnerApp.Application.Services.TrainingContext.Interfaces;

public interface ITrainingService
{
    public Task<CreateTrainingServiceOutput> CreateTrainingServiceAsync(
        CreateTrainingServiceInput input,
        CancellationToken cancellationToken);
}
