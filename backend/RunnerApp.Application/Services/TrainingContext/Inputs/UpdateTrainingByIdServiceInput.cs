using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Inputs;

public readonly struct UpdateTrainingByIdServiceInput
{
    public LocationType Location { get; init; }
    public double Distance { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTime Date { get; init; }

    private UpdateTrainingByIdServiceInput(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
    }

    public static UpdateTrainingByIdServiceInput Factory(LocationType location, double distance, TimeSpan duration, DateTime date)
        => new UpdateTrainingByIdServiceInput(location, distance, duration, date);
}
