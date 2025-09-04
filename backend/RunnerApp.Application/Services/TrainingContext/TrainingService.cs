using RunnerApp.Application.Services.TrainingContext.Inputs;
using RunnerApp.Application.Services.TrainingContext.Interfaces;
using RunnerApp.Application.Services.TrainingContext.Outputs;
using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;
using RunnerApp.Infrastructure.Data.Repositories.Interfaces;

namespace RunnerApp.Application.Services.TrainingContext;

public class TrainingService : ITrainingService
{
    private readonly ITrainingRepository _trainingRepository;

    public TrainingService(ITrainingRepository trainingRepository)
    {
        _trainingRepository = trainingRepository;
    }

    public async Task<CreateTrainingServiceOutput> CreateTrainingServiceAsync(
        CreateTrainingServiceInput input, 
        CancellationToken cancellationToken)
    {
        var training = Training.Factory(
            location: input.Location,
            distance: input.Distance,
            duration: input.Duration,
            date: input.Date,
            accountId: Guid.NewGuid());

        await _trainingRepository.CreateTrainingAsync(training, cancellationToken);

        return CreateTrainingServiceOutput.Factory(
            id: training.Id.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt);
    }

    public async Task<GetTrainingByIdServiceOutput> GetTrainingByIdServiceAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(id, cancellationToken);

        if (training is null)
            throw new KeyNotFoundException($"No training with ID {id} was found.");

        return GetTrainingByIdServiceOutput.Factory(
            id: training.Id.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt);
    }

    public async Task<UpdateTrainingByIdServiceOutput> UpdateTrainingByIdServiceAsync(
        IdValueObject id, 
        UpdateTrainingByIdServiceInput input, 
        CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(id, cancellationToken);

        if (training is null)
            throw new KeyNotFoundException($"No training with ID {id} was found.");

        training.UpdateTrainingDetails(
            location: input.Location,
            distance: input.Distance,
            duration: input.Duration,
            date: input.Date);

        await _trainingRepository.UpdateTrainingAsync(training, cancellationToken);

        return UpdateTrainingByIdServiceOutput.Factory(
            id: training.Id.ToString(),
            location: training.Location,
            distance: training.Distance,
            duration: training.Duration,
            date: training.Date,
            createdAt: training.CreatedAt);
    }

    public async Task DeleteTrainingByIdServiceAsync(IdValueObject id, CancellationToken cancellationToken)
    {
        var training = await _trainingRepository.GetTrainingByIdAsync(id, cancellationToken);

        if (training is null)
            throw new KeyNotFoundException($"No training with ID {id} was found.");

        await _trainingRepository.DeleteTrainingAsync(training, cancellationToken);
    }
}
