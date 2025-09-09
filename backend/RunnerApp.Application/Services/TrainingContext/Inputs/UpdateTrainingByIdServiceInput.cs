using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Inputs;

public readonly struct UpdateTrainingByIdServiceInput
{
    public LocationType? Location { get; }
    public double? Distance { get; }
    public TimeSpan? Duration { get; }
    public DateTime? Date { get; }

    private UpdateTrainingByIdServiceInput(LocationType? location, double? distance, TimeSpan? duration, DateTime? date)
    {
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
    }

    public static UpdateTrainingByIdServiceInput Factory(LocationType? location, double? distance, TimeSpan? duration, DateTime? date)
        => new UpdateTrainingByIdServiceInput(location, distance, duration, date);
}
