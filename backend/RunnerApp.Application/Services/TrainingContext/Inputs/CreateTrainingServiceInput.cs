using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.TrainingContext.Inputs;

public readonly struct CreateTrainingServiceInput
{
    public IdValueObject AccountId { get; init; }
    public LocationType Location { get; init; }
    public double Distance { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTime Date { get; init; }

    private CreateTrainingServiceInput(IdValueObject accountId, LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        AccountId = accountId;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
    }

    public static CreateTrainingServiceInput Factory(IdValueObject accountId, LocationType location, double distance, TimeSpan duration, DateTime date)
        => new CreateTrainingServiceInput(accountId, location, distance, duration, date);
}
