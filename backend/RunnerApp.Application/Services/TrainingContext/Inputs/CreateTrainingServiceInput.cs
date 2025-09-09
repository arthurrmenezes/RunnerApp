using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.TrainingContext.Inputs;

public readonly struct CreateTrainingServiceInput
{
    public IdValueObject AccountId { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }

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
