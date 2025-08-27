using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.TrainingContext.Outputs;

public readonly struct CreateTrainingServiceOutput
{
    public IdValueObject Id { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }
    public DateTime CreatedAt { get; }

    private CreateTrainingServiceOutput(IdValueObject id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt)
    {
        Id = id;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = createdAt;
    }

    public static CreateTrainingServiceOutput Factory(IdValueObject id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt)
        => new CreateTrainingServiceOutput(id, location, distance, duration, date, createdAt);
}
