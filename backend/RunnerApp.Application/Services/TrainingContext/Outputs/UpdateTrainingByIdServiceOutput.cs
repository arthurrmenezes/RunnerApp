using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Outputs;

public readonly struct UpdateTrainingByIdServiceOutput
{
    public string Id { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }
    public DateTime CreatedAt { get; }

    private UpdateTrainingByIdServiceOutput(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt)
    {
        Id = id;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = createdAt;
    }

    public static UpdateTrainingByIdServiceOutput Factory(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt)
        => new UpdateTrainingByIdServiceOutput(id, location, distance, duration, date, createdAt);
}
