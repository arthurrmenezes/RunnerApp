using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.WebApi.Controllers.TrainingContext.Payloads;

public readonly struct UpdateTrainingByIdPayload
{
    public LocationType? Location { get; init; }
    public double? Distance { get; init; }
    public TimeSpan? Duration { get; init; }
    public DateTime? Date { get; init; }

    public UpdateTrainingByIdPayload(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
    }
}
