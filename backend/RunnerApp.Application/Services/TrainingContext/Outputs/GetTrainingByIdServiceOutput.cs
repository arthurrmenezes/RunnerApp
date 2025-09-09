using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Outputs;

public readonly struct GetTrainingByIdServiceOutput
{
    public string Id { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }
    public DateTime CreatedAt { get; }
    public string AccountId { get; }

    private GetTrainingByIdServiceOutput(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, string accountId)
    {
        Id = id;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = createdAt;
        AccountId = accountId;
    }

    public static GetTrainingByIdServiceOutput Factory(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, string accountId)
        => new GetTrainingByIdServiceOutput(id, location, distance, duration, date, createdAt, accountId);
}
