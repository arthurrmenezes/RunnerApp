using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.WebApi.Controllers.TrainingContext.Payloads;

public readonly struct CreateTrainingPayload
{
    public string AccountId { get; init; }
    public LocationType Location { get; init; }
    public double Distance { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTime Date { get; init; }

    public CreateTrainingPayload(string accountId, LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        AccountId = accountId.ToString();
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
    }
}
